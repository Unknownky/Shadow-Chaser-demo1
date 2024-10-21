using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusBarController : MonoBehaviour
{
    public GameObject healthBarObject => GameManager.instance.healthBarObject;

    public GameObject powerBarObject => GameManager.instance.powerBarObject;

    public Vector3 healthBarOffset = new Vector3(0, -0.5f, 0);   

    public Vector3 powerBarOffset = new Vector3(0, -0.5f, 0);


    private void Update() {
        if(healthBarObject.activeSelf){
            healthBarObject.transform.localScale = transform.localScale;
            //偏移一点
            healthBarObject.transform.position = transform.position + healthBarOffset;
        }
        if(powerBarObject.activeSelf){
            powerBarObject.transform.localScale = transform.localScale;
            //偏移一点
            powerBarObject.transform.position = transform.position + powerBarOffset;
        }
    }
}
