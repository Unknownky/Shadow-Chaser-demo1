using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class WindowBar : MonoBehaviour
{
    public bool isWindowOpen = false;

    public GameObject windowInfo;

    public GameObject constraintWindowOpenLight;

    [Tooltip("进行互动的按键")]public KeyCode interactKey = KeyCode.S;

    private void Start() {
        windowInfo.SetActive(false);
        constraintWindowOpenLight.SetActive(isWindowOpen);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Cat")) {
            Debug.Log("Cat entered window bar");
            windowInfo.SetActive(true);
        }
    }    

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Cat")) {
            Debug.Log("Cat exited window bar");
            windowInfo.SetActive(false);
        }
    }

    private void Update() {
        if(Input.GetKeyDown(interactKey) && windowInfo.activeSelf) {
            isWindowOpen = !isWindowOpen;
            Debug.Log("Window is open: " + isWindowOpen);
            constraintWindowOpenLight.SetActive(isWindowOpen);
        }
    }
}
