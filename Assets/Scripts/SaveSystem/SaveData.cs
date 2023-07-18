using System;

namespace SaveSystemTutorial
{
    /// <summary>
    /// 数据传输类
    /// </summary>
    
    [Serializable]//表明类数据可序列化
    public class SaveData
    {
        /// <summary>
        /// 当前场景的索引值
        /// </summary>
        public int currentSceneIndex;
        /// <summary>
        /// 当前场景的阶段
        /// </summary>
        public int currentSceneStage;
        /// <summary>
        /// 漫画书已经找到的格子数
        /// </summary>
        public int foundLatticeCount;

    }
}

