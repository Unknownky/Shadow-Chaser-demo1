using System.IO;
using UnityEngine;

namespace SaveSystemTutorial
{

    /// <summary>
    /// ���ڴ浵ϵͳ�ľ�̬��
    /// </summary>
    public static class SaveSystem
    {
        #region JSON
        /// <summary>
        /// ����������ļ����Լ������࣬ͨ��Json�ĸ�ʽ���б���
        /// </summary>
        /// <param name="saveFileName">�ļ���</param>
        /// <param name="data">Ҫ�����������</param>
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
        /// �����Ѿ�������ļ�������Json��ʽ���ļ����ж�ȡΪ������
        /// </summary>
        /// <typeparam name="T">����������</typeparam>
        /// <param name="saveFileName">�ļ���</param>
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
        /// ɾ������·���еĸ��ļ����ļ�
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

