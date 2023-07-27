using UnityEngine;

/// <summary>
/// 通过切换鼠标挂载物体的image的sprite来实现切换鼠标的效果
/// </summary>
public class CursorController : MonoBehaviour
{
    //初始的鼠标图片
    [SerializeField] private Texture2D defaultCursorTexture2D;


    private static Texture2D defaultCursorTexture;
    private static Texture2D currentCursorTexture;
    private void Awake()
    {
        defaultCursorTexture = defaultCursorTexture2D;
        if (defaultCursorTexture == null)
        {
            Debug.Log("defaultCursorImage is null");
        }
        currentCursorTexture = defaultCursorTexture;
    }

    private void Start()
    {
        Cursor.SetCursor(currentCursorTexture, Vector2.zero, CursorMode.Auto);//设置鼠标样式
    }

    public static void SwitchCursorTextureTo(Texture2D newTexture2D)
    {
        currentCursorTexture = newTexture2D;
        SetCursorTexture();
    }

    public static void SwitchCursorTextureBack()
    {
        currentCursorTexture = defaultCursorTexture;
        SetCursorTexture();
    }


    #region Helping Functions
    private static void SetCursorTexture()
    {
        Cursor.SetCursor(currentCursorTexture, Vector2.zero, CursorMode.Auto);
    }
    #endregion
}
