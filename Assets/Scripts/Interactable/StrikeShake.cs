using UnityEngine;

public class StrikeShake : MonoBehaviour
{
    [SerializeField]private CinemaShake vcam;

    [SerializeField]private float intensity = 1f;

    [SerializeField] private float time = 0.3f;

    private void OnCollisionEnter2D(Collision2D other) {
        // GameObject boxCanvas = GameObject.Find("BagCanvas");
        //获取Screen Space - Camera模式下的Canvas组件的RenderCamera
        // Camera renderCamera = boxCanvas.GetComponent<Canvas>().worldCamera;
        Camera mainCamera = Camera.main;
        //获取当前Camera的CinemachineBrain组件的ActiveVirtualCamera
        Cinemachine.CinemachineBrain brain = mainCamera.GetComponent<Cinemachine.CinemachineBrain>();
        vcam = brain.ActiveVirtualCamera.VirtualCameraGameObject.GetComponent<CinemaShake>();

        vcam.ShakeCamera(intensity, time);
    }
}   
