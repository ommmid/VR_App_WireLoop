using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCounter : MonoBehaviour {

    public Text timeText;
    private float startTime;
    private float finishTime;
    private bool doStart = false;
    private bool doFinish = false;

    // Use this for initialization
    void Start () {
        
    }

    public void CounterStart()
    {
        startTime = Time.time;
        this.doStart = true;
        this.doFinish = false;
    }
    public void CounterFinish()
    {
        finishTime = Time.time;
        this.doFinish = true;
        this.doStart = false;
    }

    // Update is called once per frame
    void Update () {
       
        if (this.doStart)
        {
            float t = Time.time - startTime;
            string minutes = ((int)t / 60).ToString("f2");
            string seconds = (t % 60).ToString("f2");
            timeText.text = minutes + ":" + seconds;
        }
        if (this.doFinish)
        {
            float s = finishTime - startTime;
            string min = ((int)s / 60).ToString("f2");
            string sec = (s % 60).ToString("f2");
            timeText.text = min + ":" + sec;
        }
    }
}