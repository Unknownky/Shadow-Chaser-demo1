using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 结合角色控制脚本，实现虚拟平台下降能力，独立于角色脚本进行控制
/// </summary>
public class VirtualPlatformDown : MonoBehaviour
{
    [Tooltip("请确保正确放在角色控制器上")]public bool useThisAbility = false;
    IStateController stateController;

    private void Awake()
    {
        if (useThisAbility)
            stateController = GetComponent<IStateController>();
    }

    private void Update()
    {
        if (useThisAbility)
        {
            if (Input.GetButtonDown("Down") && stateController.isOnGrounded())
            {
                
            }
        }
    }


}
