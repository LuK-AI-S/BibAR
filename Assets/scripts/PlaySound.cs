using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public AudioSource audioPlayer;
    public Animator soundAnim;

    public AudioClip[] soundFiles;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SwitchAudio()
    {
        audioPlayer.clip = soundFiles[0];
    }

    public void StartAudio()
    {
        audioPlayer.Play();
        soundAnim.SetTrigger("PlaySound");
    }

    public void PauseAudio()
    {
        audioPlayer.Pause();
        soundAnim.SetTrigger("StopSound");
    }

    public void UnpauseAudio()
    {
        audioPlayer.UnPause();
        soundAnim.SetTrigger("PlaySound");
    }
}
