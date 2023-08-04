using UnityEngine;

/// <summary>
/// 状态栏脚本
/// </summary>
public class StateSlot : MonoBehaviour//这个脚本用于保存该槽位的信息，便于记录以及传递给StateContainerController
{
    public State stateOnThisSlot;
    public GameObject stateShadow;
    public StatesContainer statesContainer;
    
    /// <summary>
    /// 用来处理skillUI栏位的选定效果
    /// </summary>
    public void StateSlotPanelEvent() 
    {
        bool isUsing = stateShadow.activeSelf? true:false;
        //Controll the UI pannel 
        stateShadow.SetActive(!isUsing);
    }

    /// <summary>
    /// 用来处理选定后点击的效果
    /// </summary>
    public void SlotOnClicked()
    {
        StatesContainerController.ShowDescription(stateOnThisSlot);
        statesContainer.outWillChangeStateID = stateOnThisSlot.stateID;
    }
}
