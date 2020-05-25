using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update
    
    public static float currentTime = 0f;
    float ime = 0.0f;
    public Text timer;
    public static bool flag = false;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        if (flag){
        ime = Time.time;
        flag = false;
        }

        currentTime = 500f - Time.time + ime;
        timer.text = currentTime.ToString();
        
        if (currentTime <= 0) {
            SceneManager.LoadScene("GameOver");
        }

    }
}
