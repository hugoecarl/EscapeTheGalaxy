using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
   public float speed = 8; // velocidade do jogador
   public float gravity = -9.8f; // valor da gravidade
   public LayerMask groundMask;
   CharacterController character;
   Vector3 velocity;
   bool isGrounded;
   private Animator anim;
   public Animator anim1;
   public Rigidbody rb;
   private Vector3 moveDirection = Vector3.zero;
   int count = 0;

    public Text rock;
    public Text objective1;
    public Text objective2;
    public Text objective3;
    public Text objective4;
    public Text objective5;
    public GameObject temple;
    public GameObject portal;
    public GameObject ball;
    public GameObject portalflame1;
    public GameObject portalflame2;
    public AudioClip Clip;
    public AudioClip Clip1;
    public AudioClip Clip2;
    public AudioClip Jumpsound;
    public AudioClip getrocksound;
    public AudioClip portalsound;
    private AudioSource audioSource;

    void Start()
   {
       character = gameObject.GetComponent<CharacterController>();
       anim = GetComponent<Animator>();
       rb = GetComponent<Rigidbody>();
       objective1.enabled = true;
       objective2.enabled = false;
       objective3.enabled = false;
       objective4.enabled = false;
       objective5.enabled = false;
       rock.enabled = false;
       temple.SetActive(false);
       portal.SetActive(false);
       audioSource = GetComponent<AudioSource>();
       audioSource.PlayOneShot(Clip);
   }
 
   void Update()
   {
 
       // Verifica se encostando no chão (o centro do objeto deve ser na base)
       isGrounded = Physics.CheckSphere(transform.position, 0.2f, groundMask);
      
       // Se no chão e descendo, resetar velocidade
       if(isGrounded && velocity.y < 0.0f) {
           velocity.y = -1.0f;
       }
 
       anim.SetBool("jump", false);
       float x = Input.GetAxis("Horizontal");
       float z = Input.GetAxis("Vertical");
 
       anim.SetFloat("virar", x);
       anim.SetFloat("correr", z);

       // Rotaciona personagem
       transform.Rotate(0, x * speed * 10 * Time.deltaTime, 0);
 
       // Move personagem
       Vector3 move = transform.forward * z;
       character.Move(move * Time.deltaTime * speed);
 
       // Aplica gravidade no personagem
       velocity.y += gravity * Time.deltaTime;
       character.Move(velocity * Time.deltaTime);
      if (Input.GetButtonDown("Jump") && isGrounded) {
            moveDirection.y = 1;
            StartCoroutine(Jumps());
      }

      if (count >= 6 && Input.GetKeyDown(KeyCode.T)){
          ball.transform.localScale += new Vector3(0.04f,0.04f,0.04f);
          portalflame1.transform.localScale += new Vector3(0.04f,0.04f,0.04f);
          portalflame2.transform.localScale += new Vector3(0.04f,0.04f,0.04f);
          audioSource.PlayOneShot(portalsound);
          count++;
      }

      if (count >= 107)
      StartCoroutine(End());
   }

    IEnumerator Jumps()
    {
        audioSource.PlayOneShot(Jumpsound);
        for (int i = 0; i < 18; i++){
        anim.SetBool("jump", true);
        moveDirection.y -= -80 * Time.deltaTime;
        character.Move(moveDirection * Time.deltaTime);
        yield return new WaitForSeconds(0.000001f);
        }     
    }

    IEnumerator NewObj()
    {
        objective1.color = new Color(0,255,0,1);
        yield return new WaitForSeconds(1.0f);
        objective1.enabled = false;
        if (count == 0){
        objective2.enabled = true;
        audioSource.Stop();
        audioSource.PlayOneShot(Clip1);}
        rock.enabled = true;
            
    }

    IEnumerator NewObj1()
    {
        objective2.color = new Color(0,255,0,1);
        rock.color = new Color(0,255,0,1);
        yield return new WaitForSeconds(1.0f);
        objective2.enabled = false;
        objective3.enabled = true;

            
    }

    IEnumerator NewObj2()
    {
        objective3.color = new Color(0,255,0,1);
        audioSource.Stop();
        audioSource.PlayOneShot(Clip2);
        yield return new WaitForSeconds(1.0f);
        objective3.enabled = false;
        temple.SetActive(true);
        portal.SetActive(true);
        objective4.enabled = true;
  
    }

    IEnumerator NewObj3()
    {
        objective4.color = new Color(0,255,0,1);
        yield return new WaitForSeconds(1.0f);
        objective4.enabled = false;
        objective5.enabled = true;
        count++;
  
    }
    
    IEnumerator End()
    {
        objective5.color = new Color(0,255,0,1);
        yield return new WaitForSeconds(0.5f);
        anim1.SetBool("Fade", true);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Win");

    }
    

    void OnTriggerEnter(Collider other) {
       if (other.CompareTag("maze")) {
           StartCoroutine(NewObj());
       }
        if (other.CompareTag("rock")) {
           count++;
           audioSource.PlayOneShot(getrocksound);
           Destroy(other.gameObject);
           rock.text = count.ToString()+"/5";
           if (count == 5)
               StartCoroutine(NewObj1());   
       }
        if (other.CompareTag("saidamaze") && count == 5) {
           StartCoroutine(NewObj2());
       }
        if (other.CompareTag("entradatemplo") && count == 5) {
           StartCoroutine(NewObj3());
       }

   }



}
