using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class UI_Loading : UI_Popup
{

    private void Start()
    {
        gameObject.GetComponent<Canvas>().sortingOrder = (int)Define.SortOrder.LoadingCanvas;
    }

}
