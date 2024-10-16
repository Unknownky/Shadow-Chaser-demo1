using UnityEngine;

/// <summary>
/// ¿ØÖÆÄñÐÎÌ¬
/// </summary>
public class BirdController : MonoBehaviour, IStateController
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
    [SerializeField] private float flySpeed;
    [SerializeField] private float diveFlySpeed = 2f;
    [SerializeField] private float backGroundScale;

    [SerializeField] private float flyForce = 1f;


    private float horizontal;
    private bool isFacingRight;


    private void Update()
    {
        Movement();
        Flip();
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

    public bool isOnGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, DetectRadius, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, DetectRadius);
    }

    public void Movement()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        if (Input.GetMouseButtonDown(0))//????????
            playerBody2D.velocity = Vector2.up * flyForce;//?????????
    }

    public void StatesChange()
    {
        bool key = Input.GetKey(KeyCode.Tab);
        if (key)
            statesContainer.SetActive(true);
        else
            statesContainer.SetActive(false);
    }

    private void OnEnable()
    {
        InitParameters();
    }


    public void InitParameters()
    {
        GameObject Canvas = GameObject.Find("BagCanvas");
        statesContainer = Canvas.transform.GetChild(0).gameObject;
    }

    private void FixedUpdate()
    {
        PhysicalUpdate();
    }
    public void AnimatorUpdate()
    {
        return;
    }

    public void PhysicalUpdate()
    {
        if (isOnGrounded())
        {
            playerBody2D.velocity = new Vector2(horizontal * speed * Time.fixedDeltaTime * backGroundScale, playerBody2D.velocity.y);
        }

        else
        {
            //????????
            if (playerBody2D.velocity.y < 0)
            {
                playerBody2D.velocity = new Vector2(horizontal * diveFlySpeed * Time.fixedDeltaTime * backGroundScale, playerBody2D.velocity.y);
            }
            else
            {
                playerBody2D.velocity = new Vector2(horizontal * flySpeed * Time.fixedDeltaTime * backGroundScale, playerBody2D.velocity.y);
            }
        }
    }
}
