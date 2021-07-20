using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed;
    public float jumpPower;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        //Stop Speed
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }

        DirectionChange();
        WalkAnim();
        Jump();
    }

    void FixedUpdate()
    {
        //Move By Key Control
        float h = Input.GetAxisRaw("Horizontal");

        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rigid.velocity.x > maxSpeed)//right max speed
        {
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }else if(rigid.velocity.x < maxSpeed*(-1))// left max speed
        {
            rigid.velocity = new Vector2(maxSpeed*(-1), rigid.velocity.y);
        }
    }

    void DirectionChange()
    {
        if (Input.GetButton("Horizontal")) { 
        spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }
    }

    void WalkAnim()
    {
        if(Mathf.Abs(rigid.velocity.x) < 0.3)
        {
            anim.SetBool("isWalking", false);
        }else
        {
            anim.SetBool("isWalking", true);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }
}
