using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// �����ڳ����е�ĳ��Ʒ������״̬�Ļ�ȡ(�õ������)
/// </summary>
public class SkillWaitingForPlayer : MonoBehaviour
{
    public Skill thisSkill;
    public SkillsContainer skillsContainer;

    private void OnMouseDown()
    {
        AddSkillToSkillsContainer();
    }

    private void AddSkillToSkillsContainer()
    {
        skillsContainer.skillsPossession.Add(thisSkill);
        //��ȡ����߼�֮�����޸�
        Destroy(gameObject);
    }
}
