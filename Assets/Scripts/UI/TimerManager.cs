using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    [Header("Time")]
    public FloatVariableSO longestTime;
    public float time;

    [Header("Text UI")]
    public Text timeText;

    private float msec;
    private float sec;
    private float min;

    private void Start()
    {
        StartTimer();
    }

    public void StartTimer()
    {
        StartCoroutine("Timer");
    }

    public void StopTimer()
    {
        StopCoroutine("Timer");
    }

    private void UpdateTimeRecord()
    {
        longestTime.SetValue((time > longestTime.Value) ? 
            time : longestTime.Value); 
    }


    IEnumerator Timer()
    {
        while (true)
        {
            time += Time.deltaTime;
            msec = (int)((time - (int)time) * 100);
            sec = (int)(time % 60);
            min = (int)(time / 60 % 60);

            timeText.text = string.Format("{0:00}:{1:00}:{2:00}",
                min, sec, msec);

            yield return null;
        }
    }

}
