using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private Control ctrl;
    public AudioClip cursor;
    public AudioClip drop;
    public AudioClip control;
    public AudioClip lineClear;

    private AudioSource audioSource;

    private bool isMute = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        ctrl = GetComponent<Control>();
    }

    public void PlayCursor()
    {
        PlayAudio(cursor);
    }

    public void PlayDrop()
    {
        PlayAudio(drop);
    }

    public void PlayControl()
    {
        PlayAudio(control);
    }

    public void PlayLineClear()
    {
        PlayAudio(lineClear);
    }

    private void PlayAudio(AudioClip clip)
    {
        if (isMute)
            return;
        audioSource.clip = clip;
        audioSource.Play();
    }

    /// <summary>
    /// ½ûÒô°´Å¥
    /// </summary>
    public void OnAudioButtonClick()
    {
        isMute = !isMute;
        ctrl.view.SetMuteActive(isMute);
        if(isMute==false)
        {
            PlayCursor();
        }
    }
}
