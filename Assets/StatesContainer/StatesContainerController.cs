using UnityEngine;
using UnityEngine.UI;

public class StatesContainerController : MonoBehaviour
{
    public StateContainer StateContainer;
    public GameObject StatesGrid;
    public Text DescriptionText;
    public Image ImagePrefab;

    static StatesContainerController instance;
    public static int currentState; //由状态控制脚本保存当前的状态，便于出强光区域时，对应的脚本获取状态值进行脚本的控制和图像的切换


    private void Awake()//在开启时更新
    {
        if (instance != null)//使用单例模式
            Destroy(instance);
        instance = this;

        foreach (var state in instance.StateContainer.states)
        {
            Image stateImage = Instantiate(instance.ImagePrefab, instance.StatesGrid.transform.GetChild(state.StateID).position, Quaternion.identity);
            stateImage.gameObject.transform.SetParent(instance.StatesGrid.transform.GetChild(state.StateID).gameObject.transform);
            Slot slot = instance.StatesGrid.transform.GetChild(state.StateID).gameObject.GetComponent<Slot>();//获得对应的slot槽位
            slot.SlotState = state;
            stateImage.sprite = state.StateSprite;
            stateImage.transform.localScale = new Vector3(1f, 1f, 1f);
            if (state.StateID == 0)
                stateImage.transform.localScale = new Vector3(2f, 2f, 2f);
        }
        instance.DescriptionText.text = "";
    }

    private void OnEnable()
    {
        instance.DescriptionText.text = "";
    }

    public static void ShowDescription(State state)
    {
        if(state!=null)
            instance.DescriptionText.text = state.StateDescription;
    }
}
