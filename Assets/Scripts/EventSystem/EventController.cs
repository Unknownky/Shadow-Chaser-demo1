using UnityEngine;

namespace EventSystem
{
    /// <summary>
    /// 控制世界物体的互动
    /// </summary>
    public class EventController:MonoBehaviour
    {
        public Camera mainCamera;


        #region DragParametric
        private Vector3 worldMousePosition;
        private Vector3 dragOffset = Vector3.zero;
        #endregion



        #region EnvironmentEvent
        public void PointDragItems(GameObject dragGameObject)
        {
#if UNITY_EDITOR
            Debug.Log("Drag items");
#endif
            worldMousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            dragGameObject.transform.position = worldMousePosition - dragOffset;
        }

        public void PointClick()
        {
            Debug.Log("Clicked");
        }


        #endregion

        #region Event Help Function
        private void OnMouseDown()
        {
            var mousePosition = Input.mousePosition;
            //将屏幕坐标转换为世界坐标的射线
            Ray ray = mainCamera.ScreenPointToRay(mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                GameObject clickedObject = hit.collider.gameObject;
#if UNITY_EDITOR
                Debug.Log($"Hit gameobject name is {clickedObject.name}");
#endif

            }


        }
        #endregion


    }
}