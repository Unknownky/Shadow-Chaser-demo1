using System;
using System.Collections;
using UnityEngine;


namespace EventSystem
{
    public class CameraController : MonoBehaviour
    {
        public Transform defaulttransform;
        /// <summary>
        /// 摄像头距离目标的位置的偏置
        /// </summary>
        public Vector3 offsetPostion = Vector3.zero;
        public float moveSpeed = 0.2f;

        private Transform _targetTransform;

        public static bool isMoving;

        private void Awake()
        {
            isMoving = false;
            SmoothMoving(defaulttransform);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) 
            {
                SmoothMoving(defaulttransform);
            }
        }

        #region SmoothMoving

        /// <summary>
        /// 给出目标地点让挂载脚本的摄像头平滑移动和旋转到对应的位置
        /// <param name="transform"></param>
        public void SmoothMoving(Transform targettransform)
        {
            if (targettransform.position == transform.position)
                return;
            isMoving = true;
            _targetTransform = targettransform;
            var modulo = new Func<Vector3, float>((target) => Mathf.Sqrt(target.x * target.x + target.y * target.y + target.z * target.z));
            Vector3 adjustPosition = _targetTransform.position - transform.position;
            Vector3 adjustDirection = adjustPosition / modulo(adjustPosition);
            Vector3 speed = adjustDirection * moveSpeed;
            StartCoroutine(Moving(speed));
        }
        #endregion

        #region Help Function
        private IEnumerator Moving(Vector3 speed)
        {
            while (Vector3.Distance(transform.position, _targetTransform.position) > 0.05f)
            {
                transform.position += speed;
                Debug.Log("Moving");
                yield return null;
            }
            transform.position = _targetTransform.position;
            isMoving = false;
        }
        #endregion
    }
}

