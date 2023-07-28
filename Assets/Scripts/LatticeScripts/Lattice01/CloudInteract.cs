using Unity.VisualScripting;
using UnityEngine;

public class CloudInteract : MonoBehaviour
{
    [Header("Event Time")]
    public float durationTime = 3.0f;

    private Animator animator;
    private float stayTime = 0f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        stayTime = 0f;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("TrrigerStay"+CatEyeController.isUsing);
        if (!CatEyeController.isUsing)
            return;
        stayTime += Time.deltaTime;
        if (stayTime >= durationTime)
        {
            animator.Play("Spread");
            CatEyeController.PointClickedCorrectItemTOCorrectPosition();
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!CatEyeController.isUsing)
            return;
        stayTime = 0f;
    }
}
