using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �����ڹܿ�StateSlot��Pannel�����ڿ���UI(UI�߼�֮�����)
/// </summary>
public class StatesContainerController : MonoBehaviour
{
    public StatesContainer statesContainer;
    public GameObject playerContainer;
    public Text stateDescriptionText;
    public Image defaultImagePrefab;

    static StatesContainerController instance;
    public static int currentState; //��״̬���ƽű����浱ǰ��״̬�����ڳ�ǿ������ʱ����Ӧ�Ľű���ȡ״ֵ̬���нű��Ŀ��ƺ�ͼ����л�

    private void Awake()//�ڿ���ʱ����
    {
        if (instance != null)//ʹ�õ���ģʽ
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
            StateSlot slot = instance.playerContainer.transform.GetChild(state.stateID).gameObject.GetComponent<StateSlot>();//��ö�Ӧ��slot��λ
            slot.stateOnThisSlot = state;
            stateImage.sprite = state.stateSprite;
            stateImage.transform.localScale = new Vector3(1f, 1f, 1f);
            if (state.stateID == statesContainer.defaultCatStateID)
                stateImage.transform.localScale = new Vector3(2f, 2f, 2f);
        }
    }

    private void OnEnable()
    {
        ClearTextOnShow();
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
