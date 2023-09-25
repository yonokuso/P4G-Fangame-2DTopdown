using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAction : MonoBehaviour
{
    public GameManager gameManager;
    public float Speed;
    public AudioClip audioDamaged;
    public AudioClip audioItem;
    public AudioClip audioDie;
    public AudioClip audioFinish;


    float h;
    float v;
    bool isHorizonMove;
    bool isHpZero = true;
    Vector3 dirVec;
    Vector2 moveVec;
    GameObject scanObject;

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;
    AudioSource audioSource;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Move Value
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        // Check button Down & Up
        bool hDown = Input.GetButtonDown("Horizontal");
        bool vDown = Input.GetButtonDown("Vertical");
        bool hUp = Input.GetButtonUp("Horizontal");
        bool vUp = Input.GetButtonUp("Vertical");

        if (UIManager.instance.HPgage.value > 0.0f)
        {
            
            // Check Horizontal Move
            if (hDown)
                isHorizonMove = true;
            else if (vDown)
                isHorizonMove = false;
            else if (hUp || vUp)
                isHorizonMove = h != 0;

            // Direction
            if (vDown && v == 1)
                dirVec = Vector3.up;
            if (vDown && v == -1)
                dirVec = Vector3.down;
            if (hDown && h == 1)
                dirVec = Vector3.right;
            if (hDown && h == -1)
                dirVec = Vector3.left;
        }
        else
        {
            h = 0;
            v = 0;
            spriteRenderer.color = new Color(1, 1, 1, 0.4f);
            if (isHpZero)
            {
                OnDie();
            }
            isHpZero = false;
        }
    }

    void FixedUpdate()
    {
        // Move
        moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);
        rigid.velocity = moveVec * Speed;


        // Ray
        Debug.DrawRay(rigid.position, dirVec * 1.5f, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 0.7f, LayerMask.GetMask("Object"));

        if (rayHit.collider != null)
            scanObject = rayHit.collider.gameObject;
        else
            scanObject = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            bool isEnemy = collision.gameObject.name.Contains("Adachi");
            if (isEnemy)
            {
                UIManager.instance.HPgage.value -= 40.0f;
                OnDamaged(collision.transform.position);
                // 몬스터 맞았을 때 튕기는 모션, 임시
                rigid.AddForce(moveVec * -7, ForceMode2D.Impulse); 
                PlaySound("DAMAGED");
            }
        }

        if (collision.gameObject.tag == "Cabbage")
        {
            bool isCabbage = collision.gameObject.name.Contains("Cabbage");
            if (isCabbage)
            {
                UIManager.instance.HPgage.value += 30.0f;
                PlaySound("ITEM");
            }
        }

        if (collision.gameObject.tag == "Finish")
        {
            gameManager.NextStage();
            PlayerReposition();
            PlaySound("FINISH");
        }
    }

    public void PlayerReposition()
    {
        if (gameObject.name == "Player")
        {
            transform.position = new Vector3(0, 0, -5);
            rigid.velocity = Vector2.zero;
        }
    }


    void OnDamaged(Vector2 targetPos)
    {
        // Change Layer (Immortal Avtive)
        gameObject.layer = 11;  // 11 = layer PlayerDamaged

        // View Alpha
        spriteRenderer.color = new Color(1, 1, 1, 0.4f); // 0.4f = opacity

        // Reaction Force ***이부분 작동 안함
        //int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        //rigid.AddForce(moveVec * -7, ForceMode2D.Impulse);

        Invoke("OffDamaged", 1.5f);
    }

    void OffDamaged()
    {
        gameObject.layer = 10;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    void OnDie()
    {
        transform.rotation = Quaternion.Euler(0, 0, -90); // 잘될까?
        PlaySound("DIE");
    }

    void PlaySound(string Action)
    {
        switch (Action)
        {
            case "DAMAGED":
                audioSource.clip = audioDamaged;
                audioSource.Play();
                break;
            case "ITEM":
                audioSource.clip = audioItem;
                audioSource.Play();
                break;
            case "DIE":
                audioSource.clip = audioDie;
                audioSource.Play();
                break;
            case "FINISH":
                audioSource.clip = audioFinish;
                audioSource.Play();
                break;
        }
    }

}