using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������������
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
            rb = collision.gameObject.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector3(rb.velocity.x, streamForce, 0);
            umbrallaController = collision.gameObject.GetComponent<UmbrallaController>();
            umbrallaController.GainRotatePower(rotatePower);
        }
    }
}
