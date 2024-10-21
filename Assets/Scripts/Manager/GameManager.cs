using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public StatesContainer statesContainer;

    [Header("热键绑定")]
    public KeyCode interactKey = KeyCode.E;

    public KeyCode carryKey = KeyCode.Q;

    public KeyCode UmbrallaKey = KeyCode.Space;

    #region ParameterForDialogSystem
    public GameObject interactInfoObject;

    #endregion

    #region ParameterForTransition
    [Tooltip("记录的是上一次调用时的数据")] public bool inLight = false;
    private List<LightInOutStateChangeController> lightCheckObjects = new List<LightInOutStateChangeController>();
    #endregion

    public GameObject bagCanvasObject;

    public GameObject healthBarObject;

    public GameObject powerBarObject;

    public Canvas bagCanvas;

    public Camera mainCamera;

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
        // 订阅场景加载完成事件
        SceneManager.sceneLoaded += OnSceneLoaded;
        InitDialogSystemParameters();
        InitStatusBarParameters();
        InitCameraParameters();
    }

    void OnDestroy()
    {
        // 取消订阅场景加载完成事件
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        //场景加载完成后重新初始化摄像机参数
        InitCameraParameters();
    }

    private void InitCameraParameters()
    {
        // 获取主摄像机并设置为Canvas的render camera
        mainCamera = GameObject.Find("MainCamera")?.GetComponent<Camera>();
        if (bagCanvas != null && mainCamera != null)
        {
            bagCanvas.worldCamera = mainCamera;
        }
    }

    private void InitStatusBarParameters()
    {
        healthBarObject = GameObject.Find("HealthBar");
        powerBarObject = GameObject.Find("PowerBar");
    }

    private void InitDialogSystemParameters()
    {
        interactInfoObject = GameObject.Find("InteractInfoObject");
        interactInfoObject?.SetActive(false);
        bagCanvasObject = GameObject.Find("BagCanvas");
        bagCanvas = bagCanvasObject?.GetComponent<Canvas>();

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


    public bool ComparePlayerTag(Collider2D collider2D)
    {
        bool isPlayer = false;
        foreach (var tag in statesContainer.possessedStates.Select(state => state.name))
        {
            if (collider2D.CompareTag(tag))
            {
                isPlayer = true;
                break;
            }
        }
        return isPlayer;
    }

    public bool ComparePlayerTag(Collision2D collision2D)
    {
        bool isPlayer = false;
        foreach (var tag in statesContainer.possessedStates.Select(state => state.name))
        {
            if (collision2D.collider.CompareTag(tag))
            {
                isPlayer = true;
                break;
            }
        }
        return isPlayer;
    }


    public bool StatesContainerDetect(string name)
    {
        if (name == null) return true;
        foreach (var state in statesContainer.possessedStates)
        {
            if (state.name == name)
            {
                return true;
            }
        }
        return false;
    }

    public bool StatesContainerDetect(List<string> names)
    {
        foreach (var name in names)
        {
            if (!StatesContainerDetect(name))
            {
                return false;
            }
        }
        return true;
    }

    public static void LoadScene(string sceneName)
    {
        if (sceneName == "") return;
        SceneManager.LoadScene(sceneName);
    }

    public static void ReLoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Logger.Log("Reload Scene");
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
