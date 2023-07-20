using System;
using System.Collections;
using UnityEngine;


namespace EventSystem
{
    public class CameraController : MonoBehaviour
    {
        public Transform defaulttransform;
        /// <summary>
        /// ����ͷ����Ŀ���λ�õ�ƫ��
        /// </summary>
        public Vector3 offsetPostion = Vector3.zero;
        public float moveSpeed = 0.2f;

        private Transform _targetTransform;

        private void Awake()
        {
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
        /// ����Ŀ��ص��ù��ؽű�������ͷƽ���ƶ�����ת����Ӧ��λ��
        /// <param name="transform"></param>
        public void SmoothMoving(Transform targettransform)
        {
            if (targettransform.position == transform.position)
                return;
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
        }
        #endregion
    }
}

