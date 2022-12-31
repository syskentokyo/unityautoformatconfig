using System.Collections.Generic;

namespace SyskenTLib.UnityAutoFormatConfig.Editor
{
    public static class CommonConfig
    {
        //
        // テクスチャ
        //
        public static readonly List<string> textureNormalUIDirectoryPathList = new List<string>()
        {
            "Assets/Sample/UI",
            "Assets/Sample2/UI"
        };

        public  static readonly List<string> textureDotUIDirectoryPathList = new List<string>()
        {
            "Assets/Sample/DotUI",
            "Assets/Sample2/DotUI"
        };
        
        
        //
        // Audio
        //
        public static readonly List<string> audioBGMDirectoryPathList = new List<string>()
        {
            "Assets/Sample/Sound/BGM"
        };

        public  static readonly List<string> audioSEDirectoryPathList = new List<string>()
        {
            "Assets/Sample/Sound/SE"
        };
    }
}