using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool inLight = false;

    public KeyCode interactKey = KeyCode.E;

    #region ParameterForDialogSystem
    public GameObject interactInfoObject;

    #endregion

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        HideVirtualPlatform();
    }

    private void Start() {
        InitDialogSystemParameters();
    }

    private void InitDialogSystemParameters()
    {
        interactInfoObject = GameObject.Find("InteractInfoObject");
        interactInfoObject?.SetActive(false);
    }

    private void HideVirtualPlatform()
    {
        var virtualPlatforms = GameObject.FindGameObjectsWithTag("VirtualPlatform");
        foreach (var virtualPlatform in virtualPlatforms)
        {
            virtualPlatform.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

}
