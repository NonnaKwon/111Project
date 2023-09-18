using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class GameManagerEx
{
    public event Action<GameState> OnGameStateChange;
    private GameState CurrentState;

    public void Clear()
    {
        CurrentState = GameState.Ready;
        OnGameStateChange = null;
    }


    public void StartGame()
    {
        Debug.Log("Start Game()");
        Managers.Scene.LoadScene(Define.Scene.GameScene);
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
