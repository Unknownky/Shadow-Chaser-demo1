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
        public void MouseEnterMark()
        {

        }

        public void MouseClickMark()
        {

        }
        #endregion

        #region Event Help Function


        #endregion


    }
}