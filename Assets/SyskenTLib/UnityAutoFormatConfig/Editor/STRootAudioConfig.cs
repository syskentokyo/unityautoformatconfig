using UnityEngine;

namespace SyskenTLib.UnityAutoFormatConfig
{
    // [CreateAssetMenu(fileName = "FILENAME", menuName = "STRootAudioConfig", order = 0)]
    public class STRootAudioConfig : ScriptableObject
    {
        public STAudioConfig defaultConfig;
        public STAudioConfig bgmConfig;
        public STAudioConfig seConfig;
        public STAudioConfig custom1Config;
        public STAudioConfig custom2Config;
        public STAudioConfig custom3Config;  
    }
}