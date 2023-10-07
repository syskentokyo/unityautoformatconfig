using System.Collections.Generic;
using UnityEditor;
using System.Linq;
using UnityEngine;

namespace SyskenTLib.UnityAutoFormatConfig.Editor
{
    public class VideoAutoFormatPostProcessor : AssetPostprocessor
    {
        private enum VideoUseKind
        {
            Normal,
            Custom1,
            Custom2,
            Custom3,
            Unknown
        }

        //設定の定義

        void OnPreprocessAsset()
        {
            if (assetImporter.ToString().Contains("VideoClipImporter")== false )
            {
                //ビデオ以外だった場合
                return;
            }

            STAutoFormatConfig formatConfig = ConfigManager.GetGeneralRootConfig();
            
            
            VideoClipImporter nextImporter = assetImporter as VideoClipImporter;
            
            if (formatConfig.compileTimingVideo == FormatTiming.None)
            {
                return;
            }

            STRootVideoConfig rootConfig = ConfigManager.GetVideoRootConfig();
            
            if ((formatConfig.compileTimingVideo == FormatTiming.FirstOnly)
                && nextImporter.importSettingsMissing == false)
            {
                //初回インポート以外ののとき
                //初回以外の場合は、すでに設定されていると思うので、なにも処理しない
                return;
            }


            VideoUseKind nextUseKind = SearchUseKind(nextImporter.assetPath);

            switch (nextUseKind)
            {
                case VideoUseKind.Normal:
                {
                    //
                    // 通常
                    //
                    SetupConfig(nextImporter,rootConfig.normalConfig);
                }
                    break;
                
                case VideoUseKind.Unknown:
                {
                    //
                    // その他、すべて
                    //
                    SetupConfig(nextImporter,rootConfig.defaultConfig);
                }
                    break;
                
                case VideoUseKind.Custom1:
                {
                    //
                    // 
                    //
                    SetupConfig(nextImporter,rootConfig.custom1Config);
                }
                    break;
                
                case VideoUseKind.Custom2:
                {
                    //
                    // 
                    //
                    SetupConfig(nextImporter,rootConfig.custom2Config);
                }
                    break;
                

                case VideoUseKind.Custom3:
                {
                    //
                    // 
                    //
                    SetupConfig(nextImporter,rootConfig.custom3Config);
                }
                    break;
            }
        }

        #region 用途ごとの設定

        private void SetupConfig(VideoClipImporter targetImporter,STVideoConfig config)
        {
            

            //
            // Normal
            //
            
            //オリジナル動画の情報
            bool isHasAlpha = targetImporter.sourceHasAlpha;


            //プラットフォーム共通
            targetImporter.importAudio = true;
            targetImporter.keepAlpha = isHasAlpha;//透過があれば、透過設定維持

            //Standalone(PC)
            VideoImporterTargetSettings nextImporterPlatformStanalone  = new VideoImporterTargetSettings();
            nextImporterPlatformStanalone.enableTranscoding = true;
            nextImporterPlatformStanalone.bitrateMode = config.videoBitrateModeOnStandalone;
            nextImporterPlatformStanalone.spatialQuality = config.videoSpatialQualityOnStandalone;
            if (isHasAlpha)
            {
                nextImporterPlatformStanalone.codec = config.enableAlphaViewCodecOnStandalone;
            }
            else
            {
                // 透過なし
                nextImporterPlatformStanalone.codec = config.noAlphaViewCodecOnStandalone;
            }
            
            targetImporter.SetTargetSettings("Standalone",nextImporterPlatformStanalone);

            //iOS
            VideoImporterTargetSettings nextImporterPlatformiOS = new VideoImporterTargetSettings();
            nextImporterPlatformiOS.enableTranscoding = true;
            nextImporterPlatformiOS.bitrateMode = config.videoBitrateModeOniOS;
            nextImporterPlatformiOS.spatialQuality = config.videoSpatialQualityOniOS;
            if (isHasAlpha)
            {
                nextImporterPlatformiOS.codec = config.enableAlphaViewCodecOniOS;
            }
            else
            {
                // 透過なし
                nextImporterPlatformiOS.codec = config.noAlphaViewCodecOniOS;
            }
            
            targetImporter.SetTargetSettings("iPhone",nextImporterPlatformiOS);

            //Android
            VideoImporterTargetSettings nextImporterPlatformAndroid = new VideoImporterTargetSettings();
            nextImporterPlatformAndroid.enableTranscoding = true;
            nextImporterPlatformAndroid.bitrateMode = config.videoBitrateModeOnAndroid;
            nextImporterPlatformAndroid.spatialQuality = config.videoSpatialQualityOnAndroid;
            if (isHasAlpha)
            {
                nextImporterPlatformAndroid.codec = config.enableAlphaViewCodecOnAndroid;
            }
            else
            {
                // 透過なし
                nextImporterPlatformAndroid.codec = config.noAlphaViewCodecOnAndroid;
            }
            
            targetImporter.SetTargetSettings("Android",nextImporterPlatformAndroid);

            //TVOS
            VideoImporterTargetSettings nextImporterPlatformtvOS = new VideoImporterTargetSettings();
            nextImporterPlatformtvOS.enableTranscoding = true;
            nextImporterPlatformtvOS.bitrateMode = config.videoBitrateModeOnTVOS;
            nextImporterPlatformtvOS.spatialQuality = config.videoSpatialQualityOnTVOS;
            if (isHasAlpha)
            {
                nextImporterPlatformtvOS.codec = config.enableAlphaViewCodecOnTVOS;
            }
            else
            {
                // 透過なし
                nextImporterPlatformtvOS.codec = config.noAlphaViewCodecOnTVOS;
            }
            

            targetImporter.SetTargetSettings("tvOS",nextImporterPlatformtvOS);

            //WEBGL
            VideoImporterTargetSettings nextImporterPlatformWebGL = new VideoImporterTargetSettings();
            nextImporterPlatformWebGL.enableTranscoding = true;
            nextImporterPlatformWebGL.bitrateMode = config.videoBitrateModeOnWebGL;
            nextImporterPlatformWebGL.spatialQuality = config.videoSpatialQualityOnWebGL;
            if (isHasAlpha)
            {
                nextImporterPlatformWebGL.codec = config.enableAlphaViewCodecOnWebGL;
            }
            else
            {
                // 透過なし
                nextImporterPlatformWebGL.codec = config.noAlphaViewCodecOnWebGL;
            }
            
            targetImporter.SetTargetSettings("WebGL",nextImporterPlatformWebGL);
        }



        #endregion


        private VideoUseKind SearchUseKind(string assetPath)
        {
            STAutoFormatConfig formatConfig = ConfigManager.GetGeneralRootConfig();
            
            if (formatConfig.normalVideoDirectoryPathList.ToList()
                .Any(directoryPath => assetPath.Contains(directoryPath)))
            {
                return VideoUseKind.Normal;
            }
            
            if (formatConfig.custom1VideoDirectoryPathList.ToList()
                .Any(directoryPath => assetPath.Contains(directoryPath)))
            {
                return VideoUseKind.Custom1;
            }
            
            if (formatConfig.custom2VideoDirectoryPathList.ToList()
                .Any(directoryPath => assetPath.Contains(directoryPath)))
            {
                return VideoUseKind.Custom2;
            }
            
            if (formatConfig.custom3VideoDirectoryPathList.ToList()
                .Any(directoryPath => assetPath.Contains(directoryPath)))
            {
                return VideoUseKind.Custom3;
            }


            return VideoUseKind.Unknown;
        }
    }
}