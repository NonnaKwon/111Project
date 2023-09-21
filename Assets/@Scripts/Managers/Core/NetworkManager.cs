using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using static Define;
using ExitGames.Client.Photon;
using Photon.Pun.Demo.PunBasics;
using System.Runtime.InteropServices;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    private string gameVersion = "1";

    public UI_LobbyScene Lobby { set 
        {
            _lobby = value;
            _lobby.PlayerName.text = "Player";
        } 
    }

    int RoomCount = 0;
    UI_LobbyScene _lobby;
    UI_JoinRoom _room;
    bool _connect;
    void Awake()
    {
        init();
    }

    private void init()
    {
        Screen.SetResolution(540, 960, false);
        UnityEngine.Object.DontDestroyOnLoad(gameObject);
        _connect = true;
        PhotonNetwork.AutomaticallySyncScene = true;
        Managers.Resource.NetworkLoadAll();
        PhotonNetwork.ConnectUsingSettings();
    }



    public override void OnConnectedToMaster()
    {
        Debug.Log("Success Conneted!");
        _lobby.ConnectState.text = "연결완료!";
        _lobby.JoinBtn.BindEvent(() =>
        {
            PhotonNetwork.LocalPlayer.NickName = _lobby.PlayerName.text;
            Debug.Log(RoomCount);
            PhotonNetwork.JoinOrCreateRoom("방", new RoomOptions { MaxPlayers = 2 }, null);
        });
    }

    public override void OnJoinedRoom()
    {
        if (!_connect)
            return;
        Debug.Log("방 참가 완료");
        _room = Managers.UI.ShowPopupUI<UI_JoinRoom>();
        RoomCount++;
        RoomUpdate();
    }

    public override void OnCreatedRoom()
    {
        if (!_connect)
            return;
        Debug.Log("방 만들기 완료");
        _room = Managers.UI.ShowPopupUI<UI_JoinRoom>();
        RoomCount++;
        RoomUpdate();
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        Debug.Log($"플레이어 {newPlayer.NickName} 가 방에 참가.");

        //룸 플레이어 업데이트
        RoomUpdate();
    }


    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log($"플레이어 {otherPlayer.NickName} 가 방에서 나감.");

        //룸 플레이어 업데이트
        RoomUpdate();
    }



    public void RoomUpdate()
    {
        _room.Player1.text = "Player1 : "+PhotonNetwork.PlayerList[0].NickName+"(방장)";
        if (PhotonNetwork.PlayerList.Length == 2) //플레이어 수 2명일때 버튼 활성화
        {
             _room.Player2.text = "Player2 : " + PhotonNetwork.PlayerList[1].NickName;
             _room.StartBtn.BindEvent(Managers.Game.StartGame);
        }
    }

    public void StartGame()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            Managers.Clear();
            PhotonNetwork.LoadLevel(GetSceneName(Define.Scene.GameScene));
        }

    }

    string GetSceneName(Define.Scene type)
    {
        string name = System.Enum.GetName(typeof(Define.Scene), type);
        return name;
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
