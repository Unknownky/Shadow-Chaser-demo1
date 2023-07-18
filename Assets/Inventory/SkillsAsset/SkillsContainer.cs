using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ¼¼ÄÜÈÝÆ÷
/// </summary>
[CreateAssetMenu(fileName = "New Skill", menuName = "SkillsContainer/New SkillsContainer", order = 1)]
public class SkillsContainer : ScriptableObject
{

    public List<Skill> skillsPossession;

}
