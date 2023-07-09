using UnityEngine;
using UnityEngine.UI;

public class StatesContainerController : MonoBehaviour
{
    public StateContainer StateContainer;
    public GameObject StatesGrid;
    public Text DescriptionText;
    public Image ImagePrefab;

    static StatesContainerController instance;
    public static int currentState; //��״̬���ƽű����浱ǰ��״̬�����ڳ�ǿ������ʱ����Ӧ�Ľű���ȡ״ֵ̬���нű��Ŀ��ƺ�ͼ����л�


    private void Awake()//�ڿ���ʱ����
    {
        if (instance != null)//ʹ�õ���ģʽ
            Destroy(instance);
        instance = this;

        foreach (var state in instance.StateContainer.states)
        {
            Image stateImage = Instantiate(instance.ImagePrefab, instance.StatesGrid.transform.GetChild(state.StateID).position, Quaternion.identity);
            stateImage.gameObject.transform.SetParent(instance.StatesGrid.transform.GetChild(state.StateID).gameObject.transform);
            Slot slot = instance.StatesGrid.transform.GetChild(state.StateID).gameObject.GetComponent<Slot>();//��ö�Ӧ��slot��λ
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
