using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
    public float musicFadeSpeed = 1f;

    private AudioSource panicAudio;
    private SceneFadeInOut sceneFadeInOut;

    void Awake()
    {
        EventDispatcher.RegisterCallback<PlayerDiedEvent>(ResetLevel);
        EventDispatcher.RegisterCallback<DynamicExitLiftFinishedEvent>(ResetLevel);
        panicAudio = transform.Find("secondaryMusic").audio;
        sceneFadeInOut = GameObject.FindGameObjectWithTag(Tag.fader).GetComponent<SceneFadeInOut>();
    }

    void Update()
    {
        MusicFading();
    }

    private void ResetLevel(Event anyEvent)
    {
        sceneFadeInOut.EndScene();
    }

    void MusicFading()
    {
        audio.volume = Mathf.Lerp(audio.volume, 0f, musicFadeSpeed * Time.deltaTime);
        panicAudio.volume = Mathf.Lerp(panicAudio.volume, 0.8f, musicFadeSpeed * Time.deltaTime);
    }

}
