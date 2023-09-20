using Data;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

public class LobbyScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.LobbyScene;

        GameObject go = GameObject.Find("@Network");
        if (go == null)
        {
            go = new GameObject("@Network");
            go.AddComponent<NetworkManager>();
        }
        go.GetComponent<NetworkManager>().Lobby = Managers.UI.ShowSceneUI<UI_LobbyScene>();

    }

    public override void Clear()
    {

    }

}
