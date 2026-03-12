using UnityEngine;
using System;
using TMPro;
public class TimeZoneText : MonoBehaviour
{
    public String Unitytimezone;
    public String Androidtimezone;
    private DateTime targetTime;
    public bool isNight = false;

    private string timezone;
    void Start()
    {
        #if UNITY_EDITOR
            timezone = Unitytimezone;
        #else
            timezone = Androidtimezone; 
        #endif
    }

    // Update is called once per frame
    void Update()
    {
        DateTime utcNow = DateTime.UtcNow;
        TimeSpan istOffset = new TimeSpan(5, 30, 0);
        DateTime istTime = utcNow.Add(istOffset);
        int currentHour = istTime.Hour;
        this.gameObject.GetComponent<TMP_Text>().text = istTime.ToString("HH:mm:ss");

        if (currentHour >= 6 && currentHour < 18){
            isNight = false;
        } else
        {
            isNight=true;
        }
    }
}
