using System.Collections.Generic;
using UnityEditor;
using System.Linq;
using UnityEngine;

namespace SyskenTLib.UnityAutoFormatConfig.Editor
{
    public class TextureAufoFormatPostProcessor : AssetPostprocessor
    {
        private enum TextureUseKind
        {
            NormalUI,
            DotUI,
            Unknown
        }
        
        //設定の定義
        private readonly TextureImporterFormat STANDALONEPlatformFormat = TextureImporterFormat.BC7;
        private readonly TextureImporterFormat IOSPlatformFormat = TextureImporterFormat.ASTC_6x6;
        private readonly TextureImporterFormat ANDROIDPlatformFormat = TextureImporterFormat.ETC2_RGBA8;
        private readonly TextureImporterFormat TVOSPlatformFormat = TextureImporterFormat.ASTC_6x6;
        private readonly TextureImporterFormat WEBGLPlatformFormat = TextureImporterFormat.DXT1;
        
        /// <summary>
        /// 設定変更処理をスキップするか
        /// </summary>
        private bool _isAllSkip = false;

        /// <summary>
        /// 初回インポート時のみ設定を行うかどうか。
        /// </summary>
        private bool _isSkipMoreSecondTime = true;
        
        
        //
        // 用途ごとのフォルダを指定してください。
        //
        private readonly List<string> normalUIDirectoryPathList = new List<string>()
        {
            "Assets/Sample/UI"
        };
        
        private readonly  List<string> dotUIDirectoryPathList = new List<string>()
        {
            "Assets/Sample/DotUI"
        };
        

        void OnPreprocessTexture()
        {
            if (_isAllSkip)
            {
                return;
            }
            
            
            TextureImporter nextTextureImporter = assetImporter as TextureImporter;

            if (_isSkipMoreSecondTime == true 
                &&nextTextureImporter.importSettingsMissing == false)
            {
                //初回インポート以外ののとき
                //初回以外の場合は、すでに設定されていると思うので、なにも処理しない
                return;
            }
            
            
            
            //各プラットフォームごとの設定
            TextureImporterPlatformSettings nextTextureImporterPlatformStanalone    = nextTextureImporter.GetPlatformTextureSettings("Standalone");
            TextureImporterPlatformSettings nextTextureImporterPlatformAndroid    = nextTextureImporter.GetPlatformTextureSettings("Android");
            TextureImporterPlatformSettings nextTextureImporterPlatformiOS   = nextTextureImporter.GetPlatformTextureSettings("iPhone");
            TextureImporterPlatformSettings nextTextureImporterPlatformtvOS   = nextTextureImporter.GetPlatformTextureSettings("tvOS");
            TextureImporterPlatformSettings nextTextureImporterPlatformWebGL   = nextTextureImporter.GetPlatformTextureSettings("Web");

            TextureUseKind nextUseKind = SearchUseKind(nextTextureImporter.assetPath);

            switch (nextUseKind)
            {
                case TextureUseKind.NormalUI:
                {
                    //
                    // 通常のUI
                    //
                    //プラットフォーム共通
                    nextTextureImporter.textureType = TextureImporterType.Sprite;
                    nextTextureImporter.filterMode = FilterMode.Bilinear;
                    nextTextureImporter.maxTextureSize = 1024;
                    nextTextureImporter.mipmapEnabled = false;

                    //Standalone(PC)
                    nextTextureImporterPlatformStanalone.overridden = true;
                    nextTextureImporterPlatformStanalone.maxTextureSize = 2048;
                    nextTextureImporterPlatformStanalone.format = STANDALONEPlatformFormat;
                    nextTextureImporter.SetPlatformTextureSettings(nextTextureImporterPlatformStanalone);
                    
                    //iOS
                    nextTextureImporterPlatformiOS.overridden = true;
                    nextTextureImporterPlatformiOS.maxTextureSize = 1024;
                    nextTextureImporterPlatformiOS.format = IOSPlatformFormat;
                    nextTextureImporter.SetPlatformTextureSettings(nextTextureImporterPlatformiOS);
                    
                    //Android
                    nextTextureImporterPlatformAndroid.overridden = true;
                    nextTextureImporterPlatformAndroid.maxTextureSize = 512;
                    nextTextureImporterPlatformAndroid.format = ANDROIDPlatformFormat;
                    nextTextureImporter.SetPlatformTextureSettings(nextTextureImporterPlatformAndroid);
                    
                    //TVOS
                    nextTextureImporterPlatformtvOS.overridden = true;
                    nextTextureImporterPlatformtvOS.maxTextureSize = 1024;
                    nextTextureImporterPlatformtvOS.format = TVOSPlatformFormat;
                    nextTextureImporter.SetPlatformTextureSettings(nextTextureImporterPlatformtvOS);
                    
                    //WEBGL
                    nextTextureImporterPlatformWebGL.overridden = true;
                    nextTextureImporterPlatformWebGL.maxTextureSize = 1024;
                    nextTextureImporterPlatformWebGL.format = WEBGLPlatformFormat;
                    nextTextureImporter.SetPlatformTextureSettings(nextTextureImporterPlatformWebGL);
                }
                    break;
                
                case TextureUseKind.DotUI:
                {
                    //ドットを使ったUI
                                        //プラットフォーム共通
                    nextTextureImporter.textureType = TextureImporterType.Default;
                    nextTextureImporter.filterMode = FilterMode.Point;
                    nextTextureImporter.maxTextureSize = 1024;
                    nextTextureImporter.mipmapEnabled = true;
                    
                    //Standalone(PC)
                    nextTextureImporterPlatformStanalone.overridden = true;
                    nextTextureImporterPlatformStanalone.maxTextureSize = 2048;
                    nextTextureImporterPlatformStanalone.format = STANDALONEPlatformFormat;
                    nextTextureImporter.SetPlatformTextureSettings(nextTextureImporterPlatformStanalone);
                    
                    //iOS
                    nextTextureImporterPlatformiOS.overridden = true;
                    nextTextureImporterPlatformiOS.maxTextureSize = 1024;
                    nextTextureImporterPlatformiOS.format = IOSPlatformFormat;
                    nextTextureImporter.SetPlatformTextureSettings(nextTextureImporterPlatformiOS);
                    
                    //Android
                    nextTextureImporterPlatformAndroid.overridden = true;
                    nextTextureImporterPlatformAndroid.maxTextureSize = 512;
                    nextTextureImporterPlatformAndroid.format = ANDROIDPlatformFormat;
                    nextTextureImporter.SetPlatformTextureSettings(nextTextureImporterPlatformAndroid);
                    
                    //TVOS
                    nextTextureImporterPlatformtvOS.overridden = true;
                    nextTextureImporterPlatformtvOS.maxTextureSize = 1024;
                    nextTextureImporterPlatformtvOS.format = TVOSPlatformFormat;
                    nextTextureImporter.SetPlatformTextureSettings(nextTextureImporterPlatformtvOS);
                    
                    //WEBGL
                    nextTextureImporterPlatformWebGL.overridden = true;
                    nextTextureImporterPlatformWebGL.maxTextureSize = 1024;
                    nextTextureImporterPlatformWebGL.format = WEBGLPlatformFormat;
                    nextTextureImporter.SetPlatformTextureSettings(nextTextureImporterPlatformWebGL);
                }
                    break;
                
                case TextureUseKind.Unknown:
                {
                                
                    //
                    // その他、すべてのテクスチャ
                    //

                    //プラットフォーム共通
                    nextTextureImporter.textureType = TextureImporterType.Default;
                    nextTextureImporter.filterMode = FilterMode.Bilinear;
                    nextTextureImporter.maxTextureSize = 1024;
                    nextTextureImporter.mipmapEnabled = true;
                    
                    //Standalone(PC)
                    nextTextureImporterPlatformStanalone.overridden = true;
                    nextTextureImporterPlatformStanalone.maxTextureSize = 2048;
                    nextTextureImporterPlatformStanalone.format = STANDALONEPlatformFormat;
                    nextTextureImporter.SetPlatformTextureSettings(nextTextureImporterPlatformStanalone);
                    
                    //iOS
                    nextTextureImporterPlatformiOS.overridden = true;
                    nextTextureImporterPlatformiOS.maxTextureSize = 1024;
                    nextTextureImporterPlatformiOS.format = IOSPlatformFormat;
                    nextTextureImporter.SetPlatformTextureSettings(nextTextureImporterPlatformiOS);
                    
                    //Android
                    nextTextureImporterPlatformAndroid.overridden = true;
                    nextTextureImporterPlatformAndroid.maxTextureSize = 512;
                    nextTextureImporterPlatformAndroid.format = ANDROIDPlatformFormat;
                    nextTextureImporter.SetPlatformTextureSettings(nextTextureImporterPlatformAndroid);
                    
                    //TVOS
                    nextTextureImporterPlatformtvOS.overridden = true;
                    nextTextureImporterPlatformtvOS.maxTextureSize = 1024;
                    nextTextureImporterPlatformtvOS.format = TVOSPlatformFormat;
                    nextTextureImporter.SetPlatformTextureSettings(nextTextureImporterPlatformtvOS);
                    
                    //WEBGL
                    nextTextureImporterPlatformWebGL.overridden = true;
                    nextTextureImporterPlatformWebGL.maxTextureSize = 1024;
                    nextTextureImporterPlatformWebGL.format = WEBGLPlatformFormat;
                    nextTextureImporter.SetPlatformTextureSettings(nextTextureImporterPlatformWebGL);

                }
                    break;

            }
            
            
            

            
        }



        private TextureUseKind SearchUseKind(string assetPath)
        {
            if (normalUIDirectoryPathList.ToList()
                .Any(directoryPath => assetPath.Contains(directoryPath)))
            {
                return TextureUseKind.NormalUI;
            }
            
            if (dotUIDirectoryPathList.ToList()
                .Any(directoryPath => assetPath.Contains(directoryPath)))
            {
                return TextureUseKind.DotUI;
            }

            




            return TextureUseKind.Unknown;
        }


    }
}