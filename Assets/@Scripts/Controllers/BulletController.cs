using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        Debug.Log(_rigidbody);
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.up * Define.BULLET_SPEED);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("공격성공");
        PlayerController pc = collision.gameObject.GetComponent<PlayerController>();
        if(pc != null)
        {
            pc.decreaseHP();
            Debug.Log(pc.gameObject.name);
        }
        else
        {
            Managers.Resource.Destroy(this.gameObject);
        }
    }

}
