using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 状态点击脚本
/// </summary>
public class Slot : MonoBehaviour//这个脚本用于保存该槽位的信息，便于记录以及传递给StateContainerController
{
    public State stateOnThisSlot;
    public StatesContainer statesContainer;

    public void SlotOnClicked()
    {
        StatesContainerController.ShowDescription(stateOnThisSlot);
        statesContainer.outWillChangeStateID = stateOnThisSlot.stateID;
    }
}
