using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 挂载在管控StateSlot的Pannel上用于控制UI(UI逻辑之后更改)
/// </summary>
public class StatesContainerController : MonoBehaviour
{
    public StatesContainer statesContainer;
    public GameObject playerContainer;
    public Text stateDescriptionText;
    public Image defaultImagePrefab;

    static StatesContainerController instance;
    public static int currentState; //由状态控制脚本保存当前的状态，便于出强光区域时，对应的脚本获取状态值进行脚本的控制和图像的切换

    private void Awake()//在开启时更新
    {
        if (instance != null)//使用单例模式
            Destroy(instance);
        instance = this;

        InstantiateStateOnSlot();

        ClearTextOnShow();
    }

    void InstantiateStateOnSlot()
    {
        foreach (var state in instance.statesContainer.possessedStates)
        {
            Image stateImage = Instantiate(instance.defaultImagePrefab, instance.playerContainer.transform.GetChild(state.stateID).position, Quaternion.identity);
            stateImage.gameObject.transform.SetParent(instance.playerContainer.transform.GetChild(state.stateID).gameObject.transform);
            StateSlot slot = instance.playerContainer.transform.GetChild(state.stateID).gameObject.GetComponent<StateSlot>();//获得对应的slot槽位
            slot.stateOnThisSlot = state;
            stateImage.sprite = state.stateSprite;
            stateImage.transform.localScale = new Vector3(1f, 1f, 1f);
            if (state.stateID == statesContainer.defaultCatStateID)
                stateImage.transform.localScale = new Vector3(2f, 2f, 2f);
        }
    }

    private void OnEnable()
    {
        UpdateStateOnSlot();
        ClearTextOnShow();
    }

    private void UpdateStateOnSlot()
    {
        //清空所有的slot下的子物体
        foreach (Transform slot in instance.playerContainer.transform)
        {
            foreach (Transform child in slot)
            {
                Destroy(child.gameObject);
            }
        }
        foreach (var state in instance.statesContainer.possessedStates)
        {
            Image stateImage = Instantiate(instance.defaultImagePrefab, instance.playerContainer.transform.GetChild(state.stateID).position, Quaternion.identity);
            stateImage.gameObject.transform.SetParent(instance.playerContainer.transform.GetChild(state.stateID).gameObject.transform);
            StateSlot slot = instance.playerContainer.transform.GetChild(state.stateID).gameObject.GetComponent<StateSlot>();//获得对应的slot槽位
            slot.stateOnThisSlot = state;
            stateImage.sprite = state.stateSprite;
            stateImage.transform.localScale = new Vector3(1f, 1f, 1f);
            if (state.stateID == statesContainer.defaultCatStateID)
                stateImage.transform.localScale = new Vector3(2f, 2f, 2f);
        }
    }

    
    void ClearTextOnShow()
    {
        instance.stateDescriptionText.text = "";
    }

    public static void ShowDescription(State state)
    {
        if(state!=null)
            instance.stateDescriptionText.text = state.stateDescription;
    }
}
