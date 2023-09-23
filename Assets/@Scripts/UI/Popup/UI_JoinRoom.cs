using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_JoinRoom : UI_Popup
{
    #region Enum
    enum GameObjects
    {
        StartButton,
        ExitButton
    }

    enum Buttons
    {
    }


    enum Texts
    {
        Player1,
        Player2
    }
    #endregion


    public GameObject StartBtn { get { return GetObject((int)GameObjects.StartButton); } }
    public GameObject ExitBtn { get { return GetObject((int)GameObjects.ExitButton); } }
    public TMP_Text Player1 { get { return GetText((int)Texts.Player1); } }
    public TMP_Text Player2 { get { return GetText((int)Texts.Player2); } }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindObject(typeof(GameObjects));
        BindText(typeof(Texts));
        GetObject((int)GameObjects.ExitButton).BindEvent(()=>
        {
            PhotonNetwork.LeaveRoom();
            Managers.UI.ClosePopupUI(this);
        });

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
