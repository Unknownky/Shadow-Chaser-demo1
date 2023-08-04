using UnityEngine;

/// <summary>
/// 该脚本控制月光的投射
/// </summary>
public class LightCasterController : MonoBehaviour
{
    private GameObject lightCaster;

    private static GameObject _lightCaster;
    private void Awake()
    {
        lightCaster = transform.GetChild(0).gameObject;
#if UNITY_EDITOR
        Debug.Log(lightCaster==null?"lightCaster is null":$"lightCaster is {lightCaster.name}");
#endif
        lightCaster?.SetActive(false);
        _lightCaster = lightCaster;
    }

    public static void CasterLight()
    {
        _lightCaster.SetActive(true);
    }
}
