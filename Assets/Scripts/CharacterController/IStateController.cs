using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStateController
{
    //可以用单例模式
    void InitParameters();

    //放在Update中
    void Movement();//移动脚本
    void Flip();//翻转游戏

    bool isOnGrounded();//判断是否在地面上

    //放于FixedUpdate中用于物理和动画更新
    void AnimatorUpdate();

    void PhysicalUpdate();

}
