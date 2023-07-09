using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "State", menuName = "StateContainer/New State", order = 0)]
public class State : ScriptableObject
{
    public StateContainer container;
    public string Name;
    public Sprite StateSprite;
    [TextArea]
    public string StateDescription;
    public int StateID;
    public GameObject StatePrefab;
}
