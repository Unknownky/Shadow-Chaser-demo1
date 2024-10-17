using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractInfoPop : MonoBehaviour
{
    public GameObject interactInfoObject => GameManager.instance.interactInfoObject;

    public string dialogueNameForShow;

    public Vector3 interactInfoOffset = new Vector3(0, 0.5f, 0);    

    private void Update() {
        if (Input.GetKeyDown(GameManager.instance.interactKey)&&interactInfoObject.activeSelf) {
            // HideInteractInfo();
            StartDialogueSystem();
        }
        if(interactInfoObject.activeSelf){
            //向上偏移一点
            interactInfoObject.transform.position = transform.position + interactInfoOffset;
        }
    }
    public void ShowInteractInfo(string dialogueName)
    {
        interactInfoObject.transform.position = transform.position;
        interactInfoObject.SetActive(true);
        dialogueNameForShow = dialogueName;
        Logger.Log("dialogueNameForShow: " + dialogueNameForShow);
    }

    public void HideInteractInfo()
    {
        interactInfoObject.SetActive(false);
    }

    public void StartDialogueSystem(){
        DialogDirector.Instance.StartDialogue(dialogueNameForShow);
    }


}
