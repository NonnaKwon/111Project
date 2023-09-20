using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    public BaseScene CurrentScene { get { return GameObject.FindObjectOfType<BaseScene>(); } }

    public void LoadScene(Define.Scene type, Transform parents = null)
    {
        Managers.Clear();
        if(PhotonNetwork.IsMasterClient)
        {
            PhotonView pv = GameObject.FindObjectOfType<BaseScene>().GetComponent<PhotonView>();
            if (pv != null)
            {
                pv.RPC("PunLoadScene", RpcTarget.All);
            }
            PhotonNetwork.LoadLevel(GetSceneName(type));
        }
    }

    public void LoadScenePersonal(Define.Scene type, Transform parents = null)
    {
        Managers.Clear();
        PhotonNetwork.LoadLevel(GetSceneName(type));
    }

    string GetSceneName(Define.Scene type)
    {
        string name = System.Enum.GetName(typeof(Define.Scene), type);
        return name;
    }

    public void SimpleLoadScene(Define.Scene type, Transform parents = null)
    {
        SceneManager.LoadScene(GetSceneName(type));
    }

    public void Clear()
    {
        CurrentScene.Clear();
    }
}
