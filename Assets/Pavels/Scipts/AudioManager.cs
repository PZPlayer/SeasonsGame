using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("---Audio Source---")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("---Audio Clip---")]
    public AudioClip background;
    public AudioClip jump;
    public AudioClip land;
    public AudioClip die;
    public AudioClip slide;
    public AudioClip slideFast;
    public AudioClip walk;

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void PlayLoopSFX(AudioClip clip)
    {
        if(SFXSource.isPlaying == false)
        {
            SFXSource.clip = clip;
            SFXSource.Play();
        }
        else if (clip != SFXSource.clip)
        {
            SFXSource.Stop();
            SFXSource.clip = clip;
            SFXSource.Play();
        }
    }
}
