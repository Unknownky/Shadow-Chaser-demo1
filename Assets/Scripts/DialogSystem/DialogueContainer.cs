using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("DialogueSystem/NewDialogueContainer"), fileName = ("NewDialogueContainer"), order = 1)]
public class DialogueContainer : ScriptableObject
{
    [SerializeField] public List<Dialogue> dialogues = new List<Dialogue>();


#if UNITY_EDITOR
    [UnityEditor.MenuItem("Developer/Add All Dialogues")]
    //添加当前文件夹下所有的对话
    public static void AddAllDialogues()
    {
        DialogueContainer container = Resources.Load<DialogueContainer>("DialogueContainer");
        container.dialogues.Clear();

        string[] guids = UnityEditor.AssetDatabase.FindAssets("t:Dialogue", new[] { "Assets/Scripts/DialogSystem/DialogFiles" });
        foreach (string guid in guids)
        {
            string path = UnityEditor.AssetDatabase.GUIDToAssetPath(guid);
            Dialogue dialogue = UnityEditor.AssetDatabase.LoadAssetAtPath<Dialogue>(path);
            container.dialogues.Add(dialogue);
        }

        UnityEditor.EditorUtility.SetDirty(container);
    }
#endif

}
