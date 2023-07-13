using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// øÿ÷∆”Í…°–ŒÃ¨
/// </summary>
public class UmbrallaController : MonoBehaviour, IStateController
{
    [Header("PlayerComponent")]
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D playerBody2D;

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


    private float horizontal;
    private bool isFacingRight;


    private void OnEnable()
    {
        InitParameters();

    }


    private void Update()
    {
        Movement();
        Flip();
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
    }

    public void AnimatorUpdate()
    {
        return;
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


    public void InitParameters()
    {
        GameObject Canvas = GameObject.Find("Canvas");
        statesContainer = Canvas.transform.GetChild(0).gameObject;
    }
}
