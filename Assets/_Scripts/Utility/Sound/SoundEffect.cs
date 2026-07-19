using UnityEngine;
using UnityEngine.Events;

public class SoundEffect : MonoBehaviour
{
    [SerializeField] AudioSource AudioSource;
    public UnityEvent<SoundEffect> ClipEnd;

    public void PlaySoundEffect(AudioClip sound)
    {
        gameObject.SetActive(true);
        AudioSource.clip = sound;
        AudioSource.Play();
        Invoke("OnClipEnd", sound.length);
    }
    private void OnClipEnd()
    {
        ClipEnd?.Invoke(this);
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        ClipEnd.RemoveAllListeners();
    }
}