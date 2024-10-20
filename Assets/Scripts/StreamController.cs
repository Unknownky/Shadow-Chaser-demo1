using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 控制上升气流
/// </summary>
public class StreamController : MonoBehaviour
{
    public float streamForce;

    public float rotatePower;

    public UmbrallaController umbrallaController;

    private Rigidbody2D rb;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Umbralla"))
        {
            umbrallaController = collision.gameObject.GetComponent<UmbrallaController>();
            if (umbrallaController.isOpen)
            {
                rb = collision.gameObject.GetComponent<Rigidbody2D>();
                // rb.velocity = new Vector3(rb.velocity.x, streamForce, 0);
                //给雨伞施加一个当前Stream对应方向的力量
                rb.AddForce(transform.up * streamForce);
                umbrallaController.GainRotatePower(rotatePower);
            }
        }
    }
}
