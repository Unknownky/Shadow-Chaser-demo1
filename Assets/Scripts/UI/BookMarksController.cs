using UnityEngine;
using UnityEngine.UIElements;

public class BookMarksController : MonoBehaviour
{
    private Animator animator;

    private string m_name;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        m_name = gameObject.name;
    }

    public void PointerEnter()
    {
        animator.Play(m_name + "Enter");
    }

    public void PointerExit()
    {
        animator.Play(m_name + "Exit");
    }

}
