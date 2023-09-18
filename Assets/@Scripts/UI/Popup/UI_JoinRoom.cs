using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_JoinRoom : UI_Popup
{
    #region Enum
    enum GameObjects
    {
        StartButton
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
    public TMP_Text Player1 { get { return GetText((int)Texts.Player1); } }
    public TMP_Text Player2 { get { return GetText((int)Texts.Player2); } }

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
