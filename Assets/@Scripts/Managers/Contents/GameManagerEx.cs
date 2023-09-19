using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class GameManagerEx
{
    public PlayerController Player { get; set; }
    public PlayerController Enemy { get; set; }
    public string GameResult { get; set; }

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
        if (PhotonNetwork.IsMasterClient)
        {
            Managers.Scene.LoadScene(Define.Scene.GameScene);
   
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
