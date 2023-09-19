using Photon.Pun;
using System;
using System.Collections;
using UnityEngine;
using static Define;

public class GameScene : BaseScene, IPunObservable
{
    UI_GameScene UIGameScene;
    PhotonView _pv;
    bool _compeleteLoad;
    private void Start()
    {
        _compeleteLoad = false;
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
                NetworkLoad();
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
        yield return new WaitForSecondsRealtime(0.5f);

        Managers.Game.Player = Managers.Resource.NetworkInstantiate("Player").GetOrAddComponent<PlayerController>();
        //Managers.Game.Enemy = Managers.Resource.NetworkInstantiate("Enemy").GetOrAddComponent<PlayerController>();
        Managers.Resource.Instantiate("Background").GetComponent<SpriteRenderer>().sortingOrder = (int)Define.SortOrder.Backgound;
        Managers.Resource.Instantiate("Collider");

        yield return new WaitForSecondsRealtime(0.5f);


        while (!_compeleteLoad && Managers.Game.CurrentState != GameState.Play) //다른 컴이 로드가 끝날때까지 대기.
        {
            if (!PhotonNetwork.IsMasterClient)
                _compeleteLoad = true;
            yield return null;
        }

        Managers.Game.SetGameState(GameState.Play);
    }

    void NetworkLoad()
    {
        PlayerController[] pc = GameObject.FindObjectsOfType<PlayerController>();
        foreach(PlayerController player in pc)
        {
            Debug.Log("Player Search : " + player.ToString());
            PhotonView photonView = player.GetComponent<PhotonView>();
            if (!photonView.IsMine)
            {
                Managers.Game.Enemy = player;
                player.transform.position = new Vector3(0, Define.ENEMY_Y, 0);

                SpriteRenderer sr = player.GetComponent<SpriteRenderer>();
                sr.flipY = true;
                sr.color = new Color(1f,0.5f,0.5f,1f);
                break;
            }
        }
        Managers.UI.ClosePopupUI();
        UIGameScene = Managers.UI.ShowSceneUI<UI_GameScene>();
        Debug.Log("LoadStage 완");

    }


    public override void Clear()
    {

    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        Debug.Log("call" + stream.ToString());
        Debug.Log(info.ToString());

        if (stream.IsWriting)
        {
            stream.SendNext(_compeleteLoad);
            Debug.Log("Send : " + _compeleteLoad);

            //stream.SendNext((int)Managers.Game.CurrentState);
        }
        else
        {
            _compeleteLoad = (bool)stream.ReceiveNext();
            Debug.Log("Receive : "+ _compeleteLoad);
            //Managers.Game.CurrentState = (Define.GameState)((int)stream.ReceiveNext());

        }
    }
}
