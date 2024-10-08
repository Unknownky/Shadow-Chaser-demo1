using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ±£´æ×´Ì¬µÄÈÝÆ÷
/// </summary>
[CreateAssetMenu(fileName = "New StatesContainer", menuName = "StatesContainer/New StatesContainer", order = 0)]
public class StatesContainer : ScriptableObject
{
    public List<State> possessedStates = new List<State>();
    public int outWillChangeStateID;
    public int currentStateID;
    public int defaultOutChangeStateID;
    public int defaultCatStateID;

    public void AddState(State state)
    {
        if(possessedStates.Contains(state))
            return;
        possessedStates.Add(state);
    }
}
