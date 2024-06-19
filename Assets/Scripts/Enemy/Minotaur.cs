using UnityEngine;

public class Minotaur : Monster
{
    private float _maxHp = 1000000f;
    private float _attackPower = 20f;
    private float _defensePower = 10f;
    private float _speed = 2f;

    private float attackCoolDown = 3f;
    private float coolDown;
    GameObject player;
    private bool isMoving = true;

    private Animator animator;
    private SpriteRenderer spriteRenderer;


    private void Start()
    {
        maxHp = _maxHp;
        attackPower = _attackPower;
        defensePower = _defensePower;
        speed = _speed;
        currentHp = maxHp;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        Search();
        SpriteFlip();
        coolDown += Time.deltaTime;
        if (player != null && coolDown >= attackCoolDown)
        {
            coolDown = 0;
            
        }
    }

    public override void DoAction()
    {
        base.DoAction();
        Search();
    }

    private void SpriteFlip()
    {
        if(player.transform.position.x > transform.position.x)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }

    private void Search()
    {
        isMoving = true;
        if(isMoving && player != gameObject)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if(distance > 8.0f)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
                animator.SetBool("isRun", true);
                distance = Vector3.Distance(transform.position, player.transform.position);

                if(distance <= 8.0f)
                {
                    isMoving = false;
                    animator.SetBool("isRun", false);
                }
            }
            else
            {
                isMoving = false;
                animator.SetBool("isRun", false);
            }
        }
        else
        {
            Debug.Log("8 이하");
        }
    }
}

// Search 하고 일정 범위 밖이면 이동, Player로 부터 8 까지의 거리까지 도달하면 Stop