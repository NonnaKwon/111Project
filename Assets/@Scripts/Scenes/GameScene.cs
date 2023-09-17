using Data;
using DG.Tweening.Plugins.Core.PathCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Define;

public class GameScene : BaseScene
{
    UI_GameScene UIGameScene;

    private void Start()
    {
        Managers.Game.SetGameState(GameState.LoadingBackgrounds);
    }

    private void OnDisable()
    {
        if (Managers.Game != null)
        {
            Managers.Game.OnGameStateChange -= OnGameStateChange;
        }
    }

    protected override void Init()
    {
        Debug.Log("@>> GameScene Init()");
        base.Init();

        SceneType = Define.Scene.GameScene;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Managers.Game.OnGameStateChange += OnGameStateChange;
    }



    public void OnGameStateChange(GameState state)
    {
        switch (state)
        {
            case GameState.Ready:
                break;
            case GameState.LoadingBackgrounds:
                CoroutineManager.StartCoroutine(LoadStage());
                break;
            case GameState.Play:
                break;
            case GameState.Die:
                break;
            case GameState.Result:
                break;
        }
    }

    IEnumerator LoadStage()
    {
        UI_Loading loading = Managers.UI.ShowPopupUI<UI_Loading>();
        yield return new WaitForSecondsRealtime(0.5f);


        UIGameScene = Managers.UI.ShowSceneUI<UI_GameScene>();

        yield return new WaitForSecondsRealtime(0.5f);
        Managers.UI.ClosePopupUI(loading);
        Managers.Game.SetGameState(GameState.LoadingWave);
    }




    public override void Clear()
    {

    }

}
