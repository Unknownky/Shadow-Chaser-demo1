using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 该脚本放置于可交互的对话点上，用于触发对话
/// </summary>
[RequireComponent(typeof(BoxCollider2D))]
public class InteractSpeakPoint : MonoBehaviour
{
    public string dialogueName;

    [Tooltip("是否启用状态检测")]public bool stateDetection = false;

    [Tooltip("是否启用额外条件检测")]public bool extraConditionDetection = false;
    [Tooltip("是否启用状态及额外条件的事件触发")]public bool eventTrigger = false;

    [Tooltip("待检测的特定的状态")]public List<string> detectionTagQueue = new List<string>();
    [Tooltip("触发事件的额外物体开闭条件")]public List<GameObjectActiveConditionWrapper> eventConditions;

    [Tooltip("依次对应检测状态的对话")]public List<string> dalogueNameQueue = new List<string>();

    private InteractInfoPop interactInfoPop;


    [Tooltip("依次对应检测条件的触发事件")]public List<UnityEventWrapper> onSpeakPointTriggered;

    public int dialogueIndex = -1;


    private void Start()
    {
        if (detectionTagQueue.Count != dalogueNameQueue.Count)
        {
            Logger.Log("detectionTagQueue and dalogueNameQueue should have the same length");
        }
        if (eventTrigger)
        {
            if (onSpeakPointTriggered == null)
            {
                onSpeakPointTriggered = new List<UnityEventWrapper>();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GameManager.instance.ComparePlayerTag(other))
        {
            Logger.Log("Cat entered speak point");
            interactInfoPop = other.GetComponent<InteractInfoPop>();
            if (!stateDetection)
            {
                dialogueIndex = ExtraConditionDetectionIndex();
                if(dialogueIndex != -1)
                    interactInfoPop.ShowInteractInfo(dalogueNameQueue[dialogueIndex]);
                else
                    interactInfoPop.ShowInteractInfo(dialogueName);
                if (eventTrigger)
                {
                    DialogDirector.Instance.InjectDialogueEndEvent(onSpeakPointTriggered[dialogueIndex].unityEvent);
                }

            }
            else
            {
                dialogueIndex = -1;
                for (int i = 0; i < detectionTagQueue.Count; i++)
                {
                    if (GameManager.instance.StatesContainerDetect(detectionTagQueue[i])&&ExtraConditionDetection(eventConditions[i]))
                    {
                        dialogueIndex += 1;
                    }
                    else
                        break;
                }
                dialogueIndex = dialogueIndex == -1 ? 0 : dialogueIndex;
                interactInfoPop.ShowInteractInfo(dalogueNameQueue[dialogueIndex]);
                if (eventTrigger)
                {
                    DialogDirector.Instance.InjectDialogueEndEvent(onSpeakPointTriggered[dialogueIndex].unityEvent);
                }
            }
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (GameManager.instance.ComparePlayerTag(other))
        {
            Logger.Log("Cat exited speak point");
            interactInfoPop.HideInteractInfo();
        }
    }

    private int ExtraConditionDetectionIndex()
    {
        int index = -1;
        for (int i = 0; i < eventConditions.Count-1; i++)
        {
            if (ExtraConditionDetection(eventConditions[i]))
            {
                index = i;
            }
            else break;
        }
        return index;
    }

    private bool ExtraConditionDetection(GameObjectActiveConditionWrapper gameObjectActiveConditionWrapper)
    {
        return gameObjectActiveConditionWrapper.gameObject.activeSelf == gameObjectActiveConditionWrapper.activeCondition;
    }

}


[Serializable]
public class UnityEventWrapper
{
    public UnityEvent unityEvent;
}

[Serializable]
public class GameObjectActiveConditionWrapper
{
    public GameObject gameObject;
    public bool activeCondition;
}