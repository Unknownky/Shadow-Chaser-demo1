using UnityEngine;

namespace SaveSystemTutorial
{
    /// <summary>
    /// 数据传输控制脚本
    /// </summary>
    public class PlayerGameData : MonoBehaviour
    {
        #region Fields
        [SerializeField] int currentSceneIndex;
        [SerializeField] int currentSceneStage;
        [SerializeField] int foundLatticeCount;

        const string PLAYER_DATA_FILE_NAME = "PlayerGameData.file";
        #endregion

        #region Properties
        public int CurrentSceneIndex => currentSceneIndex;
        public int CurrentSceneStage => currentSceneStage;
        public int FoundLatticeCount => foundLatticeCount;

        #endregion

        #region Save and Load
        public void Save()
        {
            SaveByJson();
        }

        public void Load()
        {
            LoadFromJson();
        }

        #endregion

        #region Json
        void SaveByJson()
        {
            SaveSystem.SaveByJson(PLAYER_DATA_FILE_NAME, SavingData());
        }

        void LoadFromJson()
        {
            var saveData = SaveSystem.LoadFromJson<SaveData>(PLAYER_DATA_FILE_NAME);

            LoadData(saveData);
        }
        #endregion

        #region Help Function
        SaveData SavingData()
        {
            var saveData = new SaveData();
            saveData.currentSceneIndex = CurrentSceneIndex;
            saveData.currentSceneStage = CurrentSceneStage;
            saveData.foundLatticeCount = FoundLatticeCount;
            return saveData;
        }

        void LoadData(SaveData saveData)
        {
            currentSceneIndex = saveData.currentSceneIndex;
            currentSceneStage = saveData.currentSceneStage;
            foundLatticeCount = saveData.foundLatticeCount;
        }

        #if UNITY_EDITOR
        [UnityEditor.MenuItem("Developer/Dele PlayerGameData save file")]
        public static void DeletePlayerGameDataSaveFile()
        {
            SaveSystem.DeleteSavedFile(PLAYER_DATA_FILE_NAME);
        }
        #endif
        #endregion
    }

}

