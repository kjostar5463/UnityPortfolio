using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : Singleton<TimeManager>
{
    //time value
    int _timeMin;
    int _timeSec;
    public string strMin;
    public string strSec;

    //timerColor
    Color _defaultColor = new Color(219 / 255f, 214 / 255f, 187 / 255f);
    Color _warningColor = new Color(255 / 255f, 35 / 255f, 35 / 255f);
    public Color timerColor;

    WaitForSeconds _time = new WaitForSeconds(0.1f);

    // Start is called before the first frame update
    void Start()
    {
        TimeReset();
        StartCoroutine(TimerCoroutine());
    }

    //timer
    IEnumerator TimerCoroutine()
    {
        yield return _time;

        if (_timeSec == 0)
        {
            if (_timeMin == 0)
            {
                TimeReset();
            }
            else
            {
                _timeSec = 59;
                _timeMin--;
            }
        }
        else
            _timeSec--;

        if(_timeSec < 31 && _timeMin == 0)
        {
            timerColor = _warningColor;
        }

        // integer -> string
        strMin = _timeMin.ToString();

        if(_timeSec < 10)
        {
            strSec = '0' + _timeSec.ToString();
        }
        else
            strSec = _timeSec.ToString();
        
            
        StartCoroutine(TimerCoroutine());
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    //Reset
    private void TimeReset()
    {
        timerColor = _defaultColor;
        _timeMin = 3;
        _timeSec = 0;
    }
}
