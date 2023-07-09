using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour//这个脚本用于保存该槽位的信息，便于记录以及传递给StateContainerController
{
    public State SlotState;
    public StateContainer _StatesContainer;

    public void SlotOnClicked()
    {
        StatesContainerController.ShowDescription(SlotState);
        StatesContainerController.currentState = SlotState.StateID;
        _StatesContainer.CurrentState = SlotState.StateID;
    }
}
