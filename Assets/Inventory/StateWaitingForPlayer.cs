using UnityEngine;

/// <summary>
/// �����ڳ����е�״̬������״̬�Ļ�ȡ(��ʱ�õ���ײ���)
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
        //��ȡ����߼�֮�����޸�
        Destroy(gameObject);
    }
}
