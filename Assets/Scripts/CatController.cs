using System.Data;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// 控制猫形态
/// </summary>

public class CatController : MonoBehaviour
{
    [Header("PlayerComponent")]
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D playerBody2D;

    [Header("Ground Detected")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float DetectRadius;

    [Header("PlayerAttributes")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float backGroundScale; //添加该尺寸用于之后的屏幕适配，以及速度的调整
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
    public float StretchingTime = 5f;
    public float LickingTime = 15f;
    public float Sleep1Time = 23f;
    private bool canStretching = true;
    private bool canLicking = true;
    private bool canSleep1 = true;
    private bool isIdle = true;
    private bool canRecordOnGroundHorizontalPosition;

    private void Awake()//单例模式
    {
        //if (instance != null)
        //    Destroy(this);
        //instance = this;
        InitParameters();
    }

    void InitParameters()
    {
        statesContainer = GameObject.Find("BagCanvas").transform.GetChild(0).gameObject;
        IdleTime = 0f;
        isFacingRight = true;
    }

    private void Update()
    {
        StatesChangePannelCheck();
        Movement();//调整移动
        Flip();//翻转脸
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

    private void Movement()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump") && isOnGrounded())
        {
            playerBody2D.velocity = new Vector2(playerBody2D.velocity.x, jumpPower * backGroundScale);
        }
        if (Input.GetButtonUp("Jump") && playerBody2D.velocity.y > 0f)
        {
            playerBody2D.velocity = new Vector2(playerBody2D.velocity.x, playerBody2D.velocity.y * jumpDecrease);
        }
        if (Input.GetButton("Fire3") && isOnGrounded() && playerBody2D.velocity.x != 0f)
        {
            horizontal *= rushAccelerate;
        }
    }
    private void Flip()
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
        //时间更新
        IdleParametersUpdate();

        //动画更新
        AnimatorUpdate();

        //物理更新
        PhysicalUpdate();
    }

    void IdleParametersUpdate()
    {
        if (playerBody2D.velocity == Vector2.zero)
        {
            isIdle = true;
            IdleTime += Time.fixedDeltaTime;
        }
        else//移动的话更新bool值
        {
            canStretching = true;
            canLicking = true;
            canSleep1 = true;
            isIdle = false;
            IdleTime = 0f;
        }
    }



    private void AnimatorUpdate()
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

        _animator.SetBool("isOnGround", isOnGrounded());
        _animator.SetFloat("Speed", Mathf.Abs(playerBody2D.velocity.x));
        _animator.SetFloat("Yspeed", Mathf.Abs(playerBody2D.velocity.y));
        _animator.SetFloat("IdleTime", IdleTime);
        _animator.SetBool("isIdle", isIdle);
    }

    void PhysicalUpdate()
    {
        if (playerBody2D.velocity.y <= 0f)
            playerBody2D.gravityScale = fallingGravityScale;
        else
            playerBody2D.gravityScale = commonGravityScale;
        playerBody2D.velocity = new Vector2(horizontal * speed * Time.fixedDeltaTime * backGroundScale, playerBody2D.velocity.y);

        if (isOnGrounded() && canRecordOnGroundHorizontalPosition)
        {
            playerBody2D.transform.position = new Vector3(playerBody2D.transform.position.x, playerBody2D.transform.position.y - DetectRadius / 2f, 1);
            canRecordOnGroundHorizontalPosition = false;
        }
        if (!isOnGrounded())
        {
            canRecordOnGroundHorizontalPosition = true;
        }
    }
    private bool isOnGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, DetectRadius, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, DetectRadius);
    }
}
