using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_LobbyScene : UI_Scene
{
    #region Enum
    enum GameObjects
    {
        JoinButton,
        PlayerName
    }

    enum Buttons
    {
    }


    enum Texts
    {
        ConnectState
    }
    #endregion


    public GameObject JoinBtn { get { return GetObject((int)GameObjects.JoinButton); } }
    public TMP_Text ConnectState { get { return GetText((int)Texts.ConnectState); } }
    public TMP_InputField PlayerName
    { get 
        { 
            return GetObject((int)GameObjects.PlayerName).GetComponent<TMP_InputField>(); 
        } 
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindObject(typeof(GameObjects));
        BindText(typeof(Texts));
        return true;
    }

    private void Awake()
    {
        Init();
    }
    private void Start()
    {

    }
}
