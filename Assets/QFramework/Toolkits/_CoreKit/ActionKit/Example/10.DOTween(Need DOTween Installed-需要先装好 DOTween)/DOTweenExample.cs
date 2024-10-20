﻿using DG.Tweening;
using UnityEngine;

namespace QFramework.Example
{
    public class DOTweenExample : MonoBehaviour
    {
        private void Start()
        {
            // 使用 Custom 就可以方便接入
            // Just Use Custom 
            ActionKit.Custom(c =>
            {
                c.OnStart(() => { transform.DOLocalMove(Vector3.one, 0.5f).OnComplete(c.Finish); });
            }).Start(this);
            
            // 也可以自定义 IAction
            // Also implement with IAction
            DOTweenAction.Allocate(() => transform.DOLocalRotate(Vector3.one, 0.5f))
                .Start(this);
            
            // 使用 ToAction
            // Use ToAction
            DOVirtual.DelayedCall(2.0f, () => LogKit.I("2.0f")).ToAction().Start(this);

            // 链式 API 支持
            // fluent api support
            ActionKit.Sequence()
                .DOTween(() => transform.DOScale(Vector3.one, 0.5f))
                .Start(this);
        }
    }
    
  
}