using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 该脚本放置于可交互的对话点上，用于触发对话
/// </summary>
[RequireComponent(typeof(BoxCollider2D))]
public class InteractSpeakPoint : MonoBehaviour
{
    public TextAsset textAsset;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Cat")) {
            Logger.Log("Cat entered speak point");

        }
    }

    public void StartDialogueSystem(){
        TextManager.Instance.StartDialogueSystem(textAsset.text);
    }


}
