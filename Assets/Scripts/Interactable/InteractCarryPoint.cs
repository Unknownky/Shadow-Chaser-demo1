using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 该脚本放置于可交互的携带点上，用于携带物体
/// </summary>
[RequireComponent(typeof(BoxCollider2D))]
public class InteractCarryPoint : MonoBehaviour
{
    public GameObject matchedCarryObject;

    private Rigidbody2D carryObjectRigidbody;

    [Tooltip("待携带物体刚体参数")]
    public bool freezeRotation = false;

    public bool useAutoMass = true;

    public float gravityScale = 1f;

    private void Awake() {
        if (matchedCarryObject != null) {
            //给待携带物体添加Rigidbody组件并将参数设置为carryObjectRigidbody
            matchedCarryObject.AddComponent<Rigidbody2D>();
            carryObjectRigidbody = matchedCarryObject.GetComponent<Rigidbody2D>();
            carryObjectRigidbody.freezeRotation = freezeRotation;
            //设置carryObjectRigidbody的Use Auto Mass选项
            carryObjectRigidbody.useAutoMass = useAutoMass;
            //设置carryObjectRigidbody的Gravity Scale参数
            carryObjectRigidbody.gravityScale = gravityScale;
            //给matchedCarryObject添加BoxCollider2D组件
            matchedCarryObject.AddComponent<BoxCollider2D>();
            //设置matchedCarryObject的Layer为carryObject
            matchedCarryObject.layer = LayerMask.NameToLayer("carryObject");


            //将当前物体设置为matchedCarryObject的子物体
            transform.transform.SetParent(matchedCarryObject.transform);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Cat")) {
            Logger.Log("Cat entered carry point");
            CarryObject carryObject = other.GetComponent<CarryObject>();
            carryObject.SettleCarryObject(matchedCarryObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Cat")) {
            Logger.Log("Cat exited carry point");
            CarryObject carryObject = other.GetComponent<CarryObject>();
            carryObject.SettleCarryObject(matchedCarryObject);
        }
    }

}
