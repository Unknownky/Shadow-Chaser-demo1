using UnityEngine;

/// <summary>
/// 状态栏脚本
/// </summary>
public class StateSlot : MonoBehaviour//这个脚本用于保存该槽位的信息，便于记录以及传递给StateContainerController
{
    public State stateOnThisSlot;
    public GameObject stateShadow;
    public StatesContainer statesContainer;

    private Vector3 stateShadowDefaultPosition;
    /// <summary>
    /// 用来处理skillUI栏位的选定效果
    /// </summary>

    private void Awake()
    {
        // stateShadow = stateOnThisSlotSlot.
        stateShadowDefaultPosition = stateShadow.transform.position;
    }

    public void StateSlotPanelOnEvent() 
    {
        if (LightCasterController.isCastering)
        {
            SetShadowActiveOn();
            ResetShadowPosition();
        }
    }

    public void StateSlotPanelOffEvent()
    {
        SetShadowActiveOff();
        ResetShadowPosition();
    }

    /// <summary>
    /// 用来处理选定后点击的效果
    /// </summary>
    public void SlotOnClicked()
    {
        if(stateOnThisSlot){
        StatesContainerController.ShowDescription(stateOnThisSlot);
        statesContainer.outWillChangeStateID = stateOnThisSlot.stateID;


        }
    }

    #region Help Function
    private void SetShadowActiveOn()
    {
        bool isUsing = stateShadow.activeSelf ? true : false;
        //Controll the UI pannel 
        stateShadow.SetActive(!isUsing);
    }

    private void ResetShadowPosition()
    {
        stateShadow.transform.position = stateShadowDefaultPosition;
    }

    private void SetShadowActiveOff()
    {
        bool isUsing = stateShadow.activeSelf ? true : false;
        stateShadow.SetActive(!isUsing);
    }

    #endregion

}
