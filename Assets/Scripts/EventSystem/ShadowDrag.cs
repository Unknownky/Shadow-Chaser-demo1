using UnityEngine;

public class ShadowDrag : DragController
{
    private float mwidth;
    private float mheight;
    private Vector3 centerDir;

    public float zDragRotation;
    private void Start()
    {
        mwidth = 0.1f;
        mheight = 0.1f;
    }
    protected override void OnMouseDown()
    {
        Debug.Log("Down");
        if (IsBetweenTheScreen())
            base.OnMouseDrag();
        else
        {
            centerDir = Vector3.zero - transform.position;
            transform.position += Vector3.Normalize(centerDir);
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        ResetRotation();
    }

    private void ResetRotation()
    {
        transform.rotation = new Quaternion(0, 0, zDragRotation, 0);
    }

    protected override void OnMouseDrag()
    {
        Debug.Log("Drag");
        ResetRotation();

        if (IsBetweenTheScreen())
            base.OnMouseDrag();
        else
        {
            centerDir = Vector3.zero - transform.position;
            transform.position += Vector3.Normalize(centerDir);
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        ResetRotation();

    }

    protected override bool IsBetweenTheScreen()
    {
        if (transform.position.x > -backgroundWidth / 2 + mwidth && transform.position.x < backgroundWidth / 2 - mwidth)
        {
            if (transform.position.y > -backgroundHeight / 2 + mheight && transform.position.y < backgroundHeight / 2 - mheight)
                return true;
        }
        return false;
    }
}
