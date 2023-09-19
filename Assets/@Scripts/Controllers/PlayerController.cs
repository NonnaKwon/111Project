using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviourPun, IPunObservable
{
    public Define.Direction CurrentDirection { get; set; }
    public GameObject HP_UI;

    GameObject[] _hearts;
    Rigidbody2D _rigidbody;
    int _damage;
    PhotonView _pv;

    void Awake()
    {
        init();
    }

    private void init()
    {
        CurrentDirection = Define.Direction.Stop;
        _rigidbody = GetComponent<Rigidbody2D>();
        _damage = 0;
        _pv = GetComponent<PhotonView>();
    }


    private void FixedUpdate()
    {
        if (_pv.IsMine)
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
        else
        {
            switch (CurrentDirection)
            {
                case Define.Direction.Left:
                    _rigidbody.MovePosition(_rigidbody.position + Vector2.right * Define.SPEED);
                    break;
                case Define.Direction.Right:
                    _rigidbody.MovePosition(_rigidbody.position + Vector2.left * Define.SPEED);
                    break;
                case Define.Direction.Stop:
                    _rigidbody.MovePosition(_rigidbody.position);
                    break;
            }

        }
    }

    [PunRPC]
    public void Attack()
    {
        Debug.Log("АјАн");
        GameObject go = Managers.Resource.NetworkInstantiate("Bullet", transform.position,gameObject.transform);
        //GameObject go = Managers.Resource.NetworkInstantiate("Bullet", gameObject.transform, true);
        if (_pv.IsMine)
            _pv.RPC("Attack", RpcTarget.All);

    }


    [PunRPC]
    public void decreaseHP()
    {
        //if (!PhotonNetwork.IsMasterClient)
        //    return;

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

        if(_pv.IsMine)
            _pv.RPC("decreaseHP", RpcTarget.All);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        CurrentDirection = Define.Direction.Stop;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (Managers.Game.CurrentState != Define.GameState.Play)
            return;
        if(stream.IsWriting)
        {
            stream.SendNext(CurrentDirection);
        }
        else
        {
            Managers.Game.Enemy.CurrentDirection = (Define.Direction)stream.ReceiveNext();
        }
    }
}
