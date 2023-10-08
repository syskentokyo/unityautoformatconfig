using UnityEditor;
using UnityEngine;

namespace SyskenTLib.UnityAutoFormatConfig{
    // [CreateAssetMenu(fileName = "FILENAME", menuName = "STTextureConfig", order = 0)]
    public class STTextureConfig : ScriptableObject
    {
        [Header("フォーマット")]
        public TextureImporterFormat STANDALONEPlatformFormatAlpha = TextureImporterFormat.BC7;
        public TextureImporterFormat STANDALONEPlatformFormatNoAlpha = TextureImporterFormat.BC7;
        public TextureImporterFormat IOSPlatformFormatAlpha = TextureImporterFormat.ASTC_6x6;
        public TextureImporterFormat IOSPlatformFormatNoAlpha = TextureImporterFormat.ASTC_6x6;
        public TextureImporterFormat ANDROIDPlatformFormatAlpha = TextureImporterFormat.ETC2_RGBA8;
        public TextureImporterFormat ANDROIDPlatformFormatNoAlpha = TextureImporterFormat.ETC2_RGBA8;
        public TextureImporterFormat TVOSPlatformFormatAlpha = TextureImporterFormat.ASTC_6x6;
        public TextureImporterFormat TVOSPlatformFormatNoAlpha = TextureImporterFormat.ASTC_6x6;
        public TextureImporterFormat WEBGLPlatformFormatAlpha = TextureImporterFormat.DXT5;
        public TextureImporterFormat WEBGLPlatformFormatNoAlpha = TextureImporterFormat.DXT1;


        [Header("共通")] 
        public TextureImporterType textureImporterType = TextureImporterType.Sprite;

        public FilterMode filterMode = FilterMode.Bilinear;
        public int maxTextureSize = 1024;
        public bool isMipMapEnable = false;
        
        [Header("Standalone")]
        public int maxTextureSizeOnStandalone = 2048;

        [Header("iOS")]
        public int maxTextureSizeOnIOS = 1024;


        [Header("Android")]
        public int maxTextureSizeOnAndroid = 512;


        [Header("TVOS")]
        public int maxTextureSizeOnTVOS = 1024;
        
        [Header("WebGL")]
        public int maxTextureSizeOnWebGL = 1024;




    }
}