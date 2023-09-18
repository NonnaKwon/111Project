using System;
using System.Collections;
using UnityEngine;
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
                Managers.UI.ShowPopupUI<UI_GameResult>();
                break;
        }
    }

    IEnumerator LoadStage()
    {
        UI_Loading loading = Managers.UI.ShowPopupUI<UI_Loading>();
        yield return new WaitForSecondsRealtime(0.5f);

        Managers.Game.Player = Managers.Resource.Instantiate("Player").GetOrAddComponent<PlayerController>();
        Managers.Game.Enemy = Managers.Resource.Instantiate("Enemy").GetOrAddComponent<PlayerController>();
        Managers.Resource.Instantiate("Background").GetComponent<SpriteRenderer>().sortingOrder = (int)Define.SortOrder.Backgound;
        Managers.Resource.Instantiate("Collider");
        UIGameScene = Managers.UI.ShowSceneUI<UI_GameScene>();

        yield return new WaitForSecondsRealtime(0.5f);
        Managers.UI.ClosePopupUI(loading);
        Managers.Game.SetGameState(GameState.Play);
    }




    public override void Clear()
    {

    }

}
