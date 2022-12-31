using UnityEditor;

namespace SyskenTLib.UnityAutoFormatConfig.Editor
{
    public class SaveDataManager
    {
        public bool ReadUserConfigBool(string saveKey)
        {
            string savedTextValue = EditorUserSettings.GetConfigValue(saveKey);
            if (savedTextValue == null)
            {
                //値が保存されてなかった場合
                return false;
            }
            else
            {
                //値が保存されていた場合
                if (savedTextValue == "True")
                {
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
        }
        
        public void SaveUserConfigBool(string saveKey,bool saveValue)
        {
            if (saveValue == true)
            {
                EditorUserSettings.SetConfigValue(saveKey,"True");
            }
            else
            {
                EditorUserSettings.SetConfigValue(saveKey,"False"); 
            }

        }
    }
}