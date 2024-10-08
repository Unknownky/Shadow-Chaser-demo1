using System.Data;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// ����è��̬
/// </summary>

public class CatController : MonoBehaviour, IStateController    
{
    [Header("PlayerComponent")]
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D playerBody2D;

    [Header("Ground Detected")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float DetectRadius;

    [SerializeField] private bool isOnGround;

    [Header("PlayerAttributes")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float backGroundScale; //��Ӹóߴ�����֮�����Ļ���䣬�Լ��ٶȵĵ���
    [SerializeField] private float jumpDecrease;
    [SerializeField] private float commonGravityScale = 2f;
    [SerializeField] private float fallingGravityScale = 4f;
    [SerializeField] private float rushAccelerate = 2.3f;


    [Header("StateComponent")]
    [SerializeField] private GameObject statesContainer;


    //static PlayerController instance;
    private float horizontal;
    private bool isFacingRight;
    public float IdleTime;
    public float StretchingTime = 3f;
    public float LickingTime = 6f;
    public float Sleep1Time = 8f;
    private bool canStretching = true;
    private bool canLicking = true;
    private bool canSleep1 = true;
    private bool isIdle = true;
    private bool canRecordOnGroundHorizontalPosition;

    private GameObject virtualGroundGameobject;

    private PlatformEffector2D virtualPlatformEffector2D;

    private void Awake()//����ģʽ
    {
        //if (instance != null)
        //    Destroy(this);
        //instance = this;
        InitParameters();
    }

    public void InitParameters()
    {
        statesContainer = GameObject.Find("BagCanvas").transform.GetChild(0).gameObject;
        IdleTime = 0f;
        isFacingRight = true;
        canRecordOnGroundHorizontalPosition = true;
    }

    private void Update()
    {
        StatesChangePannelCheck();
        Movement();//�����ƶ�
        Flip();//��ת��
    }

    private void StatesChangePannelCheck()
    {
        bool key = Input.GetKey(KeyCode.Tab);
        if (key)
        {
            statesContainer?.SetActive(true);
        }
        else
            statesContainer?.SetActive(false);
    }

    public void Movement()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump") && isOnGround)
        {
            playerBody2D.velocity = new Vector2(playerBody2D.velocity.x, jumpPower * backGroundScale);
        }
        if (Input.GetButtonUp("Jump") && playerBody2D.velocity.y > 0f)
        {
            playerBody2D.velocity = new Vector2(playerBody2D.velocity.x, playerBody2D.velocity.y * jumpDecrease);
        }
        if (Input.GetButton("Rush") && isOnGround && playerBody2D.velocity.x != 0f)
        {
            horizontal *= rushAccelerate;
        }
        if (Input.GetButtonDown("Down") && isOnGround)
        {
            //��ʱ�ر�PlatformEffector2D
            if (virtualGroundGameobject != null)
            {
                virtualPlatformEffector2D.rotationalOffset = 180f;
            }
        }
    }
    public void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    private void FixedUpdate()
    {
        //ʱ�����
        IdleParametersUpdate();

        //��������
        AnimatorUpdate();

        //�������
        PhysicalUpdate();
    }

    void IdleParametersUpdate()
    {
        if (playerBody2D.velocity == Vector2.zero)
        {
            isIdle = true;
            IdleTime += Time.fixedDeltaTime;
        }
        else//�ƶ��Ļ�����boolֵ
        {
            canStretching = true;
            canLicking = true;
            canSleep1 = true;
            isIdle = false;
            IdleTime = 0f;
        }
    }



    public void AnimatorUpdate()
    {
        if (IdleTime >= StretchingTime && canStretching)
        {
            _animator.Play("Stretching");
            canStretching = false;
        }

        if (IdleTime >= LickingTime && canLicking)
        {
            _animator.Play("Licking");
            canLicking = false;
        }

        if (IdleTime >= Sleep1Time && canSleep1)
        {
            _animator.Play("Sleep1");
            canSleep1 = false;
        }

        _animator.SetBool("isOnGround", isOnGround);
        _animator.SetFloat("Speed", Mathf.Abs(playerBody2D.velocity.x));
        _animator.SetFloat("Yspeed", Mathf.Abs(playerBody2D.velocity.y));
        _animator.SetFloat("IdleTime", IdleTime);
        _animator.SetBool("isIdle", isIdle);
    }

    public void PhysicalUpdate()
    {
        isOnGrounded();
        if (playerBody2D.velocity.y <= 0f)
            playerBody2D.gravityScale = fallingGravityScale;
        else
            playerBody2D.gravityScale = commonGravityScale;
        playerBody2D.velocity = new Vector2(horizontal * speed * Time.fixedDeltaTime * backGroundScale, playerBody2D.velocity.y);

        if (isOnGround && canRecordOnGroundHorizontalPosition)
        {
            playerBody2D.transform.position = new Vector3(playerBody2D.transform.position.x, playerBody2D.transform.position.y - DetectRadius / 2f, 1);
            canRecordOnGroundHorizontalPosition = false;
            GetVirtualGroundGameobject();
        }
        if (!isOnGround)
        {
            canRecordOnGroundHorizontalPosition = true;
        }
    }

    private void GetVirtualGroundGameobject()
    {
        if (virtualGroundGameobject != null) //��ԭԭ����PlatformEffector2D
        {
            if (virtualGroundGameobject.CompareTag("VirtualPlatform"))
            {
                virtualPlatformEffector2D.rotationalOffset = 0f;
            }
        }
        Collider2D collider = Physics2D.OverlapCircle(groundCheck.position, DetectRadius, groundLayer);
        if (collider != null)
        {
            virtualGroundGameobject = collider.gameObject;
            if (virtualGroundGameobject.CompareTag("VirtualPlatform"))
            {
                virtualPlatformEffector2D = virtualGroundGameobject.GetComponent<PlatformEffector2D>();
                return;
            }
        }
        virtualGroundGameobject = null;
    }

    public bool isOnGrounded()
    {
        isOnGround = Physics2D.OverlapCircle(groundCheck.position, DetectRadius, groundLayer);
        return isOnGround;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, DetectRadius);
    }
}
