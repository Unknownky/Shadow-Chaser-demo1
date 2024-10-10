using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogDirector : MonoBehaviour
{
    TextManager textManager;

    string text = "<#小明>你好呀，今天的天气真好！<break><#小李>是呀，这正是散步的好日子<rain><break><#小明>……<break><#小李>……<finish>";


    public Dialogue dialogue;

    private void Start(){
        textManager = TextManager.Instance;
        text = dialogue.dialogue;   
    }
    private void Update(){
        if(Input.GetKeyDown(KeyCode.Space)){
        
            textManager.StartDialogueSystem(text);
        }

    }

}
