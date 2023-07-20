using UnityEngine;

/// <summary>
/// ��Ϊ���࣬�̳е������ܹ����϶�
/// </summary>
public class DragController : MonoBehaviour
{
    #region Drag parameters
    protected Vector3 dragOffset;
    protected Vector3 worldMousePosition;
    #endregion

    /// <summary>
    /// ���������
    /// </summary>
    protected void OnMouseDown()
    {
        dragOffset = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    }

    /// <summary>
    /// ��������϶�
    /// </summary>
    protected void OnMouseDrag()
    {
        worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = worldMousePosition - dragOffset;
    }
}
