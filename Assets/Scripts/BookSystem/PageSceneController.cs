using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageSceneController : MonoBehaviour
{



    /// <summary>
    /// ��¼��ǰ��������ҳ����
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
    /// ���»�ȡ�˵ĸ��ӵ���ʾ��������ҳ��ͼƬ�ĸ���
    /// </summary>
    private void UpdatePages()
    {

    }


}
