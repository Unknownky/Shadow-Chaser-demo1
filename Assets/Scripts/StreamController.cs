using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreamController : MonoBehaviour
{
    public float StreamForce;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Umbralla"))
        {
            collision.rigidbody.velocity = new Vector2(collision.rigidbody.velocity.x, StreamForce);
        }
    }
}
