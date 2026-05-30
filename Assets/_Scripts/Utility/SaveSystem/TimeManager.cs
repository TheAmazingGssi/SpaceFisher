using System;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private void Awake()
    {
        if (PlayerPrefs.HasKey("save"))
        {
            string timeAsString = PlayerPrefs.GetString("save");
            DateTime savedTime = DateTime.Parse(timeAsString);
            float secondsOffline = (float)(DateTime.Now - savedTime).TotalSeconds;
            Bus<OfflineTimeCalculated>.Raise(new OfflineTimeCalculated{ SecondsOffline = secondsOffline });
        }
    }

    private void OnApplicationQuit()
    {
        SaveTime();
    }

    private void OnApplicationPause(bool paused)
    {
        if (paused) SaveTime();
    }

    private void SaveTime()
    {
        PlayerPrefs.SetString("save", DateTime.Now.ToString());
        PlayerPrefs.Save();
    }
}