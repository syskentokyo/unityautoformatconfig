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

        private readonly List<string> dotUIDirectoryPathList = new List<string>()
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
                nextTextureImporterPlatformStanalone.format = IOSPlatformFormatAlpha;
            }
            else
            {
                nextTextureImporterPlatformStanalone.format = IOSPlatformFormatNoAlpha;
            }
            textureImporter.SetPlatformTextureSettings(nextTextureImporterPlatformiOS);

            //Android
            nextTextureImporterPlatformAndroid.overridden = true;
            nextTextureImporterPlatformAndroid.maxTextureSize = 512;
            if (isAlpha)
            {
                nextTextureImporterPlatformStanalone.format = ANDROIDPlatformFormatAlpha;
            }
            else
            {
                nextTextureImporterPlatformStanalone.format = ANDROIDPlatformFormatNoAlpha;
            }
            textureImporter.SetPlatformTextureSettings(nextTextureImporterPlatformAndroid);

            //TVOS
            nextTextureImporterPlatformtvOS.overridden = true;
            nextTextureImporterPlatformtvOS.maxTextureSize = 1024;
            if (isAlpha)
            {
                nextTextureImporterPlatformStanalone.format = TVOSPlatformFormatAlpha;
            }
            else
            {
                nextTextureImporterPlatformStanalone.format = TVOSPlatformFormatNoAlpha;
            }
            textureImporter.SetPlatformTextureSettings(nextTextureImporterPlatformtvOS);

            //WEBGL
            nextTextureImporterPlatformWebGL.overridden = true;
            nextTextureImporterPlatformWebGL.maxTextureSize = 1024;
            if (isAlpha)
            {
                nextTextureImporterPlatformStanalone.format = WEBGLPlatformFormatAlpha;
            }
            else
            {
                nextTextureImporterPlatformStanalone.format = WEBGLPlatformFormatNoAlpha;
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
                nextTextureImporterPlatformStanalone.format = IOSPlatformFormatAlpha;
            }
            else
            {
                nextTextureImporterPlatformStanalone.format = IOSPlatformFormatNoAlpha;
            }
            textureImporter.SetPlatformTextureSettings(nextTextureImporterPlatformiOS);

            //Android
            nextTextureImporterPlatformAndroid.overridden = true;
            nextTextureImporterPlatformAndroid.maxTextureSize = 512;
            if (isAlpha)
            {
                nextTextureImporterPlatformStanalone.format = ANDROIDPlatformFormatAlpha;
            }
            else
            {
                nextTextureImporterPlatformStanalone.format = ANDROIDPlatformFormatNoAlpha;
            }
            textureImporter.SetPlatformTextureSettings(nextTextureImporterPlatformAndroid);

            //TVOS
            nextTextureImporterPlatformtvOS.overridden = true;
            nextTextureImporterPlatformtvOS.maxTextureSize = 1024;
            if (isAlpha)
            {
                nextTextureImporterPlatformStanalone.format = TVOSPlatformFormatAlpha;
            }
            else
            {
                nextTextureImporterPlatformStanalone.format = TVOSPlatformFormatNoAlpha;
            }
            textureImporter.SetPlatformTextureSettings(nextTextureImporterPlatformtvOS);

            //WEBGL
            nextTextureImporterPlatformWebGL.overridden = true;
            nextTextureImporterPlatformWebGL.maxTextureSize = 1024;
            if (isAlpha)
            {
                nextTextureImporterPlatformStanalone.format = WEBGLPlatformFormatAlpha;
            }
            else
            {
                nextTextureImporterPlatformStanalone.format = WEBGLPlatformFormatNoAlpha;
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
                nextTextureImporterPlatformStanalone.format = IOSPlatformFormatAlpha;
            }
            else
            {
                nextTextureImporterPlatformStanalone.format = IOSPlatformFormatNoAlpha;
            }
            textureImporter.SetPlatformTextureSettings(nextTextureImporterPlatformiOS);

            //Android
            nextTextureImporterPlatformAndroid.overridden = true;
            nextTextureImporterPlatformAndroid.maxTextureSize = 512;
            
            if (isAlpha)
            {
                nextTextureImporterPlatformStanalone.format = ANDROIDPlatformFormatAlpha;
            }
            else
            {
                nextTextureImporterPlatformStanalone.format = ANDROIDPlatformFormatNoAlpha;
            }
            textureImporter.SetPlatformTextureSettings(nextTextureImporterPlatformAndroid);

            //TVOS
            nextTextureImporterPlatformtvOS.overridden = true;
            nextTextureImporterPlatformtvOS.maxTextureSize = 1024;

            if (isAlpha)
            {
                nextTextureImporterPlatformStanalone.format = TVOSPlatformFormatAlpha;
            }
            else
            {
                nextTextureImporterPlatformStanalone.format = TVOSPlatformFormatNoAlpha;
            }
            textureImporter.SetPlatformTextureSettings(nextTextureImporterPlatformtvOS);

            //WEBGL
            nextTextureImporterPlatformWebGL.overridden = true;
            nextTextureImporterPlatformWebGL.maxTextureSize = 1024;
            if (isAlpha)
            {
                nextTextureImporterPlatformStanalone.format = WEBGLPlatformFormatAlpha;
            }
            else
            {
                nextTextureImporterPlatformStanalone.format = WEBGLPlatformFormatNoAlpha;
            }
            textureImporter.SetPlatformTextureSettings(nextTextureImporterPlatformWebGL);
        }

        #endregion


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