using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    CoolTime _coolTime;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;
        BindObject(typeof(GameObjects));
        Refresh();
        return true;
    }

    void Refresh()
    {
        GetObject((int)GameObjects.Left).BindEvent(() => Managers.Game.Player.CurrentDirection = Define.Direction.Left);
        GetObject((int)GameObjects.Right).BindEvent(() => Managers.Game.Player.CurrentDirection = Define.Direction.Right);
        GetObject((int)GameObjects.Attack).BindEvent(attackEvent);
        _coolTime = GetObject((int)GameObjects.Attack).GetComponentInChildren<CoolTime>();

        if (Managers.Game.Player != null)
            Managers.Game.Player.HP_UI = GetObject((int)GameObjects.MyHP);
        else
            Debug.Log("Player is Null");

        if (Managers.Game.Enemy != null)
            Managers.Game.Enemy.HP_UI = GetObject((int)GameObjects.OpponentHP);
        else
            Debug.Log("Enemy is Null");
    }

    void attackEvent()
    {
        if(_coolTime.IsZero())
        {
            Managers.Game.Player.Attack();
            _coolTime.SetMaxValue();
        }
    }

    private void Awake()
    {
        Init();
    }


}
