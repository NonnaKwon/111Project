using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Define.Direction CurrentDirection { get; set; }
    public GameObject HP_UI;

    GameObject[] _hearts;
    Rigidbody2D _rigidbody;
    int _damage;
    void Awake()
    {
        init();
    }

    private void init()
    {
        CurrentDirection = Define.Direction.Stop;
        _rigidbody = GetComponent<Rigidbody2D>();
        _damage = 0;
    }

    //private void Update()
    //{
    //    switch (CurrentDirection)
    //    {
    //        case Define.Direction.Left:

    //            transform.Translate(Vector3.left * Define.SPEED);
    //            break;
    //        case Define.Direction.Right:
    //            transform.Translate(Vector3.right * Define.SPEED);
    //            break;
    //        case Define.Direction.Stop:
    //            transform.Translate(Vector3.zero);
    //            break;
    //    }
    //}

    private void FixedUpdate()
    {
        switch (CurrentDirection)
        {
            case Define.Direction.Left:
                _rigidbody.MovePosition(_rigidbody.position + Vector2.left * Define.SPEED);
                break;
            case Define.Direction.Right:
                _rigidbody.MovePosition(_rigidbody.position + Vector2.right * Define.SPEED);
                break;
            case Define.Direction.Stop:
                _rigidbody.MovePosition(_rigidbody.position);
                break;
        }
    }

    public void Attack()
    {
        Debug.Log("АјАн");
        GameObject go = Managers.Resource.Instantiate("Bullet", gameObject.transform, true);
        go.transform.position = transform.position;
    }


    public void decreaseHP()
    {
        if(_hearts == null)
        {
            _hearts =  new GameObject[3];
            GameObject grid = HP_UI.transform.GetChild(0).gameObject;
            for (int i = 0; i < 3; i++)
            {
                _hearts[i] = grid.transform.GetChild(i).gameObject;
            }
        }

        _hearts[_damage].SetActive(false);
        _damage++;
        if(_damage >= 3)
        {
            Debug.Log("Game over");
            Managers.Game.GameResult = gameObject.name + "Lose";
            Managers.Game.SetGameState(Define.GameState.Result);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        CurrentDirection = Define.Direction.Stop;
    }

}
