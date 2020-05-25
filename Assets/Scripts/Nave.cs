using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Nave : MonoBehaviour
{

    public float torque = 0.05f;
    public float speed = 1.0f;
    public GameObject bullet;
    public GameObject planet;
    public Text scoreText;
    public Text objective1;
    public Text objective2;
    private static int score;
    public AudioClip Clip;
    public AudioClip Clip1;
    private AudioSource audioSource;


    public float fireDelta = 0.5F;
    private float nextFire = 0.5F;
    private float myTime = 0.0F;

    void Start() {
        objective1.enabled = true;
        objective2.enabled = false;
        planet.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        score = 0;
    }

    void Update()
    {
        myTime = myTime + Time.deltaTime;
        score = Asteroid.Score;
        scoreText.text = score.ToString();
        
        
        if (Input.GetButton("Fire2") && myTime > nextFire)
        {
            audioSource.PlayOneShot(Clip);
            nextFire = myTime + fireDelta;
            GameObject instancia = Instantiate(bullet, transform.position + (transform.forward * 2), transform.rotation) as GameObject;
            instancia.GetComponent<Rigidbody>().velocity = 20.0f * transform.forward;
            Destroy(instancia, 5.0f); // Destroi o tiro depois de 5 segundos
            nextFire = nextFire - myTime;
            myTime = 0.0F;;
        }

        if (score == 20){
            objective1.color = new Color(0,255,0,1);
            scoreText.color = new Color(0,255,0,1);
            StartCoroutine(NewObj());
        }

        if (Asteroid.flag == 1){
            audioSource.PlayOneShot(Clip1);
            Asteroid.flag = 0;
        }    


    }

    IEnumerator NewObj()
    {
        yield return new WaitForSeconds(2);
        objective1.enabled = false;
        objective2.enabled = true;
        planet.SetActive(true);

    }

    void OnTriggerEnter(Collider other) {
       if (other.CompareTag("planeta")) {
           objective2.color = new Color(0,255,0,1);
       }
   }


    void FixedUpdate()
    {

        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        float p = Input.GetAxis("Jump");

        Rigidbody rb = GetComponent<Rigidbody>();

        rb.AddForce(transform.forward * speed * p);
        rb.AddTorque(transform.up * torque * h);
        rb.AddTorque(-transform.right * torque * v);
    }

}
