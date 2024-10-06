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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Umbralla"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, streamForce, 0);
            umbrallaController = collision.gameObject.GetComponent<UmbrallaController>();
            umbrallaController.GainRotatePower(rotatePower);
        }
    }
}
