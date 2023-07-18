using System.IO;
using UnityEngine;

namespace SaveSystemTutorial
{

    /// <summary>
    /// 用于存档系统的静态类
    /// </summary>
    public static class SaveSystem
    {
        #region JSON
        /// <summary>
        /// 给出保存的文件名以及数据类，通过Json的格式进行保存
        /// </summary>
        /// <param name="saveFileName">文件名</param>
        /// <param name="data">要保存的数据类</param>
        public static void SaveByJson(string saveFileName, object data)
        {
            var json = JsonUtility.ToJson(data);
            var path = Path.Combine(Application.persistentDataPath, saveFileName);

            try
            {
                File.WriteAllText(path, json);
                #if UNITY_EDITOR
                Debug.Log($"Successfully saved data to {path}.");
                #endif
            }
            catch (System.Exception exception)
            {
                #if UNITY_EDITOR
                Debug.LogError($"Failed to save data to {path}. \n{exception}");
                #endif
            }
        }
        /// <summary>
        /// 给出已经保存的文件名，对Json格式的文件进行读取为数据类
        /// </summary>
        /// <typeparam name="T">数据类类型</typeparam>
        /// <param name="saveFileName">文件名</param>
        /// <returns></returns>
        public static T LoadFromJson<T>(string saveFileName)
        {
            var path = Path.Combine(Application.persistentDataPath, saveFileName);

            try
            {
                var json = File.ReadAllText(path);
                var data = JsonUtility.FromJson<T>(json);
                #if UNITY_EDITOR
                Debug.Log($"Successfully Load data from{path}.");
                #endif
                return data;
            }
            catch (System.Exception exception)
            {
                #if UNITY_EDITOR
                Debug.LogError($"Failed to Load data from {path}. \n{exception}");
                #endif
            }
            return default;
        }

        #endregion

        #region Deleting
        /// <summary>
        /// 删除永久路径中的该文件名文件
        /// </summary>
        /// <param name="saveFileName"></param>
        public static void DeleteSavedFile(string saveFileName)
        {
            var path = Path.Combine(Application.persistentDataPath, saveFileName);

            try
            {
                File.Delete(path);
            }
            catch (System.Exception exception)
            {
                #if UNITY_EDITOR
                Debug.LogError($"Failed to delete {path}. \n{exception}");
                #endif
            }
        }
        #endregion
    }
}

