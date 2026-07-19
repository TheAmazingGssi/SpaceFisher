using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public float MasterVolume { get => GetVolume(MASTER_STRING); }
    public float BGMvolume { get => GetVolume(BGM_STRING); }
    public float SFXvolume { get => GetVolume(SFX_STRING); }

    [SerializeField] GameObject soundEffectSource;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] AudioSource bgmPlayer;
    const string MASTER_STRING = "VolMaster";
    const string BGM_STRING = "VolBMG";
    const string SFX_STRING = "VolSFX";
    const string WIND_STRING = "VolWindSfX";
    const float VOL_MULT = 20;
    List<SoundEffect> DormantSoundEffects = new List<SoundEffect>();

    private void Start()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    #region BGM
    public void SetBGM(AudioClip song)
    {
        bgmPlayer.clip = song;
    }
    #endregion
    #region Sound effects
    //with basic object pooling managing
    public void PlaySoundEffect(AudioClip sound)
    {
        if (DormantSoundEffects.Count == 0)
        {
            SoundEffect se = Instantiate(soundEffectSource, transform).GetComponent<SoundEffect>();
            se.ClipEnd.AddListener(OnClipEnd);
            DormantSoundEffects.Add(se);
        }

        DormantSoundEffects[0].PlaySoundEffect(sound);
        DormantSoundEffects.RemoveAt(0);
    }
    private void OnClipEnd(SoundEffect source)
    {
        DormantSoundEffects.Add(source);
    }
    #endregion
    #region Volume
    void ChangeVolume(string paramName, float value)
    {
        value = Mathf.Clamp(value, 0.0001f, 1.0f);
        value = Mathf.Log(value, 2) * VOL_MULT;
        audioMixer.SetFloat(paramName, value);
    }
    float GetVolume(string paramName)
    {
        if (!audioMixer.GetFloat(paramName, out float value))
            Debug.LogError(nameof(paramName) + " not leading to any mixer variable");
        value /= VOL_MULT;
        float returnValue = Mathf.Pow(2, value);
        return returnValue;

    }
    public void ChangeMasterVolume(float value)
    {
        ChangeVolume(MASTER_STRING, value);
    }
    public void ChangeBGMVolume(float value)
    {
        ChangeVolume(BGM_STRING, value);
    }
    public void ChangeSFXVolume(float value)
    {
        ChangeVolume(SFX_STRING, value);
    }
    #endregion
    #region Unique Responsibilities
    //speed here arrives as 0-1 
    public void RunSpeedVolume(float speedVolume)
    {
        ChangeVolume(WIND_STRING, speedVolume);
    }
    #endregion
}