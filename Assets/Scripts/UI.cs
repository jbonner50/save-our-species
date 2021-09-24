using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public static float days = 0f;
    public Text timerTextBox;
    //public Text healthTextBox;


    public static bool timerActive = true;
    // Start is called before the first frame update
    void Start()
    {
        timerTextBox.text = "DAY " + ((int)days).ToString();
        startTimer();
        //healthTextBox.text = "HEALTH (MAX 10): "; // + Earth.health.ToString();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(timerActive)
        {
            days += Time.deltaTime;
            timerTextBox.text = "DAY " + ((int)days).ToString();
        }
        //healthTextBox.text = "HEALTH (MAX 10): "; // + Earth.health.ToString();

    }

    public static void startTimer()
    {
        days = 0f;
        timerActive = true;
    }
    public static void stopTimer()
    {
        timerActive = false;
    }
}
