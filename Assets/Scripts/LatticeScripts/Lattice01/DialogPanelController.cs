using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogPanelController : MonoBehaviour
{
    public GameObject dialogPanel;

    private void OnMouseDown()
    {
        dialogPanel?.SetActive(true);
    }
}
