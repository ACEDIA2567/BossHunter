using System.Collections;
using System.Threading;
using System.Threading.Tasks;
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
    private bool isAttacking = false;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private GameObject earthObject;


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
        coolDown += Time.deltaTime;
        if (player != null && coolDown >= attackCoolDown && !isAttacking)
        {
            coolDown = 0;
            DoAction();
        }
    }

    public override void DoAction()
    {
        int randint = Random.Range(0, 3);
        base.DoAction();
        if (randint == 0)
        {
            WindMillReady();
        }
        else if (randint == 1)
        {
            EarthCrashReady();
        }
        else if(randint == 2)
        {
            Slash();
        }
        else
        {
            Debug.Log("뭔가 잘못됨");
        }
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
        SpriteFlip();
        if (isMoving && player != gameObject)
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

    // EarthCrash
    private void EarthCrashReady()
    {
        isAttacking = true;
        animator.SetBool("isEarthCrashReady", true);
    }

    private void EarthCrash()
    {
        animator.SetBool("isEarthCrashReady", false);
        animator.SetBool("isEarthCrash", true);
        StartCoroutine(EarthObjectCoroutine());
    }

    private IEnumerator EarthObjectCoroutine()
    {
        Vector3 earthPosition = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);
        Vector3 forwardEarthPosition = earthPosition;
        Vector3 backEarthPosition = earthPosition;
        Vector3 addPosition = new Vector3(2.5f, 0, 0);
        for (int i = 0; i < 5; i++)
        {
            forwardEarthPosition += addPosition;
            backEarthPosition -= addPosition;
            GameObject fo = Instantiate(earthObject, transform);
            fo.transform.position = forwardEarthPosition;
            GameObject bo = Instantiate(earthObject, transform);
            bo.transform.position = backEarthPosition;

            yield return new WaitForSeconds(0.3f);
        }
    }

    private void EarthCrashEnd()
    {
        isAttacking = false;
        animator.SetBool("isEarthCrash", false);
    }

    //WindMill
    private void WindMillReady()
    {
        isAttacking = true;
        animator.SetBool("isWindMillReady", true);
    }

    private void WindMill()
    {
        animator.SetBool("isWindMillReady", false);
        animator.SetBool("isWindMill", true);
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance < 4)
        {
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            Vector2 knockbackForce;
            if (spriteRenderer.flipX)
            {
                knockbackForce = new Vector2(-10f, 5f);
            }
            else
            {
                knockbackForce = new Vector2(10f, 5f);
            }
            rb.AddForce(knockbackForce, ForceMode2D.Impulse);
        }
    }

    private void WindMillEnd()
    {
        isAttacking = false;
        animator.SetBool("isWindMill", false);
    }

    // Slash
    private void Slash()
    {
        isAttacking = true;
        animator.SetBool("isSlash", true);
        // TODO: 피격 기능
    }

    private void SlashMove()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, (speed));
    }

    private void SlashEnd()
    {
        isAttacking = false;
        animator.SetBool("isSlash", false);
    }
}

// Search 하고 일정 범위 밖이면 이동, Player로 부터 8 까지의 거리까지 도달하면 Stop