using System.Linq;
using UnityEngine;

/// <summary>
/// ���ƽ����ƹ�״̬���л�
/// </summary>

public class LightStateController : MonoBehaviour
{

    public StateContainer StatesContainer;
    public GameObject PlayerContainer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int layer = LayerMask.NameToLayer("Player");//ͨ��layer�����ж������
        bool canGenner = false;
        canGenner = GameObject.FindGameObjectWithTag(StatesContainer.states[StatesContainer.defaultCatState].name) == null ? true : false;
        if (collision.gameObject.layer == layer && canGenner)
        {
            GameObject prefab = StatesContainer.states[StatesContainer.defaultCatState].StatePrefab;
            GameObject StateObject = Instantiate(prefab, collision.transform.position, Quaternion.identity);
            StateObject.transform.position = collision.transform.position;
            Destroy(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        int layer = LayerMask.NameToLayer("Player");//ͨ��layer�����ж������
        bool canGenner = false;
        if(StatesContainer.currentState == StatesContainer.defaultCatState)
        {
            StatesContainer.currentState = StatesContainer.defaultOutChangeStates; 
        }
        canGenner = GameObject.FindGameObjectWithTag(StatesContainer.states[StatesContainer.currentState].name)==null?true:false;
        if (collision.gameObject.layer == layer && canGenner)
        {
            GameObject prefab = StatesContainer.states[StatesContainer.currentState].StatePrefab;
            GameObject StateObject = Instantiate(prefab, collision.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
        }
    }
}
