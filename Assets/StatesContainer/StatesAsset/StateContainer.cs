using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����״̬������
/// </summary>
[CreateAssetMenu(fileName = "New StateContainer", menuName = "StateContainer/New StateContainer", order = 0)]
public class StateContainer : ScriptableObject
{
    public List<State> states = new List<State>();
    public int currentState;
    public int defaultOutChangeStates;
    public int defaultCatState;
}
