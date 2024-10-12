using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public KeyCode interactKey = KeyCode.E;

    public KeyCode carryKey = KeyCode.Q;

    #region ParameterForDialogSystem
    public GameObject interactInfoObject;

    #endregion

    #region ParameterForTransition
    [Tooltip("记录的是上一次调用时的数据")] public bool inLight = false;
    private List<LightInOutStateChangeController> lightCheckObjects = new List<LightInOutStateChangeController>();
    #endregion

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        //将所有名为LightCheck的物体添加到lightCheckObjects中
        GetLightCheckObjectsByName("LightCheck");
        HideVirtualPlatform();
    }

    private void Start()
    {
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

    #region PublicMethod
    public bool NeedTransition()
    {
        bool lightCheckTemp = inLight;
        return lightCheckTemp != GetLightCheck();
    }

    #endregion
    private bool GetLightCheck()
    {
        foreach (var lightCheckObject in lightCheckObjects)
        {
            if (lightCheckObject.lightCheck)
            {
                inLight = true;
                return inLight;
            }
        }
        inLight = false;
        return inLight;
    }

    private void GetLightCheckObjectsByName(string name)
    {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.name == name)
            {
                lightCheckObjects.Add(obj.GetComponent<LightInOutStateChangeController>());
            }
        }
    }
}
