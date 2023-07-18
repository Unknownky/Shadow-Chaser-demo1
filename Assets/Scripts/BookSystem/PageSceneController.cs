using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageSceneController : MonoBehaviour
{



    /// <summary>
    /// 记录当前游玩所处页面数
    /// </summary>
    public static int currentPageIndex { private get; set; }

    private PageSceneController instance;


    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
    }

    private void Update()
    {
        UpdatePages();
    }

    /// <summary>
    /// 更新获取了的格子的显示，即用于页面图片的更新
    /// </summary>
    private void UpdatePages()
    {

    }


}
