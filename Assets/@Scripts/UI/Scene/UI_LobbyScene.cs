using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_LobbyScene : UI_Scene
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
    }
    #endregion


    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindObject(typeof(GameObjects));

        GetObject((int)GameObjects.StartButton).BindEvent(() => {
            Managers.Game.StartGame();
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
