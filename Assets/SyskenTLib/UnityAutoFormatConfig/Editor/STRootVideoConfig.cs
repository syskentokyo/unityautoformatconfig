using UnityEngine;

namespace SyskenTLib.UnityAutoFormatConfig
{
    // [CreateAssetMenu(fileName = "FILENAME", menuName = "STRootVideoConfig", order = 0)]
    public class STRootVideoConfig : ScriptableObject
    {
        public STVideoConfig defaultConfig;
        public STVideoConfig normalConfig;
        public STVideoConfig custom1Config;
        public STVideoConfig custom2Config;
        public STVideoConfig custom3Config;
    }
}