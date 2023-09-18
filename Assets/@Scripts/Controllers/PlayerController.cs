using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Define.Direction CurrentDirection { get; set; }
    int _hit;
    void Awake()
    {
        init();
    }

    private void init()
    {
        CurrentDirection = Define.Direction.Stop;
        _hit = 0;
    }

    private void Update()
    {
        switch (CurrentDirection)
        {
            case Define.Direction.Left:
                transform.Translate(Vector3.left * Define.SPEED);
                break;
            case Define.Direction.Right:
                transform.Translate(Vector3.right * Define.SPEED);
                break;
            case Define.Direction.Stop:
                transform.Translate(Vector3.zero);
                break;
        }
    }

    public void Attack()
    {
        Debug.Log("공격");
        GameObject go = Managers.Resource.Instantiate("Bullet", gameObject.transform, true);
        go.transform.position = transform.position;
    }

    public void decreaseHP()
    {
        Debug.Log("HP : " + (3 - _hit).ToString());
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("충돌");
        CurrentDirection = Define.Direction.Stop;
    }

}
