using UnityEngine;


namespace BookSystem
{
    /// <summary>
    /// ���Ƹ���Book������Event�����Ľű�
    /// </summary>
    public class EventController : MonoBehaviour
    {
        /// <summary>
        /// ��Book�ű�����˼�¼��ǰ��������ҳ����
        /// </summary>
        public static int currentPageIndex { private get; set; }

        private EventController instance;

        private void Awake()
        {
            if (instance != null)
                Destroy(this);
            instance = this;
        }

        private void Update()
        {
            UpdatePages();
        }

        #region LatticeEvent
        public static void PointerClickLattice()
        {
            #if UNITY_EDITOR
            Debug.Log("PointerClick this Lattice");
            #endif
        }
        #endregion


        #region PageController
        /// <summary>
        /// ���»�ȡ�˵ĸ��ӵ���ʾ��������ҳ��ͼƬ�ĸ���
        /// </summary>
        private void UpdatePages()
        {

        }
        #endregion


    }
}

