using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CinemaTransition : MonoBehaviour
{
    public int defaultPriority = 10;

    public static CinemaTransition instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    #region 公共调用的方法
    public void SwitchCinema(CinemachineVirtualCamera from, CinemachineVirtualCamera to)
    {
        from.Priority = defaultPriority;
        to.Priority = defaultPriority + 1;
    }

    #endregion
}
