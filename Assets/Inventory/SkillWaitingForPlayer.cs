using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 挂载在场景中的某物品上用于状态的获取(用的鼠标点击)
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
        //获取后的逻辑之后再修改
        Destroy(gameObject);
    }
}
