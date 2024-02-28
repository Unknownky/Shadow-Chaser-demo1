using System.Linq;
using UnityEngine;

/// <summary>
/// ���ƽ����ƹ�״̬���л�
/// </summary>

public class LightInOutStateChangeController : MonoBehaviour
{

    public StatesContainer statesContainer;
    public GameObject playerContainer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ChangeOutStateToInStateAndInstantiate(collision);
    }

    void ChangeOutStateToInStateAndInstantiate(Collider2D collision)
    {
        int layer = LayerMask.NameToLayer("Player");//ͨ��layer�����ж������
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
        ChangeInStateToOutStateAndInstantiate(collision);
    }

    void ChangeInStateToOutStateAndInstantiate(Collider2D collision)
    {
        int layer = LayerMask.NameToLayer("Player");//ͨ��layer�����ж������
        bool canGennerOutState = false;
        CorrectOutWillChangeState();
        canGennerOutState = GameObject.FindGameObjectWithTag(statesContainer.possessedStates[statesContainer.outWillChangeStateID].name) == null ? true : false;
        if (collision.gameObject.layer == layer && canGennerOutState)
        {
            State OutWillChangeState = statesContainer.possessedStates[statesContainer.outWillChangeStateID];
            GameObject prefab = OutWillChangeState.statePrefab;
            GameObject StateObject = Instantiate(prefab, collision.transform.position, Quaternion.identity);
            StateObject.transform.parent = collision.transform.parent;
            statesContainer.currentStateID = statesContainer.outWillChangeStateID;
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
