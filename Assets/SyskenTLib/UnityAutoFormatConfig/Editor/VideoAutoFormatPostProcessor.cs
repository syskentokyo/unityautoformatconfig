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

        /// <summary>
        /// セーブデータ管理
        /// </summary>
        private SaveDataManager _saveDataManager = new SaveDataManager();
        
        
        void OnPreprocessAsset()
        {
            if (assetImporter.ToString().Contains("VideoClipImporter")== false )
            {
                //ビデオ以外だった場合
                return;
            }
            
            
            VideoClipImporter nextImporter = assetImporter as VideoClipImporter;
            
            bool _isAllSkip = _saveDataManager.ReadUserConfigBool(CommonDefine.isAllSkipVideoKEY);
            if (_isAllSkip)
            {
                return;
            }


            
            bool _isEveryImportTimeChangeConfig =
                _saveDataManager.ReadUserConfigBool(CommonDefine._isEveryImportTimeChangeConfigVideoKey);

            if (_isEveryImportTimeChangeConfig == false
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
                    SetupNormal(nextImporter);
                }
                    break;
                
                case VideoUseKind.Unknown:
                {
                    //
                    // その他、すべて
                    //
                    SetupOther(nextImporter);
                }
                    break;
                
                case VideoUseKind.Custom1:
                {
                    //
                    // 
                    //
                    SetupCustom1(nextImporter);
                }
                    break;
                
                case VideoUseKind.Custom2:
                {
                    //
                    // 
                    //
                    SetupCustom2(nextImporter);
                }
                    break;
                

                case VideoUseKind.Custom3:
                {
                    //
                    // 
                    //
                    SetupCustom3(nextImporter);
                }
                    break;
            }
        }

        #region 用途ごとの設定

        private void SetupNormal(VideoClipImporter targetImporter)
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
            nextImporterPlatformStanalone.bitrateMode = VideoBitrateMode.Medium;
            nextImporterPlatformStanalone.spatialQuality = VideoSpatialQuality.MediumSpatialQuality;
            if (isHasAlpha)
            {
                nextImporterPlatformStanalone.codec = VideoCodec.VP8;
            }
            else
            {
                // 透過なし
                nextImporterPlatformStanalone.codec = VideoCodec.H264;
            }
            
            targetImporter.SetTargetSettings("Standalone",nextImporterPlatformStanalone);

            //iOS
            VideoImporterTargetSettings nextImporterPlatformiOS = new VideoImporterTargetSettings();
            nextImporterPlatformiOS.enableTranscoding = true;
            nextImporterPlatformiOS.bitrateMode = VideoBitrateMode.Medium;
            nextImporterPlatformiOS.spatialQuality = VideoSpatialQuality.MediumSpatialQuality;
            if (isHasAlpha)
            {
                nextImporterPlatformiOS.codec = VideoCodec.VP8;
            }
            else
            {
                // 透過なし
                nextImporterPlatformiOS.codec = VideoCodec.H264;
            }
            
            targetImporter.SetTargetSettings("iPhone",nextImporterPlatformiOS);

            //Android
            VideoImporterTargetSettings nextImporterPlatformAndroid = new VideoImporterTargetSettings();
            nextImporterPlatformAndroid.enableTranscoding = true;
            nextImporterPlatformAndroid.bitrateMode = VideoBitrateMode.Medium;
            nextImporterPlatformAndroid.spatialQuality = VideoSpatialQuality.MediumSpatialQuality;
            if (isHasAlpha)
            {
                nextImporterPlatformAndroid.codec = VideoCodec.VP8;
            }
            else
            {
                // 透過なし
                nextImporterPlatformAndroid.codec = VideoCodec.H264;
            }
            
            targetImporter.SetTargetSettings("Android",nextImporterPlatformAndroid);

            //TVOS
            VideoImporterTargetSettings nextImporterPlatformtvOS = new VideoImporterTargetSettings();
            nextImporterPlatformtvOS.enableTranscoding = true;
            nextImporterPlatformtvOS.bitrateMode = VideoBitrateMode.Medium;
            nextImporterPlatformtvOS.spatialQuality = VideoSpatialQuality.MediumSpatialQuality;
            if (isHasAlpha)
            {
                nextImporterPlatformtvOS.codec = VideoCodec.VP8;
            }
            else
            {
                // 透過なし
                nextImporterPlatformtvOS.codec = VideoCodec.H264;
            }
            

            targetImporter.SetTargetSettings("tvOS",nextImporterPlatformtvOS);

            //WEBGL
            VideoImporterTargetSettings nextImporterPlatformWebGL = new VideoImporterTargetSettings();
            nextImporterPlatformWebGL.enableTranscoding = true;
            nextImporterPlatformWebGL.bitrateMode = VideoBitrateMode.Medium;
            nextImporterPlatformWebGL.spatialQuality = VideoSpatialQuality.MediumSpatialQuality;
            if (isHasAlpha)
            {
                nextImporterPlatformWebGL.codec = VideoCodec.VP8;
            }
            else
            {
                // 透過なし
                nextImporterPlatformWebGL.codec = VideoCodec.H264;
            }
            
            targetImporter.SetTargetSettings("WebGL",nextImporterPlatformWebGL);
        }



        private void SetupOther(VideoClipImporter targetImporter)
        {
            

            //
            // その他
            //

            //オリジナル動画の情報
            bool isHasAlpha = targetImporter.sourceHasAlpha;


            //プラットフォーム共通
            targetImporter.importAudio = true;
            targetImporter.keepAlpha = isHasAlpha;//透過があれば、透過設定維持

            //Standalone(PC)
            VideoImporterTargetSettings nextImporterPlatformStanalone  = new VideoImporterTargetSettings();
            nextImporterPlatformStanalone.enableTranscoding = true;
            nextImporterPlatformStanalone.bitrateMode = VideoBitrateMode.Medium;
            nextImporterPlatformStanalone.spatialQuality = VideoSpatialQuality.MediumSpatialQuality;
            if (isHasAlpha)
            {
                nextImporterPlatformStanalone.codec = VideoCodec.VP8;
            }
            else
            {
                // 透過なし
                nextImporterPlatformStanalone.codec = VideoCodec.H264;
            }

            targetImporter.SetTargetSettings("Standalone",nextImporterPlatformStanalone);

            //iOS
            VideoImporterTargetSettings nextImporterPlatformiOS = new VideoImporterTargetSettings();
            nextImporterPlatformiOS.enableTranscoding = true;
            nextImporterPlatformiOS.bitrateMode = VideoBitrateMode.Medium;
            nextImporterPlatformiOS.spatialQuality = VideoSpatialQuality.MediumSpatialQuality;
            if (isHasAlpha)
            {
                nextImporterPlatformiOS.codec = VideoCodec.VP8;
            }
            else
            {
                // 透過なし
                nextImporterPlatformiOS.codec = VideoCodec.H264;
            }

            targetImporter.SetTargetSettings("iPhone",nextImporterPlatformiOS);

            //Android
            VideoImporterTargetSettings nextImporterPlatformAndroid = new VideoImporterTargetSettings();
            nextImporterPlatformAndroid.enableTranscoding = true;
            nextImporterPlatformAndroid.bitrateMode = VideoBitrateMode.Medium;
            nextImporterPlatformAndroid.spatialQuality = VideoSpatialQuality.MediumSpatialQuality;
            if (isHasAlpha)
            {
                nextImporterPlatformAndroid.codec = VideoCodec.VP8;
            }
            else
            {
                // 透過なし
                nextImporterPlatformAndroid.codec = VideoCodec.H264;
            }
            

            targetImporter.SetTargetSettings("Android",nextImporterPlatformAndroid);

            //TVOS
            VideoImporterTargetSettings nextImporterPlatformtvOS = new VideoImporterTargetSettings();
            nextImporterPlatformtvOS.enableTranscoding = true;
            nextImporterPlatformtvOS.bitrateMode = VideoBitrateMode.Medium;
            nextImporterPlatformtvOS.spatialQuality = VideoSpatialQuality.MediumSpatialQuality;
            if (isHasAlpha)
            {
                nextImporterPlatformtvOS.codec = VideoCodec.VP8;
            }
            else
            {
                // 透過なし
                nextImporterPlatformtvOS.codec = VideoCodec.H264;
            }
            
            targetImporter.SetTargetSettings("tvOS",nextImporterPlatformtvOS);

            //WEBGL
            VideoImporterTargetSettings nextImporterPlatformWebGL = new VideoImporterTargetSettings();
            nextImporterPlatformWebGL.enableTranscoding = true;
            nextImporterPlatformWebGL.bitrateMode = VideoBitrateMode.Medium;
            nextImporterPlatformWebGL.spatialQuality = VideoSpatialQuality.MediumSpatialQuality;
            if (isHasAlpha)
            {
                nextImporterPlatformWebGL.codec = VideoCodec.VP8;
            }
            else
            {
                // 透過なし
                nextImporterPlatformWebGL.codec = VideoCodec.H264;
            }
            
            targetImporter.SetTargetSettings("WebGL",nextImporterPlatformWebGL);
        }
        
        private void SetupCustom1(VideoClipImporter targetImporter)
        {
            

            //
            // 
            //

            //オリジナル動画の情報
            bool isHasAlpha = targetImporter.sourceHasAlpha;


            //プラットフォーム共通
            targetImporter.importAudio = true;
            targetImporter.keepAlpha = isHasAlpha;//透過があれば、透過設定維持

            //Standalone(PC)
            VideoImporterTargetSettings nextImporterPlatformStanalone  = new VideoImporterTargetSettings();
            nextImporterPlatformStanalone.enableTranscoding = true;
            nextImporterPlatformStanalone.bitrateMode = VideoBitrateMode.Medium;
            nextImporterPlatformStanalone.spatialQuality = VideoSpatialQuality.MediumSpatialQuality;
            if (isHasAlpha)
            {
                nextImporterPlatformStanalone.codec = VideoCodec.VP8;
            }
            else
            {
                // 透過なし
                nextImporterPlatformStanalone.codec = VideoCodec.H264;
            }

            targetImporter.SetTargetSettings("Standalone",nextImporterPlatformStanalone);

            //iOS
            VideoImporterTargetSettings nextImporterPlatformiOS = new VideoImporterTargetSettings();
            nextImporterPlatformiOS.enableTranscoding = true;
            nextImporterPlatformiOS.bitrateMode = VideoBitrateMode.Medium;
            nextImporterPlatformiOS.spatialQuality = VideoSpatialQuality.MediumSpatialQuality;
            if (isHasAlpha)
            {
                nextImporterPlatformiOS.codec = VideoCodec.VP8;
            }
            else
            {
                // 透過なし
                nextImporterPlatformiOS.codec = VideoCodec.H264;
            }

            targetImporter.SetTargetSettings("iPhone",nextImporterPlatformiOS);

            //Android
            VideoImporterTargetSettings nextImporterPlatformAndroid = new VideoImporterTargetSettings();
            nextImporterPlatformAndroid.enableTranscoding = true;
            nextImporterPlatformAndroid.bitrateMode = VideoBitrateMode.Medium;
            nextImporterPlatformAndroid.spatialQuality = VideoSpatialQuality.MediumSpatialQuality;
            if (isHasAlpha)
            {
                nextImporterPlatformAndroid.codec = VideoCodec.VP8;
            }
            else
            {
                // 透過なし
                nextImporterPlatformAndroid.codec = VideoCodec.H264;
            }
            

            targetImporter.SetTargetSettings("Android",nextImporterPlatformAndroid);

            //TVOS
            VideoImporterTargetSettings nextImporterPlatformtvOS = new VideoImporterTargetSettings();
            nextImporterPlatformtvOS.enableTranscoding = true;
            nextImporterPlatformtvOS.bitrateMode = VideoBitrateMode.Medium;
            nextImporterPlatformtvOS.spatialQuality = VideoSpatialQuality.MediumSpatialQuality;
            if (isHasAlpha)
            {
                nextImporterPlatformtvOS.codec = VideoCodec.VP8;
            }
            else
            {
                // 透過なし
                nextImporterPlatformtvOS.codec = VideoCodec.H264;
            }
            
            targetImporter.SetTargetSettings("tvOS",nextImporterPlatformtvOS);

            //WEBGL
            VideoImporterTargetSettings nextImporterPlatformWebGL = new VideoImporterTargetSettings();
            nextImporterPlatformWebGL.enableTranscoding = true;
            nextImporterPlatformWebGL.bitrateMode = VideoBitrateMode.Medium;
            nextImporterPlatformWebGL.spatialQuality = VideoSpatialQuality.MediumSpatialQuality;
            if (isHasAlpha)
            {
                nextImporterPlatformWebGL.codec = VideoCodec.VP8;
            }
            else
            {
                // 透過なし
                nextImporterPlatformWebGL.codec = VideoCodec.H264;
            }
            
            targetImporter.SetTargetSettings("WebGL",nextImporterPlatformWebGL);
        }

        
        private void SetupCustom2(VideoClipImporter targetImporter)
        {
            

            //
            // 
            //

            //オリジナル動画の情報
            bool isHasAlpha = targetImporter.sourceHasAlpha;


            //プラットフォーム共通
            targetImporter.importAudio = true;
            targetImporter.keepAlpha = isHasAlpha;//透過があれば、透過設定維持

            //Standalone(PC)
            VideoImporterTargetSettings nextImporterPlatformStanalone  = new VideoImporterTargetSettings();
            nextImporterPlatformStanalone.enableTranscoding = true;
            nextImporterPlatformStanalone.bitrateMode = VideoBitrateMode.Medium;
            nextImporterPlatformStanalone.spatialQuality = VideoSpatialQuality.MediumSpatialQuality;
            if (isHasAlpha)
            {
                nextImporterPlatformStanalone.codec = VideoCodec.VP8;
            }
            else
            {
                // 透過なし
                nextImporterPlatformStanalone.codec = VideoCodec.H264;
            }

            targetImporter.SetTargetSettings("Standalone",nextImporterPlatformStanalone);

            //iOS
            VideoImporterTargetSettings nextImporterPlatformiOS = new VideoImporterTargetSettings();
            nextImporterPlatformiOS.enableTranscoding = true;
            nextImporterPlatformiOS.bitrateMode = VideoBitrateMode.Medium;
            nextImporterPlatformiOS.spatialQuality = VideoSpatialQuality.MediumSpatialQuality;
            if (isHasAlpha)
            {
                nextImporterPlatformiOS.codec = VideoCodec.VP8;
            }
            else
            {
                // 透過なし
                nextImporterPlatformiOS.codec = VideoCodec.H264;
            }

            targetImporter.SetTargetSettings("iPhone",nextImporterPlatformiOS);

            //Android
            VideoImporterTargetSettings nextImporterPlatformAndroid = new VideoImporterTargetSettings();
            nextImporterPlatformAndroid.enableTranscoding = true;
            nextImporterPlatformAndroid.bitrateMode = VideoBitrateMode.Medium;
            nextImporterPlatformAndroid.spatialQuality = VideoSpatialQuality.MediumSpatialQuality;
            if (isHasAlpha)
            {
                nextImporterPlatformAndroid.codec = VideoCodec.VP8;
            }
            else
            {
                // 透過なし
                nextImporterPlatformAndroid.codec = VideoCodec.H264;
            }
            

            targetImporter.SetTargetSettings("Android",nextImporterPlatformAndroid);

            //TVOS
            VideoImporterTargetSettings nextImporterPlatformtvOS = new VideoImporterTargetSettings();
            nextImporterPlatformtvOS.enableTranscoding = true;
            nextImporterPlatformtvOS.bitrateMode = VideoBitrateMode.Medium;
            nextImporterPlatformtvOS.spatialQuality = VideoSpatialQuality.MediumSpatialQuality;
            if (isHasAlpha)
            {
                nextImporterPlatformtvOS.codec = VideoCodec.VP8;
            }
            else
            {
                // 透過なし
                nextImporterPlatformtvOS.codec = VideoCodec.H264;
            }
            
            targetImporter.SetTargetSettings("tvOS",nextImporterPlatformtvOS);

            //WEBGL
            VideoImporterTargetSettings nextImporterPlatformWebGL = new VideoImporterTargetSettings();
            nextImporterPlatformWebGL.enableTranscoding = true;
            nextImporterPlatformWebGL.bitrateMode = VideoBitrateMode.Medium;
            nextImporterPlatformWebGL.spatialQuality = VideoSpatialQuality.MediumSpatialQuality;
            if (isHasAlpha)
            {
                nextImporterPlatformWebGL.codec = VideoCodec.VP8;
            }
            else
            {
                // 透過なし
                nextImporterPlatformWebGL.codec = VideoCodec.H264;
            }
            
            targetImporter.SetTargetSettings("WebGL",nextImporterPlatformWebGL);
        }

        
        private void SetupCustom3(VideoClipImporter targetImporter)
        {
            

            //
            // 
            //

            //オリジナル動画の情報
            bool isHasAlpha = targetImporter.sourceHasAlpha;


            //プラットフォーム共通
            targetImporter.importAudio = true;
            targetImporter.keepAlpha = isHasAlpha;//透過があれば、透過設定維持

            //Standalone(PC)
            VideoImporterTargetSettings nextImporterPlatformStanalone  = new VideoImporterTargetSettings();
            nextImporterPlatformStanalone.enableTranscoding = true;
            nextImporterPlatformStanalone.bitrateMode = VideoBitrateMode.Medium;
            nextImporterPlatformStanalone.spatialQuality = VideoSpatialQuality.MediumSpatialQuality;
            if (isHasAlpha)
            {
                nextImporterPlatformStanalone.codec = VideoCodec.VP8;
            }
            else
            {
                // 透過なし
                nextImporterPlatformStanalone.codec = VideoCodec.H264;
            }

            targetImporter.SetTargetSettings("Standalone",nextImporterPlatformStanalone);

            //iOS
            VideoImporterTargetSettings nextImporterPlatformiOS = new VideoImporterTargetSettings();
            nextImporterPlatformiOS.enableTranscoding = true;
            nextImporterPlatformiOS.bitrateMode = VideoBitrateMode.Medium;
            nextImporterPlatformiOS.spatialQuality = VideoSpatialQuality.MediumSpatialQuality;
            if (isHasAlpha)
            {
                nextImporterPlatformiOS.codec = VideoCodec.VP8;
            }
            else
            {
                // 透過なし
                nextImporterPlatformiOS.codec = VideoCodec.H264;
            }

            targetImporter.SetTargetSettings("iPhone",nextImporterPlatformiOS);

            //Android
            VideoImporterTargetSettings nextImporterPlatformAndroid = new VideoImporterTargetSettings();
            nextImporterPlatformAndroid.enableTranscoding = true;
            nextImporterPlatformAndroid.bitrateMode = VideoBitrateMode.Medium;
            nextImporterPlatformAndroid.spatialQuality = VideoSpatialQuality.MediumSpatialQuality;
            if (isHasAlpha)
            {
                nextImporterPlatformAndroid.codec = VideoCodec.VP8;
            }
            else
            {
                // 透過なし
                nextImporterPlatformAndroid.codec = VideoCodec.H264;
            }
            

            targetImporter.SetTargetSettings("Android",nextImporterPlatformAndroid);

            //TVOS
            VideoImporterTargetSettings nextImporterPlatformtvOS = new VideoImporterTargetSettings();
            nextImporterPlatformtvOS.enableTranscoding = true;
            nextImporterPlatformtvOS.bitrateMode = VideoBitrateMode.Medium;
            nextImporterPlatformtvOS.spatialQuality = VideoSpatialQuality.MediumSpatialQuality;
            if (isHasAlpha)
            {
                nextImporterPlatformtvOS.codec = VideoCodec.VP8;
            }
            else
            {
                // 透過なし
                nextImporterPlatformtvOS.codec = VideoCodec.H264;
            }
            
            targetImporter.SetTargetSettings("tvOS",nextImporterPlatformtvOS);

            //WEBGL
            VideoImporterTargetSettings nextImporterPlatformWebGL = new VideoImporterTargetSettings();
            nextImporterPlatformWebGL.enableTranscoding = true;
            nextImporterPlatformWebGL.bitrateMode = VideoBitrateMode.Medium;
            nextImporterPlatformWebGL.spatialQuality = VideoSpatialQuality.MediumSpatialQuality;
            if (isHasAlpha)
            {
                nextImporterPlatformWebGL.codec = VideoCodec.VP8;
            }
            else
            {
                // 透過なし
                nextImporterPlatformWebGL.codec = VideoCodec.H264;
            }
            
            targetImporter.SetTargetSettings("WebGL",nextImporterPlatformWebGL);
        }


        #endregion


        private VideoUseKind SearchUseKind(string assetPath)
        {
            if (CommonConfig.videoNormalDirectoryPathList.ToList()
                .Any(directoryPath => assetPath.Contains(directoryPath)))
            {
                return VideoUseKind.Normal;
            }
            
            if (CommonConfig.videoCustom1DirectoryPathList.ToList()
                .Any(directoryPath => assetPath.Contains(directoryPath)))
            {
                return VideoUseKind.Custom1;
            }
            
            if (CommonConfig.videoCustom2DirectoryPathList.ToList()
                .Any(directoryPath => assetPath.Contains(directoryPath)))
            {
                return VideoUseKind.Custom2;
            }
            
            if (CommonConfig.videoCustom3DirectoryPathList.ToList()
                .Any(directoryPath => assetPath.Contains(directoryPath)))
            {
                return VideoUseKind.Custom3;
            }


            return VideoUseKind.Unknown;
        }
    }
}