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

        //設定の定義
        private readonly TextureImporterFormat STANDALONEPlatformFormatAlpha = TextureImporterFormat.BC7;
        private readonly TextureImporterFormat STANDALONEPlatformFormatNoAlpha = TextureImporterFormat.BC7;
        private readonly TextureImporterFormat IOSPlatformFormatAlpha = TextureImporterFormat.ASTC_6x6;
        private readonly TextureImporterFormat IOSPlatformFormatNoAlpha = TextureImporterFormat.ASTC_6x6;
        private readonly TextureImporterFormat ANDROIDPlatformFormatAlpha = TextureImporterFormat.ETC2_RGBA8;
        private readonly TextureImporterFormat ANDROIDPlatformFormatNoAlpha = TextureImporterFormat.ETC2_RGBA8;
        private readonly TextureImporterFormat TVOSPlatformFormatAlpha = TextureImporterFormat.ASTC_6x6;
        private readonly TextureImporterFormat TVOSPlatformFormatNoAlpha = TextureImporterFormat.ASTC_6x6;
        private readonly TextureImporterFormat WEBGLPlatformFormatAlpha = TextureImporterFormat.DXT5;
        private readonly TextureImporterFormat WEBGLPlatformFormatNoAlpha = TextureImporterFormat.DXT1;


        
        void OnPreprocessTexture()
        {
            STAutoFormatConfig formatConfig = ConfigManager.GetConfig();
            
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


            TextureUseKind nextUseKind = SearchUseKind(nextTextureImporter.assetPath);

            switch (nextUseKind)
            {
                case TextureUseKind.NormalUI:
                {
                    //
                    // 通常のUI
                    //
                    SetupNormalUI(nextTextureImporter);
                }
                    break;

                case TextureUseKind.DotUI:
                {
                    //ドットを使ったUI
                    SetupDotUI(nextTextureImporter);
                }
                    break;

                case TextureUseKind.Unknown:
                {
                    //
                    // その他、すべてのテクスチャ
                    //
                    SetupOther(nextTextureImporter);
                }
                    break;
                
                case TextureUseKind.Custom1:
                {
                    //
                    // 
                    //
                    SetupCustom1(nextTextureImporter);
                }
                    break;
                
                case TextureUseKind.Custom2:
                {
                    //
                    // 
                    //
                    SetupCustom2(nextTextureImporter);
                }
                    break;
                
                case TextureUseKind.Custom3:
                {
                    //
                    // 
                    //
                    SetupCustom3(nextTextureImporter);
                }
                    break;
            }
        }

        #region 用途ごとの設定

        private void SetupNormalUI(TextureImporter textureImporter)
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
            textureImporter.textureType = TextureImporterType.Sprite;
            textureImporter.filterMode = FilterMode.Bilinear;
            textureImporter.maxTextureSize = 1024;
            textureImporter.mipmapEnabled = false;
            textureImporter.alphaIsTransparency = isAlpha;
            
            //Standalone(PC)
            nextTextureImporterPlatformStanalone.overridden = true;
            nextTextureImporterPlatformStanalone.maxTextureSize = 2048;
            if (isAlpha)
            {
                nextTextureImporterPlatformStanalone.format = STANDALONEPlatformFormatAlpha;
            }
            else
            {
                nextTextureImporterPlatformStanalone.format = STANDALONEPlatformFormatNoAlpha;
            }
            textureImporter.SetPlatformTextureSettings(nextTextureImporterPlatformStanalone);

            //iOS
            nextTextureImporterPlatformiOS.overridden = true;
            nextTextureImporterPlatformiOS.maxTextureSize = 1024;
            if (isAlpha)
            {
                nextTextureImporterPlatformiOS.format = IOSPlatformFormatAlpha;
            }
            else
            {
                nextTextureImporterPlatformiOS.format = IOSPlatformFormatNoAlpha;
            }
            textureImporter.SetPlatformTextureSettings(nextTextureImporterPlatformiOS);

            //Android
            nextTextureImporterPlatformAndroid.overridden = true;
            nextTextureImporterPlatformAndroid.maxTextureSize = 512;
            if (isAlpha)
            {
                nextTextureImporterPlatformAndroid.format = ANDROIDPlatformFormatAlpha;
            }
            else
            {
                nextTextureImporterPlatformAndroid.format = ANDROIDPlatformFormatNoAlpha;
            }
            textureImporter.SetPlatformTextureSettings(nextTextureImporterPlatformAndroid);

            //TVOS
            nextTextureImporterPlatformtvOS.overridden = true;
            nextTextureImporterPlatformtvOS.maxTextureSize = 1024;
            if (isAlpha)
            {
                nextTextureImporterPlatformtvOS.format = TVOSPlatformFormatAlpha;
            }
            else
            {
                nextTextureImporterPlatformtvOS.format = TVOSPlatformFormatNoAlpha;
            }
            textureImporter.SetPlatformTextureSettings(nextTextureImporterPlatformtvOS);

            //WEBGL
            nextTextureImporterPlatformWebGL.overridden = true;
            nextTextureImporterPlatformWebGL.maxTextureSize = 1024;
            if (isAlpha)
            {
                nextTextureImporterPlatformWebGL.format = WEBGLPlatformFormatAlpha;
            }
            else
            {
                nextTextureImporterPlatformWebGL.format = WEBGLPlatformFormatNoAlpha;
            }
            textureImporter.SetPlatformTextureSettings(nextTextureImporterPlatformWebGL);
        }


        private void SetupDotUI(TextureImporter textureImporter)
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

            ////プラットフォーム共通
            textureImporter.textureType = TextureImporterType.Default;
            textureImporter.filterMode = FilterMode.Point;
            textureImporter.maxTextureSize = 1024;
            textureImporter.mipmapEnabled = true;
            textureImporter.alphaIsTransparency = isAlpha;

            //Standalone(PC)
            nextTextureImporterPlatformStanalone.overridden = true;
            nextTextureImporterPlatformStanalone.maxTextureSize = 2048;
            if (isAlpha)
            {
                nextTextureImporterPlatformStanalone.format = STANDALONEPlatformFormatAlpha;
            }
            else
            {
                nextTextureImporterPlatformStanalone.format = STANDALONEPlatformFormatNoAlpha;
            }
            textureImporter.SetPlatformTextureSettings(nextTextureImporterPlatformStanalone);

            //iOS
            nextTextureImporterPlatformiOS.overridden = true;
            nextTextureImporterPlatformiOS.maxTextureSize = 1024;
            if (isAlpha)
            {
                nextTextureImporterPlatformiOS.format = IOSPlatformFormatAlpha;
            }
            else
            {
                nextTextureImporterPlatformiOS.format = IOSPlatformFormatNoAlpha;
            }
            textureImporter.SetPlatformTextureSettings(nextTextureImporterPlatformiOS);

            //Android
            nextTextureImporterPlatformAndroid.overridden = true;
            nextTextureImporterPlatformAndroid.maxTextureSize = 512;
            if (isAlpha)
            {
                nextTextureImporterPlatformAndroid.format = ANDROIDPlatformFormatAlpha;
            }
            else
            {
                nextTextureImporterPlatformAndroid.format = ANDROIDPlatformFormatNoAlpha;
            }
            textureImporter.SetPlatformTextureSettings(nextTextureImporterPlatformAndroid);

            //TVOS
            nextTextureImporterPlatformtvOS.overridden = true;
            nextTextureImporterPlatformtvOS.maxTextureSize = 1024;
            if (isAlpha)
            {
                nextTextureImporterPlatformtvOS.format = TVOSPlatformFormatAlpha;
            }
            else
            {
                nextTextureImporterPlatformtvOS.format = TVOSPlatformFormatNoAlpha;
            }
            textureImporter.SetPlatformTextureSettings(nextTextureImporterPlatformtvOS);

            //WEBGL
            nextTextureImporterPlatformWebGL.overridden = true;
            nextTextureImporterPlatformWebGL.maxTextureSize = 1024;
            if (isAlpha)
            {
                nextTextureImporterPlatformWebGL.format = WEBGLPlatformFormatAlpha;
            }
            else
            {
                nextTextureImporterPlatformWebGL.format = WEBGLPlatformFormatNoAlpha;
            }
            textureImporter.SetPlatformTextureSettings(nextTextureImporterPlatformWebGL);
        }


        private void SetupOther(TextureImporter textureImporter)
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

            //プラットフォーム共通
            textureImporter.textureType = TextureImporterType.Default;
            textureImporter.filterMode = FilterMode.Bilinear;
            textureImporter.maxTextureSize = 1024;
            textureImporter.mipmapEnabled = true;
            textureImporter.alphaIsTransparency = isAlpha;

            
            //Standalone(PC)
            nextTextureImporterPlatformStanalone.overridden = true;
            nextTextureImporterPlatformStanalone.maxTextureSize = 2048;
            if (isAlpha)
            {
                nextTextureImporterPlatformStanalone.format = STANDALONEPlatformFormatAlpha;
            }
            else
            {
                nextTextureImporterPlatformStanalone.format = STANDALONEPlatformFormatNoAlpha;
            }
            textureImporter.SetPlatformTextureSettings(nextTextureImporterPlatformStanalone);

            //iOS
            nextTextureImporterPlatformiOS.overridden = true;
            nextTextureImporterPlatformiOS.maxTextureSize = 1024;
            
            if (isAlpha)
            {
                nextTextureImporterPlatformiOS.format = IOSPlatformFormatAlpha;
            }
            else
            {
                nextTextureImporterPlatformiOS.format = IOSPlatformFormatNoAlpha;
            }
            textureImporter.SetPlatformTextureSettings(nextTextureImporterPlatformiOS);

            //Android
            nextTextureImporterPlatformAndroid.overridden = true;
            nextTextureImporterPlatformAndroid.maxTextureSize = 512;
            
            if (isAlpha)
            {
                nextTextureImporterPlatformAndroid.format = ANDROIDPlatformFormatAlpha;
            }
            else
            {
                nextTextureImporterPlatformAndroid.format = ANDROIDPlatformFormatNoAlpha;
            }
            textureImporter.SetPlatformTextureSettings(nextTextureImporterPlatformAndroid);

            //TVOS
            nextTextureImporterPlatformtvOS.overridden = true;
            nextTextureImporterPlatformtvOS.maxTextureSize = 1024;

            if (isAlpha)
            {
                nextTextureImporterPlatformtvOS.format = TVOSPlatformFormatAlpha;
            }
            else
            {
                nextTextureImporterPlatformtvOS.format = TVOSPlatformFormatNoAlpha;
            }
            textureImporter.SetPlatformTextureSettings(nextTextureImporterPlatformtvOS);

            //WEBGL
            nextTextureImporterPlatformWebGL.overridden = true;
            nextTextureImporterPlatformWebGL.maxTextureSize = 1024;
            if (isAlpha)
            {
                nextTextureImporterPlatformWebGL.format = WEBGLPlatformFormatAlpha;
            }
            else
            {
                nextTextureImporterPlatformWebGL.format = WEBGLPlatformFormatNoAlpha;
            }
            textureImporter.SetPlatformTextureSettings(nextTextureImporterPlatformWebGL);
        }
        
        private void SetupCustom1(TextureImporter textureImporter)
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

            //プラットフォーム共通
            textureImporter.textureType = TextureImporterType.Default;
            textureImporter.filterMode = FilterMode.Bilinear;
            textureImporter.maxTextureSize = 1024;
            textureImporter.mipmapEnabled = true; 
            textureImporter.alphaIsTransparency = isAlpha;

            //Standalone(PC)
            nextTextureImporterPlatformStanalone.overridden = true;
            nextTextureImporterPlatformStanalone.maxTextureSize = 2048;
            if (isAlpha)
            {
                nextTextureImporterPlatformStanalone.format = STANDALONEPlatformFormatAlpha;
            }
            else
            {
                nextTextureImporterPlatformStanalone.format = STANDALONEPlatformFormatNoAlpha;
            }
            textureImporter.SetPlatformTextureSettings(nextTextureImporterPlatformStanalone);

            //iOS
            nextTextureImporterPlatformiOS.overridden = true;
            nextTextureImporterPlatformiOS.maxTextureSize = 1024;
            
            if (isAlpha)
            {
                nextTextureImporterPlatformiOS.format = IOSPlatformFormatAlpha;
            }
            else
            {
                nextTextureImporterPlatformiOS.format = IOSPlatformFormatNoAlpha;
            }
            textureImporter.SetPlatformTextureSettings(nextTextureImporterPlatformiOS);

            //Android
            nextTextureImporterPlatformAndroid.overridden = true;
            nextTextureImporterPlatformAndroid.maxTextureSize = 512;
            
            if (isAlpha)
            {
                nextTextureImporterPlatformAndroid.format = ANDROIDPlatformFormatAlpha;
            }
            else
            {
                nextTextureImporterPlatformAndroid.format = ANDROIDPlatformFormatNoAlpha;
            }
            textureImporter.SetPlatformTextureSettings(nextTextureImporterPlatformAndroid);

            //TVOS
            nextTextureImporterPlatformtvOS.overridden = true;
            nextTextureImporterPlatformtvOS.maxTextureSize = 1024;

            if (isAlpha)
            {
                nextTextureImporterPlatformtvOS.format = TVOSPlatformFormatAlpha;
            }
            else
            {
                nextTextureImporterPlatformtvOS.format = TVOSPlatformFormatNoAlpha;
            }
            textureImporter.SetPlatformTextureSettings(nextTextureImporterPlatformtvOS);

            //WEBGL
            nextTextureImporterPlatformWebGL.overridden = true;
            nextTextureImporterPlatformWebGL.maxTextureSize = 1024;
            if (isAlpha)
            {
                nextTextureImporterPlatformWebGL.format = WEBGLPlatformFormatAlpha;
            }
            else
            {
                nextTextureImporterPlatformWebGL.format = WEBGLPlatformFormatNoAlpha;
            }
            textureImporter.SetPlatformTextureSettings(nextTextureImporterPlatformWebGL);
        }

        
        private void SetupCustom2(TextureImporter textureImporter)
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

            //プラットフォーム共通
            textureImporter.textureType = TextureImporterType.Default;
            textureImporter.filterMode = FilterMode.Bilinear;
            textureImporter.maxTextureSize = 1024;
            textureImporter.mipmapEnabled = true; 
            textureImporter.alphaIsTransparency = isAlpha;

            //Standalone(PC)
            nextTextureImporterPlatformStanalone.overridden = true;
            nextTextureImporterPlatformStanalone.maxTextureSize = 2048;
            if (isAlpha)
            {
                nextTextureImporterPlatformStanalone.format = STANDALONEPlatformFormatAlpha;
            }
            else
            {
                nextTextureImporterPlatformStanalone.format = STANDALONEPlatformFormatNoAlpha;
            }
            textureImporter.SetPlatformTextureSettings(nextTextureImporterPlatformStanalone);

            //iOS
            nextTextureImporterPlatformiOS.overridden = true;
            nextTextureImporterPlatformiOS.maxTextureSize = 1024;
            
            if (isAlpha)
            {
                nextTextureImporterPlatformiOS.format = IOSPlatformFormatAlpha;
            }
            else
            {
                nextTextureImporterPlatformiOS.format = IOSPlatformFormatNoAlpha;
            }
            textureImporter.SetPlatformTextureSettings(nextTextureImporterPlatformiOS);

            //Android
            nextTextureImporterPlatformAndroid.overridden = true;
            nextTextureImporterPlatformAndroid.maxTextureSize = 512;
            
            if (isAlpha)
            {
                nextTextureImporterPlatformAndroid.format = ANDROIDPlatformFormatAlpha;
            }
            else
            {
                nextTextureImporterPlatformAndroid.format = ANDROIDPlatformFormatNoAlpha;
            }
            textureImporter.SetPlatformTextureSettings(nextTextureImporterPlatformAndroid);

            //TVOS
            nextTextureImporterPlatformtvOS.overridden = true;
            nextTextureImporterPlatformtvOS.maxTextureSize = 1024;

            if (isAlpha)
            {
                nextTextureImporterPlatformtvOS.format = TVOSPlatformFormatAlpha;
            }
            else
            {
                nextTextureImporterPlatformtvOS.format = TVOSPlatformFormatNoAlpha;
            }
            textureImporter.SetPlatformTextureSettings(nextTextureImporterPlatformtvOS);

            //WEBGL
            nextTextureImporterPlatformWebGL.overridden = true;
            nextTextureImporterPlatformWebGL.maxTextureSize = 1024;
            if (isAlpha)
            {
                nextTextureImporterPlatformWebGL.format = WEBGLPlatformFormatAlpha;
            }
            else
            {
                nextTextureImporterPlatformWebGL.format = WEBGLPlatformFormatNoAlpha;
            }
            textureImporter.SetPlatformTextureSettings(nextTextureImporterPlatformWebGL);
        }

        
        private void SetupCustom3(TextureImporter textureImporter)
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

            //プラットフォーム共通
            textureImporter.textureType = TextureImporterType.Default;
            textureImporter.filterMode = FilterMode.Bilinear;
            textureImporter.maxTextureSize = 1024;
            textureImporter.mipmapEnabled = true;
            textureImporter.alphaIsTransparency = isAlpha;

            //Standalone(PC)
            nextTextureImporterPlatformStanalone.overridden = true;
            nextTextureImporterPlatformStanalone.maxTextureSize = 2048;
            if (isAlpha)
            {
                nextTextureImporterPlatformStanalone.format = STANDALONEPlatformFormatAlpha;
            }
            else
            {
                nextTextureImporterPlatformStanalone.format = STANDALONEPlatformFormatNoAlpha;
            }
            textureImporter.SetPlatformTextureSettings(nextTextureImporterPlatformStanalone);

            //iOS
            nextTextureImporterPlatformiOS.overridden = true;
            nextTextureImporterPlatformiOS.maxTextureSize = 1024;
            
            if (isAlpha)
            {
                nextTextureImporterPlatformiOS.format = IOSPlatformFormatAlpha;
            }
            else
            {
                nextTextureImporterPlatformiOS.format = IOSPlatformFormatNoAlpha;
            }
            textureImporter.SetPlatformTextureSettings(nextTextureImporterPlatformiOS);

            //Android
            nextTextureImporterPlatformAndroid.overridden = true;
            nextTextureImporterPlatformAndroid.maxTextureSize = 512;
            
            if (isAlpha)
            {
                nextTextureImporterPlatformAndroid.format = ANDROIDPlatformFormatAlpha;
            }
            else
            {
                nextTextureImporterPlatformAndroid.format = ANDROIDPlatformFormatNoAlpha;
            }
            textureImporter.SetPlatformTextureSettings(nextTextureImporterPlatformAndroid);

            //TVOS
            nextTextureImporterPlatformtvOS.overridden = true;
            nextTextureImporterPlatformtvOS.maxTextureSize = 1024;

            if (isAlpha)
            {
                nextTextureImporterPlatformtvOS.format = TVOSPlatformFormatAlpha;
            }
            else
            {
                nextTextureImporterPlatformtvOS.format = TVOSPlatformFormatNoAlpha;
            }
            textureImporter.SetPlatformTextureSettings(nextTextureImporterPlatformtvOS);

            //WEBGL
            nextTextureImporterPlatformWebGL.overridden = true;
            nextTextureImporterPlatformWebGL.maxTextureSize = 1024;
            if (isAlpha)
            {
                nextTextureImporterPlatformWebGL.format = WEBGLPlatformFormatAlpha;
            }
            else
            {
                nextTextureImporterPlatformWebGL.format = WEBGLPlatformFormatNoAlpha;
            }
            textureImporter.SetPlatformTextureSettings(nextTextureImporterPlatformWebGL);
        }


        #endregion


        private TextureUseKind SearchUseKind(string assetPath)
        {
            STAutoFormatConfig formatConfig = ConfigManager.GetConfig();
            
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