﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    public AudioClip sound;

    private Button button { get { return GetComponent<Button>(); } }
    private AudioSource source { get { return GetComponent<AudioSource>(); } }

    void Start()
    {
        gameObject.AddComponent<AudioSource>();
        source.clip = sound;
        source.playOnAwake = false;
        // AudioSource 안에 다른 초기화 설정은 아래에 추가

        button.onClick.AddListener(() => PlaySound());
    }

    void PlaySound()
    {
        source.PlayOneShot(sound);
    }
}
