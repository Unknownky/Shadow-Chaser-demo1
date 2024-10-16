using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ThingStayOpen : MonoBehaviour
{
    public float stayedTime = 0f;

    public float stayedForOpenTime = 1f;

    [Tooltip("需要进行保持检测的标签")] public string triggerDetectTag = "Fire";

    public List<GameObject> thingForOpen;

    private void Start() {
        if (thingForOpen != null)
        {
            foreach (var thing in thingForOpen)
            {
                thing.SetActive(false);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag(triggerDetectTag))
        {
            stayedTime += Time.deltaTime;
            if (stayedTime >= stayedForOpenTime)
            {
                if (thingForOpen != null)
                {
                    foreach (var thing in thingForOpen)
                    {
                        thing.SetActive(true);
                    }
                }
            }
        }
    }

}
