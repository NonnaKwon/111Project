using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using static Define;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    UI_LobbyScene _lobby;
    UI_JoinRoom _room;
    bool _connect;
    void Awake()
    {
        init();
    }

    private void init()
    {
        Screen.SetResolution(960, 540, false);
        UnityEngine.Object.DontDestroyOnLoad(gameObject);
        _lobby = Managers.UI.ShowSceneUI<UI_LobbyScene>();
        _lobby.JoinBtn.BindEvent(() => PhotonNetwork.ConnectUsingSettings());
        _lobby.PlayerName.text = "Player";
        _connect = true;
    }



    public override void OnConnectedToMaster()
    {
        Debug.Log("Success Conneted");
        PhotonNetwork.LocalPlayer.NickName = _lobby.PlayerName.text;
        PhotonNetwork.JoinOrCreateRoom("방", new RoomOptions { MaxPlayers = 2 }, null);
    }

    public override void OnJoinedRoom()
    {
        if (!_connect)
            return;
        Debug.Log("방 참가 완료");
        _room = Managers.UI.ShowPopupUI<UI_JoinRoom>();
        RoomUpdate();
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        Debug.Log($"플레이어 {newPlayer.NickName} 가 방에 참가.");

        //룸 플레이어 업데이트
        RoomUpdate();
    }


    public override void OnCreatedRoom()
    {
        if (!_connect)
            return;
        Debug.Log("방 만들기 완료");
        _room = Managers.UI.ShowPopupUI<UI_JoinRoom>();
        RoomUpdate();
    }



    public void RoomUpdate()
    {
        _room.Player1.text = "Player1 : "+PhotonNetwork.PlayerList[0].NickName;
        if (PhotonNetwork.PlayerList.Length == 2)
        {
             _room.Player2.text = "Player2 : " + PhotonNetwork.PlayerList[1].NickName;
        }
            _room.StartBtn.BindEvent(() => Managers.Game.StartGame());
    }


   
    public override void OnCreateRoomFailed(short returnCode,string message)
    {
        Debug.Log("방 만들기 실패");
        Debug.Log(message);
        Debug.Log(returnCode);
    }


    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("방 참가 실패");
        Debug.Log(message);
        Debug.Log(returnCode);
    }


}
