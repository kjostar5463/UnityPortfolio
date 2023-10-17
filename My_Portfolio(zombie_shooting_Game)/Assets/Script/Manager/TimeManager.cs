using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : Singleton<TimeManager>
{
    //time value
    int timeMin;
    int timeSec;
    public string stringMin;
    public string stringSec;

    //timerColor
    Color defaultColor = new Color(219 / 255f, 214 / 255f, 187 / 255f);
    Color warningColor = new Color(255 / 255f, 35 / 255f, 35 / 255f);
    public Color timerColor;

    // Start is called before the first frame update
    void Start()
    {
        timeReset();
        StartCoroutine(TimerCoroutine());
    }

    //timer
    IEnumerator TimerCoroutine()
    {
        yield return new WaitForSeconds(0.1f);

        if (timeSec == 0)
        {
            if (timeMin == 0)
            {
                timeReset();
            }
            else
            {
                timeSec = 59;
                timeMin--;
            }
        }
        else
            timeSec--;

        if(timeSec < 31 && timeMin == 0)
        {
            timerColor = warningColor;
        }

        // integer -> string
        stringMin = timeMin.ToString();

        if(timeSec < 10)
        {
            stringSec = '0' + timeSec.ToString();
        }
        else
            stringSec = timeSec.ToString();
        
            
        StartCoroutine(TimerCoroutine());
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    //Reset
    private void timeReset()
    {
        timerColor = defaultColor;
        timeMin = 3;
        timeSec = 0;
    }
}
