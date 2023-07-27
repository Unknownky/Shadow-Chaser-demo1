using System;
using TMPro;
using UnityEngine;

public class CatEyeController : MonoBehaviour
{
    [Header("Cursor Texture2D")]
    public Texture2D catEyeTexture2D;


    private bool isUsing;

    /// <summary>
    /// 未使用技能时，技能书签被点击
    /// </summary>
    public void PointClickedMarks()
    {
        if(isUsing == false){
            isUsing = true;
            CursorController.SwitchCursorTextureTo(catEyeTexture2D);
        }
    }
        
    /// <summary>
    /// 打开技能后正确的物体被点击
    /// </summary>
    public void PointClickedCorrectItem()
    {
        if(isUsing == true)
        {
            isUsing = false;
            CursorController.SwitchCursorTextureBack();
        }
    }
}
