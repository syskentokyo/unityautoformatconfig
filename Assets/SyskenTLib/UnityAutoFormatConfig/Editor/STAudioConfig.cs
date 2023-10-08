using UnityEngine;

namespace SyskenTLib.UnityAutoFormatConfig
{
    // [CreateAssetMenu(fileName = "FILENAME", menuName = "STAudioConfig", order = 0)]
    public class STAudioConfig : ScriptableObject
    {
        
        [Header("Standalone")]
        public AudioCompressionFormat audioCompressionFormatOnStandalone = AudioCompressionFormat.Vorbis;
        public AudioClipLoadType audioClipsLoadTypeOnStandalone = AudioClipLoadType.Streaming;
        public float audioQualityOnStandalone = 1.0f;
        public uint audioSampleRateOnStandalone = 44100;
        
        [Header("iOS")]
        public AudioCompressionFormat audioCompressionFormatOnIOS = AudioCompressionFormat.Vorbis;
        public AudioClipLoadType audioClipsLoadTypeOnIOS = AudioClipLoadType.Streaming;
        public float audioQualityOnIOS = 1.0f;
        public uint audioSampleRateOnIOS = 44100;
        
        [Header("Android")]
        public AudioCompressionFormat audioCompressionFormatOnAndroid = AudioCompressionFormat.Vorbis;
        public AudioClipLoadType audioClipsLoadTypeOnAndroid = AudioClipLoadType.Streaming;
        public float audioQualityOnAndroid = 1.0f;
        public uint audioSampleRateOnAndroid = 44100;
        
        [Header("TVOS")]
        public AudioCompressionFormat audioCompressionFormatOnTVOS = AudioCompressionFormat.Vorbis;
        public AudioClipLoadType audioClipsLoadTypeOnTVOS = AudioClipLoadType.Streaming;
        public float audioQualityOnTVOS = 1.0f;
        public uint audioSampleRateOnTVOS = 44100;
        
        [Header("WebGL")]
        public AudioCompressionFormat audioCompressionFormatOnWebGL = AudioCompressionFormat.AAC;
        public AudioClipLoadType audioClipsLoadTypeOnWebGL = AudioClipLoadType.Streaming;
        public float audioQualityOnWebGL = 1.0f;
        // public uint audioSampleRateOnWebGL = 44100;
    }
}