using UnityEngine;
using System.Collections;
using TMPro;

public class Player : MonoBehaviour
{

    [SerializeField] float m_speed = 4.0f;
    [SerializeField] float m_jumpForce = 7.5f;
    [SerializeField] float m_rollForce = 6.0f;
    [SerializeField] bool m_noBlood = false;
    
    private Animator m_animator;
    private Rigidbody2D m_body2d;
    private Sensor_HeroKnight m_groundSensor;
    private Sensor_HeroKnight m_wallSensorR1;
    private Sensor_HeroKnight m_wallSensorR2;
    private Sensor_HeroKnight m_wallSensorL1;
    private Sensor_HeroKnight m_wallSensorL2;

    private GameObject weaponHitBox;
    private BoxCollider2D weaponHitBoxCollider;
    private Vector3 weaponHitBoxRightPos;
    private Vector3 weaponHitBoxLeftPos;

    private GameObject blockHitBox;
    private BoxCollider2D blockHitBoxCollider;
    private Vector3 blockHitBoxRightPos;
    private Vector3 blockHitBoxLeftPos;

    private bool m_isWallSliding = false;
    private bool m_grounded = false;
    private bool m_rolling = false;
    private int m_facingDirection = 1;
    private int m_currentAttack = 0;
    private float m_timeSinceAttack = 0.0f;
    private float m_delayToIdle = 0.0f;
    private float m_rollDuration = 8.0f / 14.0f;
    private float m_rollCurrentTime;

    public float hp = 100.0f;
    private bool isDead = false;

    // SH Task
    public float m_attackPower = 3000.0f;
    public bool isBlock = false;
    public float m_blockKeepTime;
    [SerializeField] private TextMeshProUGUI curHpText;

    // Use this for initialization
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_HeroKnight>();
        m_wallSensorR1 = transform.Find("WallSensor_R1").GetComponent<Sensor_HeroKnight>();
        m_wallSensorR2 = transform.Find("WallSensor_R2").GetComponent<Sensor_HeroKnight>();
        m_wallSensorL1 = transform.Find("WallSensor_L1").GetComponent<Sensor_HeroKnight>();
        m_wallSensorL2 = transform.Find("WallSensor_L2").GetComponent<Sensor_HeroKnight>();

        weaponHitBox = GameObject.Find("WeaponHitBox");
        weaponHitBoxCollider = weaponHitBox.GetComponent<BoxCollider2D>();
        weaponHitBoxCollider.enabled = false;
        weaponHitBoxRightPos = weaponHitBox.transform.localPosition;
        weaponHitBoxLeftPos = new Vector3(-weaponHitBoxRightPos.x, weaponHitBoxRightPos.y, weaponHitBoxRightPos.z);

        blockHitBox = GameObject.Find("BlockHitBox");
        blockHitBoxCollider = blockHitBox.GetComponent<BoxCollider2D>();
        blockHitBoxCollider.enabled = false;
        blockHitBoxRightPos = blockHitBox.transform.localPosition;
        blockHitBoxLeftPos = new Vector3(-blockHitBoxRightPos.x, blockHitBoxRightPos.y, blockHitBoxRightPos.z);

        isBlock = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Increase timer that controls attack combo
        m_timeSinceAttack += Time.deltaTime;
        if(weaponHitBoxCollider.enabled && m_timeSinceAttack > 0.1f)
            weaponHitBoxCollider.enabled = false;

        // Increase timer that checks roll duration
        if (m_rolling)
            m_rollCurrentTime += Time.deltaTime;

        // Disable rolling if timer extends duration
        if (m_rollCurrentTime > m_rollDuration)
            m_rolling = false;

        //Check if character just landed on the ground
        if (!m_grounded && m_groundSensor.State())
        {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
        }

        //Check if character just started falling
        if (m_grounded && !m_groundSensor.State())
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }

        // Check player HP
        if (hp <= 0 && !isDead)
        {
            isDead = true;

            Invoke("DeathAnimation", 0.1f);

            //////////////////////////////
            // TODO: GAME END UI

            //////////////////////////////
        }


        // -- Handle input and movement --
        float inputX = Input.GetAxis("Horizontal");
        if(!isDead) HandleInputandMovement(inputX);

        //Set AirSpeed in animator
        m_animator.SetFloat("AirSpeedY", m_body2d.velocity.y);

        // -- Handle Animations --
        if (!isDead) HandleAnimation(inputX);

        // �ӽ�
        curHpText.text = hp.ToString("N0");
        if (isBlock)
        {
            m_blockKeepTime += Time.deltaTime;
        }
        else if (!isBlock)
        {
            m_blockKeepTime = 0.0f;
        }
    }

    private void HandleInputandMovement(float inputX)
    {
        // Swap direction of sprite depending on walk direction
        if (inputX > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            m_facingDirection = 1;
            weaponHitBox.transform.localPosition = weaponHitBoxRightPos;
            blockHitBox.transform.localPosition = blockHitBoxRightPos;
        }

        else if (inputX < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            m_facingDirection = -1;
            weaponHitBox.transform.localPosition = weaponHitBoxLeftPos;
            blockHitBox.transform.localPosition = blockHitBoxLeftPos;
        }

        // Move
        if (!m_rolling)
            m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);
    }

    private void HandleAnimation(float inputX)
    {
        /*
        //Death
        if (Input.GetKeyDown("e") && !m_rolling)
        {
            m_animator.SetBool("noBlood", m_noBlood);
            m_animator.SetTrigger("Death");
        }

        //Hurt
        else if (Input.GetKeyDown("q") && !m_rolling)
            m_animator.SetTrigger("Hurt");
        */

        //Attack
        if (Input.GetMouseButtonDown(0) && m_timeSinceAttack > 0.25f && !m_rolling)
        {
            weaponHitBoxCollider.enabled = true;

            blockHitBoxCollider.enabled = false;

            m_animator.SetBool("IdleBlock", false);

            m_currentAttack++;

            // Loop back to one after third attack
            if (m_currentAttack > 3)
                m_currentAttack = 1;

            // Reset Attack combo if time since last attack is too large
            if (m_timeSinceAttack > 1.0f)
                m_currentAttack = 1;

            // Call one of three attack animations "Attack1", "Attack2", "Attack3"
            m_animator.SetTrigger("Attack" + m_currentAttack);

            // Reset timer
            m_timeSinceAttack = 0.0f;
        }

        // Block
        else if (Input.GetMouseButtonDown(1) && !m_rolling)
        {
            blockHitBoxCollider.enabled = true;
            isBlock = true;
            m_animator.SetTrigger("Block");
            m_animator.SetBool("IdleBlock", true);
        }

        else if (Input.GetMouseButtonUp(1))
        {
            blockHitBoxCollider.enabled = false;
            isBlock = false;
            m_animator.SetBool("IdleBlock", false);
        }

        // Roll
        else if (Input.GetKeyDown("left shift") && !m_rolling && !m_isWallSliding)
        {
            m_rolling = true;
            m_animator.SetTrigger("Roll");
            m_body2d.velocity = new Vector2(m_facingDirection * m_rollForce, m_body2d.velocity.y);
        }


        //Jump
        else if (Input.GetKeyDown("space") && m_grounded && !m_rolling)
        {
            m_animator.SetTrigger("Jump");
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
            m_groundSensor.Disable(0.2f);
        }

        //Run
        else if (Mathf.Abs(inputX) > Mathf.Epsilon)
        {
            // Reset timer
            m_delayToIdle = 0.05f;
            m_animator.SetInteger("AnimState", 1);
        }

        //Idle
        else
        {
            // Prevents flickering transitions to idle
            m_delayToIdle -= Time.deltaTime;
            if (m_delayToIdle < 0)
                m_animator.SetInteger("AnimState", 0);
        }
    }

    public void HurtAnimation()
    {
        if (!m_rolling)
            m_animator.SetTrigger("Hurt");
    }

    private void DeathAnimation()
    {
        m_animator.SetBool("noBlood", m_noBlood);
        m_animator.SetTrigger("Death");
    }
}
