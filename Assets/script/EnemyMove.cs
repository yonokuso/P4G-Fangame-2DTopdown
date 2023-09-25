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
            //Debug.Log("���� �ν��߽��ϴ�.");
            //scanObject = rayHit.collider.gameObject;

            if (nextmoveh == 1 || nextmoveh == -1)
            {
                nextmoveh *= -1;
                //Debug.Log("������ (��/������)���� �ٲ���ϴ�.");

            }
            else if (nextmovev == 1 || nextmovev == -1)
            {
                //Debug.Log("�� �Ǵ� �Ʒ� ��");
                nextmovev *= -1;
                //Debug.Log("������ (��/�Ʒ�)�� �ٲ���ϴ�.");

            }
            else if(nextmoveh == 0)
            {
                nextmoveh = 1;
                //Debug.Log("0���� 1�� ����:h");
            }
            else if (nextmovev == 0)
            {
                nextmovev = 1;
                //Debug.Log("0���� 1�� ����:v");

            }
            //CancelInvoke(); // ���� nextThinkTime �� ���?
            //Invoke("Think", 1);
            //Debug.Log("�� ��ٸ� ����.");
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
                //Debug.Log("�� �Ǵ� ������ ��");
                nextmoveh *= -1;
                //Debug.Log("ĳ���͸� (��/������)���� �ٲ���ϴ�.");

            }
            else if (nextmovev == 1 || nextmovev == -1)
            {
                //Debug.Log("�� �Ǵ� �Ʒ� ��");
                nextmovev *= -1;
                //Debug.Log("ĳ���͸� (��/�Ʒ�)�� �ٲ���ϴ�.");

            }
            else if (nextmoveh == 0)
            {
                nextmoveh = 1;
                //Debug.Log("ĳ���� 0���� 1�� ����:h");
            }
            else if (nextmovev == 0)
            {
                nextmovev = 1;
                //Debug.Log("ĳ���� 0���� 1�� ����:v");

            }
        }
    }
    
}
