using UnityEngine;

public class CatEyeController : MonoBehaviour
{
    [Header("Cursor Texture2D")]
    public Texture2D catEyeTexture2D;


    public static bool isUsing = false;

    /// <summary>
    /// 未使用技能时，技能书签被点击
    /// </summary>
    public void PointClickedMarks()
    {
        if(isUsing == false){
            isUsing = true;
            CursorController.SwitchCursorTextureTo(catEyeTexture2D);
        }
        else if(isUsing == true)
        {
            isUsing = false;
            CursorController.SwitchCursorTextureBack();
        }
    }
        
    /// <summary>
    /// 打开技能后正确的物体被点击
    /// </summary>
    public static void PointClickedCorrectItemTOCorrectPosition()
    {
        if(isUsing == true)
        {
            isUsing = false;
            CursorController.SwitchCursorTextureBack();
        }
    }
}
