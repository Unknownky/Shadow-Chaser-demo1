using System;

namespace SaveSystemTutorial
{
    /// <summary>
    /// ���ݴ�����
    /// </summary>
    
    [Serializable]//���������ݿ����л�
    public class SaveData
    {
        /// <summary>
        /// ��ǰ����������ֵ
        /// </summary>
        public int currentSceneIndex;
        /// <summary>
        /// ��ǰ�����Ľ׶�
        /// </summary>
        public int currentSceneStage;
        /// <summary>
        /// �������Ѿ��ҵ��ĸ�����
        /// </summary>
        public int foundLatticeCount;

    }
}

