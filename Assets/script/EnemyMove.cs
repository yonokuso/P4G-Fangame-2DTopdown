using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;
    public int nextmoveh;
    public int nextmovev;
    public float Speed;

    Vector3 dirVec;
    GameObject scanObject;
    GameObject scanEnemy;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();

        Invoke("Think", 1);
    }

    void FixedUpdate()
    {
        // Move
        rigid.velocity = dirVec * Speed;

        // Direction
        if (nextmovev == 1)
            dirVec = Vector3.up;
        if (nextmovev == -1)
            dirVec = Vector3.down;
        if (nextmoveh == 1)
            dirVec = Vector3.right;
        if (nextmoveh == -1)
            dirVec = Vector3.left;


        Debug.DrawRay(rigid.position, dirVec * 1.5f, new Color(0, 1, 0));        
        // BorderLine Check
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 0.7f, LayerMask.GetMask("BorderLine"));

        if (rayHit.collider != null)
        {
            //Debug.Log("벽을 인식했습니다.");
            //scanObject = rayHit.collider.gameObject;

            if (nextmoveh == 1 || nextmoveh == -1)
            {
                nextmoveh *= -1;
                //Debug.Log("방향을 (왼/오른쪽)으로 바꿨습니다.");

            }
            else if (nextmovev == 1 || nextmovev == -1)
            {
                //Debug.Log("위 또는 아래 벽");
                nextmovev *= -1;
                //Debug.Log("방향을 (위/아래)로 바꿨습니다.");

            }
            else if(nextmoveh == 0)
            {
                nextmoveh = 1;
                //Debug.Log("0에서 1로 변경:h");
            }
            else if (nextmovev == 0)
            {
                nextmovev = 1;
                //Debug.Log("0에서 1로 변경:v");

            }
            //CancelInvoke(); // 밑의 nextThinkTime 을 취소?
            //Invoke("Think", 1);
            //Debug.Log("긴 기다림 시작.");
        }
    }

    void Think()
    {
        // Random h,v
        nextmoveh = 2;
        nextmovev = 3;
        int choiceHV = Random.Range(2,4);

        if (choiceHV == 2)
        {
            nextmoveh = Random.Range(-1, 2);
        }
        else if (choiceHV == 3)
        { 
            nextmovev = Random.Range(-1, 2);
        }

        float nextThinktime = Random.Range(0.2f, 0.5f);
        Invoke("Think", nextThinktime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            if (nextmoveh == 1 || nextmoveh == -1)
            {
                //Debug.Log("왼 또는 오른쪽 벽");
                nextmoveh *= -1;
                //Debug.Log("캐릭터를 (왼/오른쪽)으로 바꿨습니다.");

            }
            else if (nextmovev == 1 || nextmovev == -1)
            {
                //Debug.Log("위 또는 아래 벽");
                nextmovev *= -1;
                //Debug.Log("캐릭터를 (위/아래)로 바꿨습니다.");

            }
            else if (nextmoveh == 0)
            {
                nextmoveh = 1;
                //Debug.Log("캐릭터 0에서 1로 변경:h");
            }
            else if (nextmovev == 0)
            {
                nextmovev = 1;
                //Debug.Log("캐릭터 0에서 1로 변경:v");

            }
        }
    }
    
}
