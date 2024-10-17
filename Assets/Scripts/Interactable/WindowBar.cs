using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class WindowBar : MonoBehaviour
{
    public bool isWindowOpen = false;

    public GameObject windowInfo;

    public List<GameObject> constraintWindowOpenThings;


    private void Start()
    {
        windowInfo.SetActive(false);
        ConstraintWindowOpenThingsSetActive(isWindowOpen);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Cat"))
        {
            Debug.Log("Cat entered window bar");
            windowInfo.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Cat"))
        {
            Debug.Log("Cat exited window bar");
            windowInfo.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(GameManager.instance.interactKey) && windowInfo.activeSelf)
        {
            isWindowOpen = !isWindowOpen;
            Debug.Log("Window is open: " + isWindowOpen);
            ConstraintWindowOpenThingsSetActive(isWindowOpen);
        }
    }

    private void ConstraintWindowOpenThingsSetActive(bool active)
    {
        foreach (GameObject constraintWindowOpenThing in constraintWindowOpenThings)
        {
            constraintWindowOpenThing.SetActive(active);
        }
    }
}
