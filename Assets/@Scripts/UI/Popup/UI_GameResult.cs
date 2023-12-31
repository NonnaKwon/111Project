using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_GameResult : UI_Popup
{
    #region Enum
    enum GameObjects
    {
        GoLobby
    }
    enum Texts
    {
        Result
    }
    #endregion

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindText(typeof(Texts));
        BindObject(typeof(GameObjects));
        Refresh();
        return true;
    }

    void Refresh()
    {
        gameObject.GetComponent<Canvas>().sortingOrder = (int)Define.SortOrder.LoadingCanvas;
        GetObject((int)GameObjects.GoLobby).BindEvent(() =>
        {
            Managers.Scene.LoadScenePersonal(Define.Scene.LobbyScene);
            PhotonNetwork.LeaveRoom();
        });
        if (Managers.Game.GameResult[0] == 'E')//적이 졌으면
            GetText((int)Texts.Result).text = "You Win!";
        else //내가 졌으면
            GetText((int)Texts.Result).text = "You Lose..";
    }

    private void Awake()
    {
        Init();
    }
}
