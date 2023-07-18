using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ״̬����ScriptableObject�ű�
/// </summary>

[CreateAssetMenu(fileName = "State", menuName = "StatesContainer/New State", order = 0)]
public class State : ScriptableObject
{
    public StatesContainer statesContainer;
    public string stateName;
    public Sprite stateSprite;
    [TextArea]
    public string stateDescription;
    public int stateID;
    public GameObject statePrefab;
}
