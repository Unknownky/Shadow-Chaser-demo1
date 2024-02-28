using UnityEngine;

/// <summary>
/// 控制角色对话实现
/// </summary>
public class PlayerController : MonoBehaviour
{
    public GameObject dialogBoxPanel;

    public static GameObject _dialogBoxPanel;
    private void Awake()
    {
        _dialogBoxPanel = dialogBoxPanel;
        _dialogBoxPanel?.SetActive(false);
    }

    public static void ShowDialogBoxPanel()
    {
        _dialogBoxPanel.SetActive(true);
    }

    #region Help Functions

    #endregion
}
