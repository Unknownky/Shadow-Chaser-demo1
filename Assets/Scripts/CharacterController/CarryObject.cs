using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryObject : MonoBehaviour
{
    public GameObject carryObject;

    private Vector3 carryOffset = new Vector3(0, 0.2f, 0);

    public Vector3 carryOverOffset = new Vector3(0, 0.05f, 0);   

    public bool isCarrying = false;

    private void Carry()
    {
        if (carryObject != null)
        {
            carryObject.transform.position = transform.position + carryOffset;
        }
    }

    private void Drop()
    {
        if (carryObject != null)
        {
            carryObject = null;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(GameManager.instance.carryKey))
        {
            isCarrying = !isCarrying;
        }
        if (isCarrying)
        {
            Carry();
        }
        else
        {
            Drop();
        }

    }

    public void SettleCarryObject(GameObject obj)
    {
        if(obj == null){
            return;
        }
        carryObject = obj;
        //carryOffset等于carryObject的sprite的一半高度
        carryOffset = new Vector3(0, carryObject.GetComponent<SpriteRenderer>().bounds.size.y / 2, 0) + carryOverOffset;

    }
}
