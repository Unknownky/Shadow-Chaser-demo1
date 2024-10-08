using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class StateCollect : MonoBehaviour
{
    public StatesContainer statesContainer;

    public State stateForCollect;


    private void OnTriggerEnter2D(Collider2D other) {
        foreach(var tag in statesContainer.possessedStates.Select(state => state.name)) {
            if (other.CompareTag(tag)) {
                statesContainer.AddState(stateForCollect);
                Destroy(gameObject);
                break;
            }
        }
    }
}
