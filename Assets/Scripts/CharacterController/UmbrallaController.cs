using System;
using System.Diagnostics;
using UnityEngine;


/// <summary>
/// 控制雨伞形态
/// </summary>
public class UmbrallaController : MonoBehaviour, IStateController
{
    [Header("PlayerComponent")]
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D playerBody2D;

    [SerializeField] private GameObject windPowerEffectObject;

    [Header("Ground Detected")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float DetectRadius;

    [Header("StateComponent")]
    [SerializeField] private GameObject statesContainer;

    [Header("PlayerAttributes")]
    [SerializeField] private float force;
    [SerializeField] private float speed;
    [SerializeField] private float backGroundScale;

    [SerializeField] private float rotateRate = 2f;

    [SerializeField] private float usingPowerRate = 0.02f;

    [SerializeField] public bool isOpen = false;

    //世界坐标朝向右
    private Vector3 rightRotation = new Vector3(0, 0, 90);

    private Vector3 leftRotation = new Vector3(0, 0, 90);

    public float RotatePower
    {
        get => rotatePower;
        private set => rotatePower = value;
    }

    private float rotatePower = 0f;


    private float horizontal;
    private bool isFacingRight;


    private void OnEnable()
    {
        InitParameters();
    }

    private void Update()
    {
        Movement(); //移动
        Flip();     //翻转
        Rotate();   //旋转
        Switch();   //切换
    }

    private void Switch()
    {
        if (Input.GetKeyDown(GameManager.instance.UmbrallaKey))
        {
            if (isOpen)
            {
                isOpen = false;
            }
            else
            {
                isOpen = true;
            }
        }
    }

    private void Rotate()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.down * rotateRate);
            UseRotatePower();
            WindPowerEffect("left");

        }
        else if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.up * rotateRate);
            UseRotatePower();
            WindPowerEffect("right");
        }
        else
        {
            windPowerEffectObject.SetActive(false);
        }
    }

    public void Movement()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
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
        PhysicalUpdate();
        AnimatorUpdate();
    }

    public void AnimatorUpdate()
    {
        if (isOpen)
        {
            _animator.Play("OpenUmbralla");
        }
        else
        {
            _animator.Play("CloseUmbralla");
        }
    }


    public bool isOnGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, DetectRadius, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, DetectRadius);
    }


    public void PhysicalUpdate()
    {
        playerBody2D.velocity = new Vector2(horizontal * speed * Time.fixedDeltaTime * backGroundScale, playerBody2D.velocity.y);
    }

    public void GainRotatePower(float power)
    {
        RotatePower += power;
    }

    public void UseRotatePower()
    {
        RotatePower -= usingPowerRate;
    }

    public void WindPowerEffect(string direction)
    {
        if (RotatePower <= 0)
        {
            windPowerEffectObject.SetActive(false);
            return;
        }
        UseRotatePower();
        windPowerEffectObject.SetActive(true);
        if (direction == "left")
        {
            windPowerEffectObject.transform.rotation = Quaternion.Euler(leftRotation);
        }
        else if (direction == "right")
        {
            windPowerEffectObject.transform.rotation = Quaternion.Euler(rightRotation);
        }
    }

    public void InitParameters()
    {
        windPowerEffectObject = transform.GetChild(1).gameObject;
        playerBody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        isOpen = true;
    }
}
