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
            Custom1,
            Custom2,
            Custom3,
            Unknown
        }
        


        
        void OnPreprocessTexture()
        {
            STAutoFormatConfig formatConfig = ConfigManager.GetGeneralRootConfig();
            
            if (formatConfig.compileTimingTexture == FormatTiming.None)
            {
                return;
            }


            TextureImporter nextTextureImporter = assetImporter as TextureImporter;

  
            if (formatConfig.compileTimingTexture == FormatTiming.FirstOnly
                && nextTextureImporter.importSettingsMissing == false)
            {
                //初回インポート以外ののとき
                //初回以外の場合は、すでに設定されていると思うので、なにも処理しない
                return;
            }

            STRootTextureConfig rootConfig = ConfigManager.GetTextureRootConfig();


            TextureUseKind nextUseKind = SearchUseKind(nextTextureImporter.assetPath);

            switch (nextUseKind)
            {
                case TextureUseKind.NormalUI:
                {
                    //
                    // 通常のUI
                    //
                    SetupConfig(nextTextureImporter,rootConfig.normalUIConfig);
                }
                    break;

                case TextureUseKind.DotUI:
                {
                    //ドットを使ったUI
                    SetupConfig(nextTextureImporter,rootConfig.dotUIConfig);
                }
                    break;

                case TextureUseKind.Unknown:
                {
                    //
                    // その他、すべてのテクスチャ
                    //
                    SetupConfig(nextTextureImporter,rootConfig.defaultConfig);
                }
                    break;
                
                case TextureUseKind.Custom1:
                {
                    //
                    // 
                    //
                    SetupConfig(nextTextureImporter,rootConfig.custom1Config);
                }
                    break;
                
                case TextureUseKind.Custom2:
                {
                    //
                    // 
                    //
                    SetupConfig(nextTextureImporter,rootConfig.custom2Config);
                }
                    break;
                
                case TextureUseKind.Custom3:
                {
                    //
                    // 
                    //
                    SetupConfig(nextTextureImporter,rootConfig.custom3Config);
                }
                    break;
            }
        }

        #region 用途ごとの設定

        private void SetupConfig(TextureImporter textureImporter,STTextureConfig config)
        {
            TextureImporterPlatformSettings nextTextureImporterPlatformStanalone =
                textureImporter.GetPlatformTextureSettings("Standalone");
            TextureImporterPlatformSettings nextTextureImporterPlatformAndroid =
                textureImporter.GetPlatformTextureSettings("Android");
            TextureImporterPlatformSettings nextTextureImporterPlatformiOS =
                textureImporter.GetPlatformTextureSettings("iPhone");
            TextureImporterPlatformSettings nextTextureImporterPlatformtvOS =
                textureImporter.GetPlatformTextureSettings("tvOS");
            TextureImporterPlatformSettings nextTextureImporterPlatformWebGL =
                textureImporter.GetPlatformTextureSettings("Web");

            //透過があるか
            bool isAlpha = textureImporter.DoesSourceTextureHaveAlpha();


            //
            // 通常のUI
            //
            //プラットフォーム共通
            textureImporter.textureType = config.textureImporterType;
            textureImporter.filterMode = config.filterMode;
            textureImporter.maxTextureSize = config.maxTextureSize;
            textureImporter.mipmapEnabled = config.isMipMapEnable;
            textureImporter.alphaIsTransparency = isAlpha;
            
            //Standalone(PC)
            nextTextureImporterPlatformStanalone.overridden = true;
            nextTextureImporterPlatformStanalone.maxTextureSize = config.maxTextureSizeOnStandalone;
            if (isAlpha)
            {
                nextTextureImporterPlatformStanalone.format = config.STANDALONEPlatformFormatAlpha;
            }
            else
            {
                nextTextureImporterPlatformStanalone.format = config.STANDALONEPlatformFormatNoAlpha;
            }
            textureImporter.SetPlatformTextureSettings(nextTextureImporterPlatformStanalone);

            //iOS
            nextTextureImporterPlatformiOS.overridden = true;
            nextTextureImporterPlatformiOS.maxTextureSize = config.maxTextureSizeOnIOS;;
            if (isAlpha)
            {
                nextTextureImporterPlatformiOS.format = config.IOSPlatformFormatAlpha;
            }
            else
            {
                nextTextureImporterPlatformiOS.format = config.IOSPlatformFormatNoAlpha;
            }
            textureImporter.SetPlatformTextureSettings(nextTextureImporterPlatformiOS);

            //Android
            nextTextureImporterPlatformAndroid.overridden = true;
            nextTextureImporterPlatformAndroid.maxTextureSize = config.maxTextureSizeOnAndroid;;
            if (isAlpha)
            {
                nextTextureImporterPlatformAndroid.format = config.ANDROIDPlatformFormatAlpha;
            }
            else
            {
                nextTextureImporterPlatformAndroid.format = config.ANDROIDPlatformFormatNoAlpha;
            }
            textureImporter.SetPlatformTextureSettings(nextTextureImporterPlatformAndroid);

            //TVOS
            nextTextureImporterPlatformtvOS.overridden = true;
            nextTextureImporterPlatformtvOS.maxTextureSize = config.maxTextureSizeOnTVOS;;
            if (isAlpha)
            {
                nextTextureImporterPlatformtvOS.format = config.TVOSPlatformFormatAlpha;
            }
            else
            {
                nextTextureImporterPlatformtvOS.format = config.TVOSPlatformFormatNoAlpha;
            }
            textureImporter.SetPlatformTextureSettings(nextTextureImporterPlatformtvOS);

            //WEBGL
            nextTextureImporterPlatformWebGL.overridden = true;
            nextTextureImporterPlatformWebGL.maxTextureSize = config.maxTextureSizeOnWebGL;
            if (isAlpha)
            {
                nextTextureImporterPlatformWebGL.format =config.WEBGLPlatformFormatAlpha;
            }
            else
            {
                nextTextureImporterPlatformWebGL.format = config.WEBGLPlatformFormatNoAlpha;
            }
            textureImporter.SetPlatformTextureSettings(nextTextureImporterPlatformWebGL);
        }


        #endregion


        private TextureUseKind SearchUseKind(string assetPath)
        {
            STAutoFormatConfig formatConfig = ConfigManager.GetGeneralRootConfig();
            
            if (formatConfig.normalUITextureDirectoryPathList.ToList()
                .Any(directoryPath => assetPath.Contains(directoryPath)))
            {
                return TextureUseKind.NormalUI;
            }

            if (formatConfig.dotUITextureDirectoryPathList.ToList()
                .Any(directoryPath => assetPath.Contains(directoryPath)))
            {
                return TextureUseKind.DotUI;
            }
            
            if (formatConfig.custom1TextureDirectoryPathList.ToList()
                .Any(directoryPath => assetPath.Contains(directoryPath)))
            {
                return TextureUseKind.Custom1;
            }
            
            if (formatConfig.custom2TextureDirectoryPathList.ToList()
                .Any(directoryPath => assetPath.Contains(directoryPath)))
            {
                return TextureUseKind.Custom2;
            }
            
            if (formatConfig.custom3TextureDirectoryPathList.ToList()
                .Any(directoryPath => assetPath.Contains(directoryPath)))
            {
                return TextureUseKind.Custom3;
            }


            return TextureUseKind.Unknown;
        }
    }
}