using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

/// <summary>
/// 控制进出灯光状态的切换
/// </summary>

public class LightInOutStateChangeController : MonoBehaviour
{

    public StatesContainer statesContainer;
    public GameObject playerContainer;

    private GameObject bagCanvas;

    public bool lightCheck = false; //检测是否进入灯光

    private void Start()
    {
        bagCanvas = GameObject.Find("BagCanvas");
        playerContainer = bagCanvas.transform.GetChild(0).gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (ComparePlayerOutTag(collision))
        {
            lightCheck = true;
            Logger.Log("Player ENter Light");
            if(GameManager.instance.NeedTransition())
                ChangeOutStateToInStateAndInstantiate(collision);
        }
    }

    private bool ComparePlayerOutTag(Collider2D collider2D)
    {
        bool isPlayerOut =false;
        foreach (var tag in statesContainer.possessedStates.Select(state => state.name))
        {
            if (collider2D.CompareTag(tag))
            {
                isPlayerOut = true;
                break;
            }
        }
        
        return isPlayerOut;
    }

    void ChangeOutStateToInStateAndInstantiate(Collider2D collision)
    {
        int layer = LayerMask.NameToLayer("Player");//通过layer进行判定和添加
        bool canGennerCat = false;
        canGennerCat = GameObject.FindGameObjectWithTag(statesContainer.possessedStates[statesContainer.defaultCatStateID].name) == null ? true : false;
        if (collision.gameObject.layer == layer && canGennerCat)
        {
            State InWillChangeState = statesContainer.possessedStates[statesContainer.defaultCatStateID];
            GameObject prefab = InWillChangeState.statePrefab;
            GameObject StateObject = Instantiate(prefab, collision.transform.position, Quaternion.identity);
            StateObject.transform.position = collision.transform.position;
            statesContainer.currentStateID = statesContainer.defaultCatStateID;
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Cat"))
        {
            lightCheck = false;
            Logger.Log("Player Exit Light");
            if(GameManager.instance.NeedTransition())
                ChangeInStateToOutStateAndInstantiate(collision);
        }
    }

    void ChangeInStateToOutStateAndInstantiate(Collider2D collision)
    {
        int layer = LayerMask.NameToLayer("Player");//通过layer进行判定和添加
        bool canGennerOutState = false;
        CorrectOutWillChangeState();
        //从statesContainer中获取对应为outWillChangeStateID的state
        State OutWillChangeState = statesContainer.possessedStates.Select(state => state).Where(state => state.stateID == statesContainer.outWillChangeStateID).FirstOrDefault();
        canGennerOutState = GameObject.FindGameObjectWithTag(OutWillChangeState.name) == null ? true : false;
        if (collision.gameObject.layer == layer && canGennerOutState)
        {
            GameObject prefab = OutWillChangeState.statePrefab;
            GameObject StateObject = Instantiate(prefab, collision.transform.position, Quaternion.identity);
            StateObject.transform.parent = collision.transform.parent;
            statesContainer.currentStateID = statesContainer.outWillChangeStateID;
            playerContainer.SetActive(false);
            Destroy(collision.gameObject);
        }
    }

    void CorrectOutWillChangeState()
    {
        if (statesContainer.outWillChangeStateID == statesContainer.defaultCatStateID)
        {
            statesContainer.outWillChangeStateID = statesContainer.defaultOutChangeStateID;
        }
    }
}
