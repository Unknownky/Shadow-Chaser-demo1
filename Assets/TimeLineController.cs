using UnityEngine;
using UnityEngine.Playables;

public class TimeLineController : MonoBehaviour
{
    private PlayableDirector director;
    private void Awake()
    {
        director = GetComponent<PlayableDirector>();
    }

    public void DirectorPlay()
    {
        director.Play();
    }
}
