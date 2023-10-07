using UnityEditor;
using UnityEngine;

namespace SyskenTLib.UnityAutoFormatConfig
{
    public class ConfigManager
    {
        public static STAutoFormatConfig GetConfig()
        {
            string configPath = GetFormatConfigPath();
            if (configPath == "")
            {
                return null;
            }
            
            Debug.Log("設定のコンフィグパス："+configPath);
            return AssetDatabase.LoadAssetAtPath<STAutoFormatConfig> (configPath);
           
        }
        
        
        private static string GetFormatConfigPath()
        {
            string[] guids = AssetDatabase.FindAssets("t:STAutoFormatConfig");
            if (guids.Length != 1)
            {
                Debug.Log("設定ファイルがありません。");
                return "";
            }

            string filePath = AssetDatabase.GUIDToAssetPath(guids[0]);

            return filePath;

        }
    }
}