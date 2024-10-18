using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogDirector : MonoBehaviour
{
    public static DialogDirector Instance;
    TextManager textManager;

    string text = "";

    public DialogueContainer dialogueContainer;

    public Dialogue textDialogue;

    public bool oneDialogueOn = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        textManager = TextManager.Instance;
        text = textDialogue.dialogue;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {

            textManager.StartDialogueSystem(text);
        }

    }

    private void SearchDialogue(string dialogueName)
    {
        foreach (var item in dialogueContainer.dialogues)
        {
            if (item.dialogueName == dialogueName)
            {
                text = item.dialogue;
                Logger.Log("Has search the text: " + text);
                oneDialogueOn = true;
                return;
            }
        }
        Logger.Log("No dialogue found");
        text = "";
    }

    #region 暴露给外部的方法
    public void StartDialogue(string dialogueName)
    {
        SearchDialogue(dialogueName);
        textManager.StartDialogueSystem(text);
    }

    public void InjectDialogueEndEvent(UnityEvent unityEvent)
    {
        textManager.dialogueEndEvent = unityEvent;
    }
    #endregion

    [UnityEditor.MenuItem("Developer/Show All DialoguesName")]
    public static void ShowAllDialoguesName()
    {
        //遍历dialogueContainer中的所有对话名字
        foreach (var item in Instance.dialogueContainer.dialogues)
        {
            Logger.Log("dialogueName: " + item.dialogueName);
        }
    }
}
