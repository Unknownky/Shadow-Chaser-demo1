using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHiddenPlatform : MonoBehaviour
{

#if UNITY_EDITOR
    [UnityEditor.MenuItem("Developer/Show Hidden VirtualPlatform")]
    public static void ShowHiddenVirtualPlatform()
    {
        var virtualPlatforms = GameObject.FindGameObjectsWithTag("VirtualPlatform");
        foreach (var virtualPlatform in virtualPlatforms)
        {
            virtualPlatform.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    [UnityEditor.MenuItem("Developer/Hide VirtualPlatform")]
    public static void HideVirtualPlatform()
    {
        var virtualPlatforms = GameObject.FindGameObjectsWithTag("VirtualPlatform");
        foreach (var virtualPlatform in virtualPlatforms)
        {
            virtualPlatform.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
#endif

}
