using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class SplashScreenState : GameState
{ 

    public SplashScreenState(GameManager gm) : base(gm)
    {

    }

    public override GameStateType GameStateType
    {
        get
        {
            return GameStateType.SplashScreen;
        }
    }

    public override void UpdateGUI()
    {
        throw new NotImplementedException();
    }

    public override void OnStateEnter()
    {
        if (_gameManager.SkipSplashScreen)
        {
            _gameManager.ChangeState(GameStateType.MainMenu);
        }
        else
        {
            _gameManager.TextDescription.alignment = TextAnchor.MiddleCenter;
            _gameManager.StartCoroutine(playSplashScreenAudio());
        }
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateUpdate()
    {
        
    }

    IEnumerator playSplashScreenAudio()
    {
        for (int i = 0; i < _gameManager.SplashScreenAudio.Length; i++)
        {
            _gameManager.TextDescription.text = _gameManager.SplashScreenText[i];
            _gameManager.Narrator.Play(_gameManager.SplashScreenAudio[i]);
            yield return new WaitForSeconds(_gameManager.SplashScreenAudio[i].length * (1 / _gameManager.Narrator.Speed));
        }
        _gameManager.ChangeState(GameStateType.MainMenu);
    }
}