using UnityEditor;
using UnityEngine;

namespace SyskenTLib.UnityAutoFormatConfig
{
    public class ConfigManager
    {
        public static STAutoFormatConfig GetGeneralRootConfig()
        {
            string configPath = GetFormatConfigPath("STAutoFormatConfig");
            if (configPath == "")
            {
                return null;
            }
            
            Debug.Log("設定のコンフィグパス："+configPath);
            return AssetDatabase.LoadAssetAtPath<STAutoFormatConfig> (configPath);
        }
        
        public static STRootTextureConfig GetTextureRootConfig()
        {
            string configPath = GetFormatConfigPath("STRootTextureConfig");
            if (configPath == "")
            {
                return null;
            }
            
            Debug.Log("設定のコンフィグパス："+configPath);
            return AssetDatabase.LoadAssetAtPath<STRootTextureConfig> (configPath);
        }
        
        public static STRootAudioConfig GetAudioRootConfig()
        {
            string configPath = GetFormatConfigPath("STRootAudioConfig");
            if (configPath == "")
            {
                return null;
            }
            
            Debug.Log("設定のコンフィグパス："+configPath);
            return AssetDatabase.LoadAssetAtPath<STRootAudioConfig> (configPath);
        }
        
        public static STRootVideoConfig GetVideoRootConfig()
        {
            string configPath = GetFormatConfigPath("STRootVideoConfig");
            if (configPath == "")
            {
                return null;
            }
            
            Debug.Log("設定のコンフィグパス："+configPath);
            return AssetDatabase.LoadAssetAtPath<STRootVideoConfig> (configPath);
        }
        
        
        private static string GetFormatConfigPath(string fileName)
        {
            string[] guids = AssetDatabase.FindAssets("t:"+fileName);
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