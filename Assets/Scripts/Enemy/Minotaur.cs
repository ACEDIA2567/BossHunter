using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class Minotaur : Monster
{
    private float _maxHp = 200000f;
    public float _curHp;
    public float _attackPower = 20f;
    public float _defensePower = 3f;
    private float _speed = 2f;

    private float attackCoolDown = 3f;
    private float coolDown;
    GameObject player;
    private Player playerComponent;
    private bool isMoving = true;
    private bool isAttacking = false;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private GameObject earthObject;
    [SerializeField] private GameObject slashHitBox;

    // 임시
    [SerializeField] private TextMeshProUGUI curHpText;


    private void Start()
    {
        maxHp = _maxHp;
        _curHp = maxHp;
        attackPower = _attackPower;
        defensePower = _defensePower;
        speed = _speed;
        currentHp = maxHp;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerComponent = player.GetComponent<Player>();
    }

    private void Update()
    {
        if(!isAttacking && defensePower == 0)
        {
            DestroyArmor();
        }
        if (!isAttacking)
        {
            Search();
        }
        coolDown += Time.deltaTime;
        if (player != null && coolDown >= attackCoolDown && !isAttacking)
        {
            coolDown = 0;
            DoAction();
        }
        // curHp == 0 -> 죽어라

        // 임시
        curHpText.text = _curHp.ToString("N0");
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
            SlashReady();
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

    // DestroyArmor
    private void DestroyArmor()
    {
        isAttacking = true;
        animator.SetBool("isDestroyArmor", true);
    }

    private void RepairArmor()
    {
        animator.SetBool("isDestroyArmor", false);
        animator.SetBool("isRepairArmor", true);
    }

    private void RepairArmorEnd()
    {
        defensePower = _defensePower;
        animator.SetBool("isRepairArmor", false);
        isAttacking = false;
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
            Transform blockHitBoxTransform = player.transform.Find("BlockHitBox");
            BoxCollider2D blockHitBoxCollider = blockHitBoxTransform.GetComponent<BoxCollider2D>();
            if (!blockHitBoxCollider.enabled)
            {
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
            else
            {
                Debug.Log("Blocked!");
            }
            RadeManager.Instance.DamageToPlayer(1.5f, playerComponent.isBlock);
        }
    }

    private void WindMillEnd()
    {
        isAttacking = false;
        animator.SetBool("isWindMill", false);
    }

    // Slash
    private void SlashReady()
    {
        isAttacking = true;
        animator.SetBool("isSlashReady", true);
    }

    private void Slash()
    {
        animator.SetBool("isSlashReady", false);
        animator.SetBool("isSlash", true);
    }

    private void SlashMove()
    {
        slashHitBox.SetActive(true);
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, (speed / 2));

    }

    private void SlashEnd()
    {
        isAttacking = false;
        animator.SetBool("isSlash", false);
    }
}

// Search 하고 일정 범위 밖이면 이동, Player로 부터 8 까지의 거리까지 도달하면 Stop