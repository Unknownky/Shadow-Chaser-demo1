using EventSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BookSystem
{
    /// <summary>
    /// 控制各种Book场景的Event互动的脚本
    /// </summary>
    public class EventController : MonoBehaviour
    {
        /// <summary>
        /// 从Book脚本获得了记录当前游玩所处页面数
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
        public static void PointerClickLattice(GameObject lattice)
        {
            #if UNITY_EDITOR
            Debug.Log("PointerClick this Lattice");
#endif
            if (!CameraController.isMoving)
            {
                string lat = lattice.name;
                string pag = lattice.transform.parent.name;
                SceneManager.LoadScene(pag + "_" + lat);
            }
        }
        #endregion

        #region PageController
        /// <summary>
        /// 更新获取了的格子的显示，即用于页面图片的更新
        /// </summary>
        private void UpdatePages()
        {

        }
        #endregion


    }
}

