using UnityEngine;

/// <summary>
/// 用于状态的获取
/// </summary>
public class StateForCat : MonoBehaviour
{
    public State thisState;
    public StateContainer CatContainer;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        int layer = LayerMask.NameToLayer("Player");//通过layer进行判定和添加
        if (collision.gameObject.layer == layer)//只是一次添加。
        {
            CatContainer.states.Add(thisState);
            Destroy(gameObject);
        }
    }
}
