using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStates
{
    //�����õ���ģʽ


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

    //����Update��
    void Movement();//�ƶ��ű�
    void Flip();//��ת��Ϸ
    bool isOnGrounded();//�ж��Ƿ��ڵ�����

    void StatesChange();//�򿪱����л�״̬,����

    //private void StatesChange()
    //{
    //    bool key = Input.GetKey(KeyCode.Tab);
    //    if (key)
    //        statesContainer.SetActive(true);
    //    else
    //        statesContainer.SetActive(false);
    //}




    //����FixedUpdate����������Ͷ�������
    void AnimatorUpdate();

}
