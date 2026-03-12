using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System.Collections;
using System.Net;

public class Weather : MonoBehaviour
{
    public TMP_Text weatherText;
    string url = "https://api.openweathermap.org/data/2.5/weather?lat=17.36&lon=78.47&APPID=a4ca03f62690b6caa3ae68bb8ae3da00&units=imperial";
    public TimeZoneText timeZoneText;

    public GameObject sun;
    public GameObject moon;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("GetDataFromWeb", 2f, 900f);
    }

    private void Update()
    {
        if (timeZoneText.isNight)
        {
            sun.SetActive(false);
            moon.SetActive(true);
        } else
        {
            sun.SetActive(true);
            moon.SetActive(false);
        }
    }

    void GetDataFromWeb()
    {
        StartCoroutine(GetRequest(url));
    }

    IEnumerator GetRequest(string url)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url)) {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(": Error: " + webRequest.error);
            }
            else {
                Debug.Log(":\nReceived: " + webRequest.downloadHandler.text);

                int startTemp = webRequest.downloadHandler.text.IndexOf("temp", 0);
                int endTemp = webRequest.downloadHandler.text.IndexOf(",", startTemp);
                double tempF = float.Parse(webRequest.downloadHandler.text.Substring(startTemp + 6, (endTemp - startTemp - 6)));
                int easyTempF = Mathf.RoundToInt((float)tempF);
                Debug.Log("Integer temperature is " + easyTempF.ToString());
                int startConditions = webRequest.downloadHandler.text.IndexOf("main", 0);
                int endConditions = webRequest.downloadHandler.text.IndexOf(',', startConditions);
                string conditions = webRequest.downloadHandler.text.Substring(startConditions + 7, (endConditions - startConditions - 8));
                Debug.Log(conditions);

                weatherText.text = easyTempF.ToString() + "F\n" + conditions;
            }
        }
    }
}
