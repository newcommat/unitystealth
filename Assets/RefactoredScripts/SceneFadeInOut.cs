using UnityEngine;
using System.Collections;

public class SceneFadeInOut : MonoBehaviour 
{
    private enum State { Starting, Playing, Ending };

    public float fadeSpeed = 1.5f;

    private State state;

    void Awake()
    {
        state = State.Starting;
        guiTexture.pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
    }

    void Update()
    {
        switch (state)
        {
            case State.Starting:
                StartScene();
                break;
            case State.Playing:
                break;
            case State.Ending:
                EndingScene();
                break;
        }
    }

    void FadeToClear()
    {
        guiTexture.color = Color.Lerp(guiTexture.color, Color.clear, fadeSpeed * Time.deltaTime);
    }

    void FadeToBlack()
    {
        guiTexture.color = Color.Lerp(guiTexture.color, Color.black, fadeSpeed * Time.deltaTime);
    }

    void StartScene()
    {
        FadeToClear();

        if (guiTexture.color.a <= 0.05f)
        {
            guiTexture.color = Color.clear;
            guiTexture.enabled = false;
            state = State.Playing;
        }
    }

    private void EndingScene()
    {
        guiTexture.enabled = true;
        FadeToBlack();

        if (guiTexture.color.a >= 0.95f)
        {
            Application.LoadLevel(1);
        }
    }

    public void EndScene()
    {
        state = State.Ending;
    }
}
