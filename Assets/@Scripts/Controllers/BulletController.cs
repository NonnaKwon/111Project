using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviourPun, IPunObservable
{
    Rigidbody2D _rigidbody;
    PhotonView _pv;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _pv = GetComponent<PhotonView>();        
    }

    private void FixedUpdate()
    {
        if(_pv.IsMine)
            _rigidbody.MovePosition(_rigidbody.position + Vector2.up * Define.BULLET_SPEED);
        else
            _rigidbody.MovePosition(_rigidbody.position + Vector2.down * Define.BULLET_SPEED);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController pc = collision.gameObject.GetComponent<PlayerController>();
        if(pc != null)
        {
            if(pc.gameObject.layer != (int)Define.Layer.Player)
                pc.decreaseHP();
        }

        Managers.Resource.Destroy(this.gameObject);
    }

    [PunRPC]
    public void InitPos(Vector3 position)
    {
        //if (!PhotonNetwork.IsMasterClient)
        //    return;
        gameObject.transform.position = position;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }
}
