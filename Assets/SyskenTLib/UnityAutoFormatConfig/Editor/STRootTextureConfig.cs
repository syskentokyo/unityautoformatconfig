using UnityEngine;

namespace SyskenTLib.UnityAutoFormatConfig
{
    // [CreateAssetMenu(fileName = "FILENAME", menuName = "STRootTextureConfig", order = 0)]
    public class STRootTextureConfig : ScriptableObject
    {

        public STTextureConfig defaultConfig;
        public STTextureConfig normalUIConfig;
        public STTextureConfig dotUIConfig;
        public STTextureConfig custom1Config;
        public STTextureConfig custom2Config;
        public STTextureConfig custom3Config;
    }
}