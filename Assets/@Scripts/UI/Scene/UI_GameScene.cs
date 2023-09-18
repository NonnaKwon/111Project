using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_GameScene : UI_Scene
{
    #region Enum
    enum GameObjects
    {
        Left,
        Attack,
        Right,
        OpponentHP,
        MyHP
    }
    #endregion

    public override bool Init()
    {
        if (base.Init() == false)
            return false;
        BindObject(typeof(GameObjects));
        GetObject((int)GameObjects.Left).BindEvent(() => Managers.Game.Player.CurrentDirection = Define.Direction.Left);
        GetObject((int)GameObjects.Right).BindEvent(() => Managers.Game.Player.CurrentDirection = Define.Direction.Right);
        GetObject((int)GameObjects.Attack).BindEvent(() => Managers.Game.Player.Attack());
        return true;
    }

    private void Awake()
    {
        Init();
    }


}
