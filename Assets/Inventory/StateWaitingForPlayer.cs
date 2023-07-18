using UnityEngine;

/// <summary>
/// 挂载在场景中的状态上用于状态的获取(暂时用的碰撞检测)
/// </summary>
public class StateWaitingForPlayer : MonoBehaviour
{
    public State thisState;
    public StatesContainer statesContainer;


    private void OnMouseDown()
    {
        AddStateToStatesContainer();
    }

    private void AddStateToStatesContainer()
    {
        statesContainer.possessedStates.Add(thisState);
        //获取后的逻辑之后再修改
        Destroy(gameObject);
    }
}
