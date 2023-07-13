using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ״̬����ű�
/// </summary>
public class Slot : MonoBehaviour//����ű����ڱ���ò�λ����Ϣ�����ڼ�¼�Լ����ݸ�StateContainerController
{
    public State stateOnThisSlot;
    public StatesContainer statesContainer;

    public void SlotOnClicked()
    {
        StatesContainerController.ShowDescription(stateOnThisSlot);
        statesContainer.outWillChangeStateID = stateOnThisSlot.stateID;
    }
}
