using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicVolume : MonoBehaviour
{
    [SerializeField] private GameObject bgMusic;
    [SerializeField] private GameObject gameMusic;
    private Slider slider;
    private AudioSource bgMusicAudio;
    private AudioSource gameMusicAudio;
    void Start()
    {
        bgMusicAudio = bgMusic.GetComponent<AudioSource>();
        gameMusicAudio = gameMusic.GetComponent<AudioSource>();
        slider = gameObject.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        bgMusicAudio.volume = slider.value*0.25f;
        gameMusicAudio.volume = slider.value*0.25f;
    }
}
