using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnterSwitchCamera : MonoBehaviour
{
    private Collider2D m_Collider;

    public CinemachineVirtualCamera switchCamera1;

    public CinemachineVirtualCamera switchCamera2;


    private void Start()
    {
        m_Collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (GameManager.instance.ComparePlayerTag(other))
        {
            //根据当前的相机切换到另一个相机
            if (switchCamera1.Priority == CinemaTransition.instance.defaultPriority)
            {
                CinemaTransition.instance.SwitchCinema(switchCamera2, switchCamera1);
            }
            else
            {
                CinemaTransition.instance.SwitchCinema(switchCamera1, switchCamera2);
            }
        }   
    }
}
