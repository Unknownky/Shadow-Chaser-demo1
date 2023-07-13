using UnityEngine;

/// <summary>
/// ����״̬�Ļ�ȡ
/// </summary>
public class StateWaitingForCat : MonoBehaviour
{
    public State thisState;
    public StatesContainer statesContainer;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        AddStateToStatesContainer(collision);
    }

    private void AddStateToStatesContainer(Collision2D collision)
    {
        int layer = LayerMask.NameToLayer("Player");//ͨ��layer�����ж������
        if (collision.gameObject.layer == layer)//ֻ��һ����ӡ�
        {
            statesContainer.possessedStates.Add(thisState);
            Destroy(gameObject);
        }
    }
}
