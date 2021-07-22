using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;
    public int nextMove;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Invoke("Think", 4);
    }

    void FixedUpdate()
    {
        
        EnemyMoveLogic();

        //Platform Check
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.3f, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector2.down, 1, LayerMask.GetMask("Platform"));
        if (rayHit.collider == null)
        {
            Turn();
        }
    }

    //Move
    void EnemyMoveLogic()
    {
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);
    }

    void Think()
    {
        //Setting Next Active
        nextMove = Random.Range(-1, 2);//Move Range

        //Sprite Animation
        anim.SetInteger("WalkSpeed", nextMove);
        //Flip Sprite
        if(nextMove != 0)
        spriteRenderer.flipX = nextMove == 1;

        //Recursive 재귀함수는 함수 제일 아래쪽에 작성
        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", nextThinkTime);//재귀함수 = 함수가 자기자신을 호출하는 함수
    }

    void Turn()
    {
        nextMove *= -1;
        spriteRenderer.flipX = nextMove == 1;

        CancelInvoke();
        Invoke("Think", 4);
    }

}
