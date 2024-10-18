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

    public bool stateDetection = false;

    public bool eventTrigger = false;

    public List<string> detectionTagQueue = new List<string>();

    public List<string> dalogueNameQueue = new List<string>();

    private InteractInfoPop interactInfoPop;

    public List<UnityEventWrapper> onSpeakPointTriggered;

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
                interactInfoPop.ShowInteractInfo(dialogueName);
                if (eventTrigger) onSpeakPointTriggered[dialogueIndex].unityEvent.Invoke();
            }
            else
            {
                dialogueIndex = -1;
                for (int i = 0; i < detectionTagQueue.Count; i++)
                {
                    if (GameManager.instance.StatesContainerDetect(detectionTagQueue[i]))
                    {
                        dialogueIndex += 1;
                    }
                    else
                        break;
                }
                dialogueIndex = dialogueIndex == -1 ? 0 : dialogueIndex;
                interactInfoPop.ShowInteractInfo(dalogueNameQueue[dialogueIndex]);
                if (eventTrigger) onSpeakPointTriggered[dialogueIndex].unityEvent.Invoke();
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
}


[Serializable]
public class UnityEventWrapper
{
    public UnityEvent unityEvent;
}