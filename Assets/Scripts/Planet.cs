using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
 
public class Planet : MonoBehaviour
{
   
   public Animator Anim;

   void OnTriggerEnter(Collider other) {
       if (other.CompareTag("Player")) {
           StartCoroutine(Load());
       }
   }

    IEnumerator Load()
    {
        Anim.SetBool("Fade", true);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Planeta");
        
    }

}
