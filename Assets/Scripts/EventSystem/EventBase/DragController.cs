using UnityEngine;

/// <summary>
/// 作为父类，继承的子类能够被拖动
/// </summary>
public class DragController : MonoBehaviour
{
    #region Drag parameters
    protected Vector3 dragOffset;
    protected Vector3 worldMousePosition;
    #endregion

    /// <summary>
    /// 定义鼠标点击
    /// </summary>
    protected virtual void OnMouseDown()
    {
        dragOffset = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    }

    /// <summary>
    /// 定义鼠标拖动
    /// </summary>
    protected virtual void OnMouseDrag()
    {
        worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = worldMousePosition - dragOffset;
    }
}
