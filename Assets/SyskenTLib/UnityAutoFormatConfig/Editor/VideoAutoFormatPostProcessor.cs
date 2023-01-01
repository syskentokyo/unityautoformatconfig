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
            }
        }

        #region 用途ごとの設定

        private void SetupNormal(VideoClipImporter targetImporter)
        {
            

            //
            // Normal
            //
            
            int MAX_WIDTH = 3840;
            int MAX_HEIGHT = 2160;
            
            //オリジナル動画の情報
            bool isHasAlpha = targetImporter.sourceHasAlpha;
            int originalWidth = targetImporter.GetResizeWidth(VideoResizeMode.OriginalSize);
            int originalHeight = targetImporter.GetResizeHeight(VideoResizeMode.OriginalSize);

            //プラットフォーム共通
            targetImporter.importAudio = true;
            targetImporter.keepAlpha = isHasAlpha;//透過があれば、透過設定維持

            //Standalone(PC)
            VideoImporterTargetSettings nextImporterPlatformStanalone  = new VideoImporterTargetSettings();
            nextImporterPlatformStanalone.enableTranscoding = true;
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
            
            MAX_WIDTH = 3840;
            MAX_HEIGHT = 2160;
            //サイズ調整
            if (originalWidth >= MAX_WIDTH && originalHeight >= MAX_HEIGHT)
            {
                //幅、高さが大きすぎる場合
                nextImporterPlatformStanalone.resizeMode = VideoResizeMode.CustomSize;
                nextImporterPlatformStanalone.customWidth = MAX_WIDTH;
                nextImporterPlatformStanalone.customHeight = MAX_WIDTH*(originalHeight/originalWidth);
                
            } else if (originalWidth>=MAX_WIDTH)
            {
                //幅が大きすぎる場合
                nextImporterPlatformStanalone.resizeMode = VideoResizeMode.CustomSize;
                nextImporterPlatformStanalone.customWidth = MAX_WIDTH;
                nextImporterPlatformStanalone.customHeight = MAX_WIDTH*(originalHeight/originalWidth);
                


            }else if (originalHeight >= MAX_HEIGHT)
            {
                //高さが大きすぎる場合
                nextImporterPlatformStanalone.resizeMode = VideoResizeMode.CustomSize;
                nextImporterPlatformStanalone.customWidth = MAX_HEIGHT*(originalWidth/originalHeight);
                nextImporterPlatformStanalone.customHeight = MAX_HEIGHT;
            }
            targetImporter.SetTargetSettings("Standalone",nextImporterPlatformStanalone);

            //iOS
            VideoImporterTargetSettings nextImporterPlatformiOS = new VideoImporterTargetSettings();
            nextImporterPlatformiOS.enableTranscoding = true;
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
            
            MAX_WIDTH = 960;
            MAX_HEIGHT = 540;
            //サイズ調整
            if (originalWidth >= MAX_WIDTH && originalHeight >= MAX_HEIGHT)
            {
                //幅、高さが大きすぎる場合
                nextImporterPlatformiOS.resizeMode = VideoResizeMode.CustomSize;
                nextImporterPlatformiOS.customWidth = MAX_WIDTH;
                nextImporterPlatformiOS.customHeight = MAX_WIDTH*(originalHeight/originalWidth);
                
            } else if (originalWidth>=MAX_WIDTH)
            {
                //幅が大きすぎる場合
                nextImporterPlatformiOS.resizeMode = VideoResizeMode.CustomSize;
                nextImporterPlatformiOS.customWidth = MAX_WIDTH;
                nextImporterPlatformiOS.customHeight = MAX_WIDTH*(originalHeight/originalWidth);
                


            }else if (originalHeight >= MAX_HEIGHT)
            {
                //高さが大きすぎる場合
                nextImporterPlatformiOS.resizeMode = VideoResizeMode.CustomSize;
                nextImporterPlatformiOS.customWidth = MAX_HEIGHT*(originalWidth/originalHeight);
                nextImporterPlatformiOS.customHeight = MAX_HEIGHT;
            }
            targetImporter.SetTargetSettings("iPhone",nextImporterPlatformiOS);

            //Android
            VideoImporterTargetSettings nextImporterPlatformAndroid = new VideoImporterTargetSettings();
            nextImporterPlatformAndroid.enableTranscoding = true;
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
            
            MAX_WIDTH = 960;
            MAX_HEIGHT = 540;
            //サイズ調整
            if (originalWidth >= MAX_WIDTH && originalHeight >= MAX_HEIGHT)
            {
                //幅、高さが大きすぎる場合
                nextImporterPlatformAndroid.resizeMode = VideoResizeMode.CustomSize;
                nextImporterPlatformAndroid.customWidth = MAX_WIDTH;
                nextImporterPlatformAndroid.customHeight = MAX_WIDTH*(originalHeight/originalWidth);
                
            } else if (originalWidth>=MAX_WIDTH)
            {
                //幅が大きすぎる場合
                nextImporterPlatformAndroid.resizeMode = VideoResizeMode.CustomSize;
                nextImporterPlatformAndroid.customWidth = MAX_WIDTH;
                nextImporterPlatformAndroid.customHeight = MAX_WIDTH*(originalHeight/originalWidth);
                


            }else if (originalHeight >= MAX_HEIGHT)
            {
                //高さが大きすぎる場合
                nextImporterPlatformAndroid.resizeMode = VideoResizeMode.CustomSize;
                nextImporterPlatformAndroid.customWidth = MAX_HEIGHT*(originalWidth/originalHeight);
                nextImporterPlatformAndroid.customHeight = MAX_HEIGHT;
            }
            targetImporter.SetTargetSettings("Android",nextImporterPlatformAndroid);

            //TVOS
            VideoImporterTargetSettings nextImporterPlatformtvOS = new VideoImporterTargetSettings();
            nextImporterPlatformtvOS.enableTranscoding = true;
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
            
            MAX_WIDTH = 1920;
            MAX_HEIGHT = 1080;
            //サイズ調整
            if (originalWidth >= MAX_WIDTH && originalHeight >= MAX_HEIGHT)
            {
                //幅、高さが大きすぎる場合
                nextImporterPlatformtvOS.resizeMode = VideoResizeMode.CustomSize;
                nextImporterPlatformtvOS.customWidth = MAX_WIDTH;
                nextImporterPlatformtvOS.customHeight = MAX_WIDTH*(originalHeight/originalWidth);
                
            } else if (originalWidth>=MAX_WIDTH)
            {
                //幅が大きすぎる場合
                nextImporterPlatformtvOS.resizeMode = VideoResizeMode.CustomSize;
                nextImporterPlatformtvOS.customWidth = MAX_WIDTH;
                nextImporterPlatformtvOS.customHeight = MAX_WIDTH*(originalHeight/originalWidth);
                


            }else if (originalHeight >= MAX_HEIGHT)
            {
                //高さが大きすぎる場合
                nextImporterPlatformtvOS.resizeMode = VideoResizeMode.CustomSize;
                nextImporterPlatformtvOS.customWidth = MAX_HEIGHT*(originalWidth/originalHeight);
                nextImporterPlatformtvOS.customHeight = MAX_HEIGHT;
            }
            targetImporter.SetTargetSettings("tvOS",nextImporterPlatformtvOS);

            //WEBGL
            VideoImporterTargetSettings nextImporterPlatformWebGL = new VideoImporterTargetSettings();
            nextImporterPlatformWebGL.enableTranscoding = true;
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
            
            MAX_WIDTH = 1920;
            MAX_HEIGHT = 1080;
            //サイズ調整
            if (originalWidth >= MAX_WIDTH && originalHeight >= MAX_HEIGHT)
            {
                //幅、高さが大きすぎる場合
                nextImporterPlatformWebGL.resizeMode = VideoResizeMode.CustomSize;
                nextImporterPlatformWebGL.customWidth = MAX_WIDTH;
                nextImporterPlatformWebGL.customHeight = MAX_WIDTH*(originalHeight/originalWidth);
                
            } else if (originalWidth>=MAX_WIDTH)
            {
                //幅が大きすぎる場合
                nextImporterPlatformWebGL.resizeMode = VideoResizeMode.CustomSize;
                nextImporterPlatformWebGL.customWidth = MAX_WIDTH;
                nextImporterPlatformWebGL.customHeight = MAX_WIDTH*(originalHeight/originalWidth);
                


            }else if (originalHeight >= MAX_HEIGHT)
            {
                //高さが大きすぎる場合
                nextImporterPlatformWebGL.resizeMode = VideoResizeMode.CustomSize;
                nextImporterPlatformWebGL.customWidth = MAX_HEIGHT*(originalWidth/originalHeight);
                nextImporterPlatformWebGL.customHeight = MAX_HEIGHT;
            }
            targetImporter.SetTargetSettings("WebGL",nextImporterPlatformWebGL);
        }



        private void SetupOther(VideoClipImporter targetImporter)
        {
            

            //
            // その他
            //
            
            int MAX_WIDTH = 3840;
            int MAX_HEIGHT = 2160;
            
            //オリジナル動画の情報
            bool isHasAlpha = targetImporter.sourceHasAlpha;
            int originalWidth = targetImporter.GetResizeWidth(VideoResizeMode.OriginalSize);
            int originalHeight = targetImporter.GetResizeHeight(VideoResizeMode.OriginalSize);

            //プラットフォーム共通
            targetImporter.importAudio = true;
            targetImporter.keepAlpha = isHasAlpha;//透過があれば、透過設定維持

            //Standalone(PC)
            VideoImporterTargetSettings nextImporterPlatformStanalone  = new VideoImporterTargetSettings();
            nextImporterPlatformStanalone.enableTranscoding = true;
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
            
            MAX_WIDTH = 3840;
            MAX_HEIGHT = 2160;
            //サイズ調整
            if (originalWidth >= MAX_WIDTH && originalHeight >= MAX_HEIGHT)
            {
                //幅、高さが大きすぎる場合
                nextImporterPlatformStanalone.resizeMode = VideoResizeMode.CustomSize;
                nextImporterPlatformStanalone.customWidth = MAX_WIDTH;
                nextImporterPlatformStanalone.customHeight = MAX_WIDTH*(originalHeight/originalWidth);
                
            } else if (originalWidth>=MAX_WIDTH)
            {
                //幅が大きすぎる場合
                nextImporterPlatformStanalone.resizeMode = VideoResizeMode.CustomSize;
                nextImporterPlatformStanalone.customWidth = MAX_WIDTH;
                nextImporterPlatformStanalone.customHeight = MAX_WIDTH*(originalHeight/originalWidth);
                


            }else if (originalHeight >= MAX_HEIGHT)
            {
                //高さが大きすぎる場合
                nextImporterPlatformStanalone.resizeMode = VideoResizeMode.CustomSize;
                nextImporterPlatformStanalone.customWidth = MAX_HEIGHT*(originalWidth/originalHeight);
                nextImporterPlatformStanalone.customHeight = MAX_HEIGHT;
            }
            targetImporter.SetTargetSettings("Standalone",nextImporterPlatformStanalone);

            //iOS
            VideoImporterTargetSettings nextImporterPlatformiOS = new VideoImporterTargetSettings();
            nextImporterPlatformiOS.enableTranscoding = true;
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
            
            MAX_WIDTH = 960;
            MAX_HEIGHT = 540;
            //サイズ調整
            if (originalWidth >= MAX_WIDTH && originalHeight >= MAX_HEIGHT)
            {
                //幅、高さが大きすぎる場合
                nextImporterPlatformiOS.resizeMode = VideoResizeMode.CustomSize;
                nextImporterPlatformiOS.customWidth = MAX_WIDTH;
                nextImporterPlatformiOS.customHeight = MAX_WIDTH*(originalHeight/originalWidth);
                
            } else if (originalWidth>=MAX_WIDTH)
            {
                //幅が大きすぎる場合
                nextImporterPlatformiOS.resizeMode = VideoResizeMode.CustomSize;
                nextImporterPlatformiOS.customWidth = MAX_WIDTH;
                nextImporterPlatformiOS.customHeight = MAX_WIDTH*(originalHeight/originalWidth);
                


            }else if (originalHeight >= MAX_HEIGHT)
            {
                //高さが大きすぎる場合
                nextImporterPlatformiOS.resizeMode = VideoResizeMode.CustomSize;
                nextImporterPlatformiOS.customWidth = MAX_HEIGHT*(originalWidth/originalHeight);
                nextImporterPlatformiOS.customHeight = MAX_HEIGHT;
            }
            targetImporter.SetTargetSettings("iPhone",nextImporterPlatformiOS);

            //Android
            VideoImporterTargetSettings nextImporterPlatformAndroid = new VideoImporterTargetSettings();
            nextImporterPlatformAndroid.enableTranscoding = true;
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
            
            MAX_WIDTH = 960;
            MAX_HEIGHT = 540;
            //サイズ調整
            if (originalWidth >= MAX_WIDTH && originalHeight >= MAX_HEIGHT)
            {
                //幅、高さが大きすぎる場合
                nextImporterPlatformAndroid.resizeMode = VideoResizeMode.CustomSize;
                nextImporterPlatformAndroid.customWidth = MAX_WIDTH;
                nextImporterPlatformAndroid.customHeight = MAX_WIDTH*(originalHeight/originalWidth);
                
            } else if (originalWidth>=MAX_WIDTH)
            {
                //幅が大きすぎる場合
                nextImporterPlatformAndroid.resizeMode = VideoResizeMode.CustomSize;
                nextImporterPlatformAndroid.customWidth = MAX_WIDTH;
                nextImporterPlatformAndroid.customHeight = MAX_WIDTH*(originalHeight/originalWidth);
                


            }else if (originalHeight >= MAX_HEIGHT)
            {
                //高さが大きすぎる場合
                nextImporterPlatformAndroid.resizeMode = VideoResizeMode.CustomSize;
                nextImporterPlatformAndroid.customWidth = MAX_HEIGHT*(originalWidth/originalHeight);
                nextImporterPlatformAndroid.customHeight = MAX_HEIGHT;
            }
            targetImporter.SetTargetSettings("Android",nextImporterPlatformAndroid);

            //TVOS
            VideoImporterTargetSettings nextImporterPlatformtvOS = new VideoImporterTargetSettings();
            nextImporterPlatformtvOS.enableTranscoding = true;
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
            
            MAX_WIDTH = 1920;
            MAX_HEIGHT = 1080;
            //サイズ調整
            if (originalWidth >= MAX_WIDTH && originalHeight >= MAX_HEIGHT)
            {
                //幅、高さが大きすぎる場合
                nextImporterPlatformtvOS.resizeMode = VideoResizeMode.CustomSize;
                nextImporterPlatformtvOS.customWidth = MAX_WIDTH;
                nextImporterPlatformtvOS.customHeight = MAX_WIDTH*(originalHeight/originalWidth);
                
            } else if (originalWidth>=MAX_WIDTH)
            {
                //幅が大きすぎる場合
                nextImporterPlatformtvOS.resizeMode = VideoResizeMode.CustomSize;
                nextImporterPlatformtvOS.customWidth = MAX_WIDTH;
                nextImporterPlatformtvOS.customHeight = MAX_WIDTH*(originalHeight/originalWidth);
                


            }else if (originalHeight >= MAX_HEIGHT)
            {
                //高さが大きすぎる場合
                nextImporterPlatformtvOS.resizeMode = VideoResizeMode.CustomSize;
                nextImporterPlatformtvOS.customWidth = MAX_HEIGHT*(originalWidth/originalHeight);
                nextImporterPlatformtvOS.customHeight = MAX_HEIGHT;
            }
            targetImporter.SetTargetSettings("tvOS",nextImporterPlatformtvOS);

            //WEBGL
            VideoImporterTargetSettings nextImporterPlatformWebGL = new VideoImporterTargetSettings();
            nextImporterPlatformWebGL.enableTranscoding = true;
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
            
            MAX_WIDTH = 1920;
            MAX_HEIGHT = 1080;
            //サイズ調整
            if (originalWidth >= MAX_WIDTH && originalHeight >= MAX_HEIGHT)
            {
                //幅、高さが大きすぎる場合
                nextImporterPlatformWebGL.resizeMode = VideoResizeMode.CustomSize;
                nextImporterPlatformWebGL.customWidth = MAX_WIDTH;
                nextImporterPlatformWebGL.customHeight = MAX_WIDTH*(originalHeight/originalWidth);
                
            } else if (originalWidth>=MAX_WIDTH)
            {
                //幅が大きすぎる場合
                nextImporterPlatformWebGL.resizeMode = VideoResizeMode.CustomSize;
                nextImporterPlatformWebGL.customWidth = MAX_WIDTH;
                nextImporterPlatformWebGL.customHeight = MAX_WIDTH*(originalHeight/originalWidth);
                


            }else if (originalHeight >= MAX_HEIGHT)
            {
                //高さが大きすぎる場合
                nextImporterPlatformWebGL.resizeMode = VideoResizeMode.CustomSize;
                nextImporterPlatformWebGL.customWidth = MAX_HEIGHT*(originalWidth/originalHeight);
                nextImporterPlatformWebGL.customHeight = MAX_HEIGHT;
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


            return VideoUseKind.Unknown;
        }
    }
}