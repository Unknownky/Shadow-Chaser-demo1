using UnityEngine;

namespace EventSystem
{
    /// <summary>
    /// ������������Ļ���
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
            //����Ļ����ת��Ϊ�������������
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