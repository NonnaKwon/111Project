using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_GameResult : UI_Popup
{
    #region Enum
    enum Texts
    {
        Result
    }
    #endregion

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindText(typeof(Texts));
        Refresh();
        return true;
    }

    void Refresh()
    {
        gameObject.GetComponent<Canvas>().sortingOrder = (int)Define.SortOrder.LoadingCanvas;

        if (Managers.Game.GameResult[0] == 'E')//���� ������
            GetText((int)Texts.Result).text = "You Win!";
        else //���� ������
            GetText((int)Texts.Result).text = "You Lose..";
    }

    private void Awake()
    {
        Init();
    }
}
