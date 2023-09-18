using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_GameScene : UI_Scene
{
    #region Enum
    enum GameObjects
    {
        JumpButton,
        AttackButton,
        ShieldButton,
        Heart1,
        Heart2,
        Heart3
    }

    
    enum Texts
    {
    }
    #endregion

    int life;
    public override bool Init()
    {
        if (base.Init() == false)
            return false;
        BindObject(typeof(GameObjects));
        return true;
    }

    private void Awake()
    {
        Init();
    }


}
