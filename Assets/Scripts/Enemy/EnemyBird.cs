using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBird : MonoBehaviour
{
    public string chaseTag = "Umbralla";
    public float speed = 2f;

    public float enemyScale = 1f;

    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag(chaseTag).transform;
    }

    private void Update()
    {
        //调整敌人的朝向
        if (transform.position.x > player.position.x)
        {
            transform.localScale = new Vector3(-enemyScale, enemyScale, enemyScale);
        }
        else
        {
            transform.localScale = new Vector3(enemyScale, enemyScale, enemyScale);
        }

        if(player != null)
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }
}
