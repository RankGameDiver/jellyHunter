using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundM : MonoBehaviour
{
    public AudioClip[] sound;

    private Button button { get { return gameObject.GetComponent<Button>(); } }
    private AudioSource source { get { return gameObject.GetComponent<AudioSource>(); } }

    void Start()
    {
        gameObject.AddComponent<AudioSource>();
        source.clip = sound[0];
        source.playOnAwake = false;
        // AudioSource 안에 다른 초기화 설정은 아래에 추가

        //button.onClick.AddListener(() => PlaySound());
    }

    public void PlaySound()
    {
        source.PlayOneShot(sound[0]);
    }

    public void PlaySound(int temp)
    {
        source.PlayOneShot(sound[temp]);
    }

    public void SetSoundClip(int temp)
    {
        source.clip = sound[temp];
        source.Play();
    }

    public void StopSound()
    {
        source.Stop();
    }
}
