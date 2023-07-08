using UnityEngine;


[CreateAssetMenu(fileName = "State", menuName = "StateContainer/New State", order = 0)]
public class State : ScriptableObject
{
    public StateContainer container;
    public string Name;
    [TextArea]
    public string StateDescription;
    public int StateID;
}
