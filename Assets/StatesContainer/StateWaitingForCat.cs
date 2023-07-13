using UnityEngine;

/// <summary>
/// 用于状态的获取
/// </summary>
public class StateWaitingForCat : MonoBehaviour
{
    public State thisState;
    public StatesContainer statesContainer;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        AddStateToStatesContainer(collision);
    }

    private void AddStateToStatesContainer(Collision2D collision)
    {
        int layer = LayerMask.NameToLayer("Player");//通过layer进行判定和添加
        if (collision.gameObject.layer == layer)//只是一次添加。
        {
            statesContainer.possessedStates.Add(thisState);
            Destroy(gameObject);
        }
    }
}
