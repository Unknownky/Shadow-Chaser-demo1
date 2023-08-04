using UnityEngine;

/// <summary>
/// 作为父类，继承的子类能够被拖动
/// </summary>
public class DragController : MonoBehaviour
{
    #region Drag parameters
    protected Vector3 dragOffset;
    protected Vector3 worldMousePosition;
    protected float backgroundWidth;
    protected float backgroundHeight;
    #endregion

    private void Awake()
    {
        GameObject parent = GameObject.Find("Background");
        RectTransform pRectTransform = parent.GetComponent<RectTransform>();
        backgroundWidth = pRectTransform.rect.width;
        backgroundHeight = pRectTransform.rect.height;
    }

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

    protected virtual bool IsBetweenTheScreen()
    {
        return true;
    }
}
