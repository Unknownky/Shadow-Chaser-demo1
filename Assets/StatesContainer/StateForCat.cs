using UnityEngine;

/// <summary>
/// ����״̬�Ļ�ȡ
/// </summary>
public class StateForCat : MonoBehaviour
{
    public State thisState;
    public StateContainer CatContainer;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        int layer = LayerMask.NameToLayer("Player");//ͨ��layer�����ж������
        if (collision.gameObject.layer == layer)//ֻ��һ����ӡ�
        {
            CatContainer.states.Add(thisState);
            Destroy(gameObject);
        }
    }
}
