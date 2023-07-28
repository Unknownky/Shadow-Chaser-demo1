using UnityEngine;

public class WindDrag : DragController
{

    private GameObject parent;
    private float pWidth;
    private float pHeight;
    private float mwidth;
    private float mheight;
    private Vector3 centerDir;

    private void Start()
    {
        mwidth = 0.5f;
        mheight = 0.5f;
        parent = gameObject.transform.parent.gameObject;
        RectTransform pRectTransform = parent.GetComponent<RectTransform>();
        pWidth = pRectTransform.rect.width;
        pHeight = pRectTransform.rect.height;
    }

    protected override void OnMouseDrag()
    {
        if (CatEyeController.isUsing)
        {
            if (IsBetweenTheScreen())
                base.OnMouseDrag();
            else
            {
                centerDir = Vector3.zero - transform.position;
                transform.position += Vector3.Normalize(centerDir);
            }
        }
    }

    protected override void OnMouseDown()
    {
        if (CatEyeController.isUsing)
        {
            if(IsBetweenTheScreen())
                base.OnMouseDown();
            else
            {
                centerDir = Vector3.zero - transform.position;
                transform.position += Vector3.Normalize(centerDir);
            }
        }
    }

    #region Helping Functions
    private bool IsBetweenTheScreen()
    {
        if(transform.position.x > -pWidth / 2 + mwidth && transform.position.x < pWidth / 2 - mwidth)
        {
            if (transform.position.y > -pHeight / 2 + mheight && transform.position.y < pHeight / 2 - mheight)
                return true;
        }
        return false;
    }
    #endregion
}
