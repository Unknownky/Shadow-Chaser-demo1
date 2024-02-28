
#if UNITY_EDITOR
public class Logger
{
    public static void Log(string message, string color = "white")
    {
        UnityEngine.Debug.Log("<color=" + color + ">" + message + "</color>");
    }
}
#endif