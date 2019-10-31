using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public Text timerText;
    public double penalty;
    public static Timer instance;
    public double timeTackOn;
    public double opponentPenalty;



    private float startTime;
    private float time;
    private string minutes;
    private string seconds;
    
    
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        timeTackOn = 0;
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (PlayerController.instance.finished == true && OpponentController.instance.finished == true)
        {
            nextScene();
            //Move to the next Scene after both racers are finished!
        }
        else
        {
            time = Time.time - startTime;

            minutes = ((int)time / 60).ToString();
            seconds = (time % 60).ToString("f3");

            timerText.text = minutes + ":" + seconds;
        }
        
    }

    public void hitHurdle()
    {
        timeTackOn += penalty;
    }

    public void opponentHitHurdle()
    {
        opponentPenalty += penalty;
    }

    public void nextScene()
    {
        
        //WaitForSeconds(5);
        SceneManager.LoadScene("WinningScene", LoadSceneMode.Single);
        
    }
}
