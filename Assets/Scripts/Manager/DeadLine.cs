using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadLine : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Logger.Log("DeadLine OnTriggerEnter2D");
        if (GameManager.instance.ComparePlayerTag(other))
        {
            GameManager.ReLoadScene();
        }
    }
}
