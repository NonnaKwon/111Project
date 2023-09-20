using Photon.Pun;
using System;
using System.Collections;
using UnityEngine;
using static Define;

public class GameScene : BaseScene, IPunObservable
{
    UI_GameScene UIGameScene;
    PhotonView _pv;
    int _compeleteLoad;
    bool _compelete;

    private void Start()
    {
        _compeleteLoad = 0;
        _compelete = false;
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
        _pv = GetComponent<PhotonView>();
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
        Managers.UI.ShowPopupUI<UI_Loading>();
        Time.timeScale = 1;
        Managers.Game.Player = Managers.Resource.NetworkInstantiate("Player").GetOrAddComponent<PlayerController>();
        yield return new WaitForSecondsRealtime(0.5f);
        //Managers.Game.Enemy = Managers.Resource.NetworkInstantiate("Enemy").GetOrAddComponent<PlayerController>();
        Managers.Resource.Instantiate("Background").GetComponent<SpriteRenderer>().sortingOrder = (int)Define.SortOrder.Backgound;
        Managers.Resource.Instantiate("Collider");

        yield return new WaitForSecondsRealtime(0.5f);
        //Time.timeScale = 0;

        PlayerController[] pc;
        while (true)
        {
            pc = GameObject.FindObjectsOfType<PlayerController>();
            if (pc.Length == 2)
                break;
            yield return new WaitForSecondsRealtime(0.5f);
        }

        foreach(PlayerController player in pc)
        {
            Debug.Log("Player Search : " + player.ToString());
            PhotonView photonView = player.GetComponent<PhotonView>();
            if (!photonView.IsMine)
            {
                Managers.Game.Enemy = player;
                player.gameObject.name = "Enemy";
                player.GetComponent<Collider2D>().offset = new Vector2(0, 0.2f);
                player.transform.position = new Vector3(0, Define.ENEMY_Y, 0);

                SpriteRenderer sr = player.GetComponent<SpriteRenderer>();
                sr.flipY = true;
                sr.color = new Color(1f,0.5f,0.5f,1f);
            }else
            {
                player.gameObject.layer = (int)Define.Layer.Player;
                player.transform.position = new Vector3(0, Define.PLAYER_Y, 0);
            }
        }

        yield return new WaitForSecondsRealtime(0.5f);
        Managers.UI.ClosePopupUI();
        UIGameScene = Managers.UI.ShowSceneUI<UI_GameScene>();
        Debug.Log("LoadStage 완");

        Managers.Game.SetGameState(GameState.Play);
    }




    public override void Clear()
    {

    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }
}
