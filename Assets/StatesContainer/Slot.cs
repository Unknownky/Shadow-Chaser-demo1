using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour//����ű����ڱ���ò�λ����Ϣ�����ڼ�¼�Լ����ݸ�StateContainerController
{
    public State SlotState;

    public void SlotOnClicked()
    {
        StatesContainerController.ShowDescription(SlotState);
        StatesContainerController.currentState = SlotState.StateID;
    }
}
