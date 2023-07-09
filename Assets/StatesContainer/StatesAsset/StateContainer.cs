using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New StateContainer", menuName = "StateContainer/New StateContainer", order = 0)]
public class StateContainer : ScriptableObject
{
    public List<State> states = new List<State>();
    public int CurrentState;
}
