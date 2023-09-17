using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;
using static Define;

public class GameManagerEx
{
    public event Action<GameState> OnGameStateChange;
    public int Wave { get; set; } = 0; //맵에 따라 웨이브 갯수 다름
    public int NowWave { get { return _nowWave; } }

    private int _nowWave;
    private GameState CurrentState;

    public void Clear()
    {
        CurrentState = GameState.Ready;
        OnGameStateChange = null;
    }


    public void StartGame()
    {
        Debug.Log("Start Game()");
        _nowWave = 1;
        Managers.Scene.LoadScene(Define.Scene.GameScene);
    }


    public void NextWave()
    {
        _nowWave++;
        if(_nowWave >= Wave)
        {
            SetGameState(GameState.Result);
        }
    }


    #region GameState
    public void SetGameState(GameState newState)
    {
        CurrentState = newState;
        OnGameStateChange?.Invoke(CurrentState);
        HandleGameState();
    }

    private void HandleGameState()
    {
        switch (CurrentState)
        {
            case GameState.Ready:
                break;
            case GameState.LoadingBackgrounds:
                break;
            case GameState.LoadingWave:
                break;
            case GameState.Play:
                break;
            case GameState.Die:
                break;
            case GameState.Result:
                break;
        }
    }
    #endregion

}
