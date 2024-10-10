using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractInfoPop : MonoBehaviour
{
    public GameObject interactInfoObject => GameManager.instance.interactInfoObject;

    public string dialogueNameForShow;


    private void Update() {
        if (Input.GetKeyDown(GameManager.instance.interactKey)) {
            HideInteractInfo();
            StartDialogueSystem();
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
