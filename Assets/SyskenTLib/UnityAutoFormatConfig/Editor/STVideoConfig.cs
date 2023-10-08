using UnityEditor;
using UnityEngine;

namespace SyskenTLib.UnityAutoFormatConfig
{
    // [CreateAssetMenu(fileName = "FILENAME", menuName = "STVideoConfig", order = 0)]
    public class STVideoConfig : ScriptableObject
    {
        [Header("Standalone")] public VideoBitrateMode videoBitrateModeOnStandalone = VideoBitrateMode.Medium;
        public VideoSpatialQuality videoSpatialQualityOnStandalone = VideoSpatialQuality.MediumSpatialQuality;
        public VideoCodec noAlphaViewCodecOnStandalone = VideoCodec.H264;
        public VideoCodec enableAlphaViewCodecOnStandalone = VideoCodec.VP8;
        
        [Header("iOS")] public VideoBitrateMode videoBitrateModeOniOS = VideoBitrateMode.Medium;
        public VideoSpatialQuality videoSpatialQualityOniOS = VideoSpatialQuality.MediumSpatialQuality;
        public VideoCodec noAlphaViewCodecOniOS = VideoCodec.H264;
        public VideoCodec enableAlphaViewCodecOniOS = VideoCodec.VP8;
        
        [Header("Android")] public VideoBitrateMode videoBitrateModeOnAndroid = VideoBitrateMode.Medium;
        public VideoSpatialQuality videoSpatialQualityOnAndroid = VideoSpatialQuality.MediumSpatialQuality;
        public VideoCodec noAlphaViewCodecOnAndroid = VideoCodec.H264;
        public VideoCodec enableAlphaViewCodecOnAndroid = VideoCodec.VP8;
        
        [Header("tvOS")] public VideoBitrateMode videoBitrateModeOnTVOS = VideoBitrateMode.Medium;
        public VideoSpatialQuality videoSpatialQualityOnTVOS = VideoSpatialQuality.MediumSpatialQuality;
        public VideoCodec noAlphaViewCodecOnTVOS = VideoCodec.H264;
        public VideoCodec enableAlphaViewCodecOnTVOS = VideoCodec.VP8;
        
        [Header("WebGL")] public VideoBitrateMode videoBitrateModeOnWebGL = VideoBitrateMode.Medium;
        public VideoSpatialQuality videoSpatialQualityOnWebGL = VideoSpatialQuality.MediumSpatialQuality;
        public VideoCodec noAlphaViewCodecOnWebGL = VideoCodec.H264;
        public VideoCodec enableAlphaViewCodecOnWebGL = VideoCodec.VP8;


    }
}