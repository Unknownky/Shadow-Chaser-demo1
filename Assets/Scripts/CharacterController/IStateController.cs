using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStateController
{
    //�����õ���ģʽ
    void InitParameters();

    //����Update��
    void Movement();//�ƶ��ű�
    void Flip();//��ת��Ϸ

    bool isOnGrounded();//�ж��Ƿ��ڵ�����

    //����FixedUpdate����������Ͷ�������
    void AnimatorUpdate();

    void PhysicalUpdate();

}
