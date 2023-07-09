using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStates
{
    //可以用单例模式


    //[Header("PlayerComponent")]
    //[SerializeField] public Animator _animator { get; set; }
    //[SerializeField] public Rigidbody2D playerBody2D { get; set; }

    //[Header("Ground Detected")]
    //[SerializeField] public LayerMask groundLayer { get; set; }
    //[SerializeField] public Transform groundCheck { get; set; }
    //[SerializeField] public float DetectRadius { get; set; }

    //[Header("StateComponent")]
    //[SerializeField] public GameObject statesContainer { get; set; }

    //[Header("PlayerAttributes")]

    //放在Update中
    void Movement();//移动脚本
    void Flip();//翻转游戏
    bool isOnGrounded();//判断是否在地面上

    void StatesChange();//打开背包切换状态,见下

    //private void StatesChange()
    //{
    //    bool key = Input.GetKey(KeyCode.Tab);
    //    if (key)
    //        statesContainer.SetActive(true);
    //    else
    //        statesContainer.SetActive(false);
    //}




    //放于FixedUpdate中用于物理和动画更新
    void AnimatorUpdate();

}
