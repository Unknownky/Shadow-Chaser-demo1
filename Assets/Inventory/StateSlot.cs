using UnityEngine;

/// <summary>
/// ״̬���ű�
/// </summary>
public class StateSlot : MonoBehaviour//����ű����ڱ���ò�λ����Ϣ�����ڼ�¼�Լ����ݸ�StateContainerController
{
    public State stateOnThisSlot;
    public StatesContainer statesContainer;

    /// <summary>
    /// ��������skillUI��λ��ѡ��Ч��
    /// </summary>
    public void StateSlotPanelEvent() 
    { 
        
    }

    /// <summary>
    /// ��������ѡ��������Ч��
    /// </summary>
    public void SlotOnClicked()
    {
        StatesContainerController.ShowDescription(stateOnThisSlot);
        statesContainer.outWillChangeStateID = stateOnThisSlot.stateID;
    }
}
