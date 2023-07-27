using UnityEngine;
using UnityEngine.Playables;

public class LoadPanelController : MonoBehaviour
{
    public PlayableDirector director;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PointClicked()
    {
        animator.Play("Spread");
        director?.Play();
    }
}
