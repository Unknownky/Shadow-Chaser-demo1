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
    [SerializeField] private float backGroundScale;


    private float horizontal;
    private bool isFacingRight;


    public void InitParameters()
    {
        throw new System.NotImplementedException();
    }

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
        GameObject Canvas = GameObject.Find("Canvas");
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
        playerBody2D.velocity = new Vector2(horizontal * speed * Time.fixedDeltaTime * backGroundScale, playerBody2D.velocity.y);
    }
}
