using System.Linq;
using UnityEngine;

public class LightStateController : MonoBehaviour
{

    public StateContainer StatesContainer;
    public GameObject PlayerContainer;

    private void Awake()
    {
        Debug.Log("Awake");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int layer = LayerMask.NameToLayer("Player");//ͨ��layer�����ж������
        Debug.Log("Player Enter");
        bool canGenner = false;
        canGenner = GameObject.FindGameObjectWithTag(StatesContainer.states[0].name) == null ? true : false;
        if (collision.gameObject.layer == layer && canGenner)
        {
            Debug.Log("Find the Layer of Player");
            GameObject prefab = StatesContainer.states[0].StatePrefab;
            //Instantiate(myPrefab, new Vector3(0, 0, 0), Quaternion.identity); //ʵ����Ԥ����
            GameObject StateObject = Instantiate(prefab, collision.transform.position, Quaternion.identity);
            if (StateObject != null)
            {
                Debug.Log("StateObject ism't none");
            }
            StateObject.transform.position = collision.transform.position;
            Destroy(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        int layer = LayerMask.NameToLayer("Player");//ͨ��layer�����ж������
        bool canGenner = false;
        canGenner = GameObject.FindGameObjectWithTag(StatesContainer.states[StatesContainer.CurrentState].name)==null?true:false;
        if (collision.gameObject.layer == layer && canGenner)
        {
            GameObject prefab = StatesContainer.states[StatesContainerController.currentState].StatePrefab;
            GameObject StateObject = Instantiate(prefab, collision.transform.position, Quaternion.identity);
            //float offset = PlayerController.Facing == true ? 1 : -1;
            //StateObject.transform.position = new Vector3(collision.transform.position.x + offset, collision.transform.position.y, collision.transform.position.z);
            if (StateObject != null)
            {
                Debug.Log("StateObject ism't none");
            }
            Destroy(collision.gameObject);
        }
    }
}
