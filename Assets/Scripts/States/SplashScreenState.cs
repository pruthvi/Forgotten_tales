using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class SplashScreenState : GameState
{
    public bool SkipSplashScreen;
    public AudioClip[] AudioSplashScreen;
    public string[] AudioTextDescription;

    public override GameStateType GameStateType
    {
        get
        {
            return GameStateType.SplashScreen;
        }
    }

    public override void OnGUIChange()
    {
        
    }

    public override void OnEnter()
    {
        if (SkipSplashScreen)
        {
            _gm.ChangeState(GameStateType.MainMenu);
        }
        else
        {
            _gm.UIManager.TextContent.alignment = TextAnchor.MiddleCenter;
            _gm.StartCoroutine(playSplashScreenAudio());
        }
    }

    public override void OnExit()
    {
    }

    public override void OnUpdate()
    {

    }

    IEnumerator playSplashScreenAudio()
    {
        for (int i = 0; i < AudioSplashScreen.Length; i++)
        {
            _gm.UIManager.Content = AudioTextDescription[i];
            _gm.Narrator.Play(AudioSplashScreen[i]);
            yield return new WaitForSeconds(AudioSplashScreen[i].length * (1 / _gm.Narrator.Speed));
        }
        _gm.ChangeState(GameStateType.MainMenu);
    }

    public override void OnInput()
    {
        
    }
}