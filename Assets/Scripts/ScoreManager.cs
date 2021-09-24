using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static int highScore = 0;
    public Text scoreText;
    public bool playOnAwake = true;
    public float delayToStart;
    public float delayBetweenChars = 0.125f;
    public float delayAfterPunctuation = 0.5f;

    private string story;
    private float originDelayBetweenChars;
    private bool lastCharPunctuation = false;
    private char charComma;
    private char charPeriod;

    void Awake()
    {
        if ((int)UI.days >= highScore)
        {
            highScore = (int) UI.days;

        }
        originDelayBetweenChars = delayBetweenChars;

        charComma = Convert.ToChar(44);
        charPeriod = Convert.ToChar(46);

        if (playOnAwake)
        {
            ChangeText("MISSION GOAL: " + highScore.ToString() + " DAYS\nMISSION REPORT: " + ((int)UI.days).ToString() + " DAYS", delayToStart);
        }
    }

    //Update text and start typewriter effect
    public void ChangeText(string textContent, float delayBetweenChars = 0f)
    {
        StopCoroutine(PlayText()); //stop Coroutime if exist
        story = textContent;
        scoreText.text = "";
        Invoke("Start_PlayText", delayBetweenChars); //Invoke effect
    }

    void Start_PlayText()
    {
        StartCoroutine(PlayText());
    }

    IEnumerator PlayText()
    {

        foreach (char c in story)
        {
            delayBetweenChars = originDelayBetweenChars;

            if (lastCharPunctuation)  //If previous character was a comma/period, pause typing
            {
                yield return new WaitForSeconds(delayBetweenChars = delayAfterPunctuation);
                lastCharPunctuation = false;
            }

            if (c == charComma || c == charPeriod)
            {
                lastCharPunctuation = true;
            }

            scoreText.text += c;
            yield return new WaitForSeconds(delayBetweenChars);
        }
    }
}
