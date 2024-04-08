using UnityEngine;

public class StrikeShake : MonoBehaviour
{
    [SerializeField]private CinemaShake vcam;

    [SerializeField]private float intensity = 1f;

    [SerializeField] private float time = 0.3f;

    private void OnCollisionEnter2D(Collision2D other) {
        vcam.ShakeCamera(intensity, time);
    }
}   
