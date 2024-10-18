using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatBallController : MonoBehaviour, IStateController
{
    public float speed;

    private float horizontal;

    private void Update() {
        Movement();
    }

    public void AnimatorUpdate()
    {
        throw new System.NotImplementedException();
    }

    public void Flip()
    {
        throw new System.NotImplementedException();
    }

    public void InitParameters()
    {
        throw new System.NotImplementedException();
    }

    public bool isOnGrounded()
    {
        throw new System.NotImplementedException();

    }

    public void Movement()
    {
        horizontal = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * horizontal * speed * Time.deltaTime);
    }

    public void PhysicalUpdate()
    {
        throw new System.NotImplementedException();
    }
}
