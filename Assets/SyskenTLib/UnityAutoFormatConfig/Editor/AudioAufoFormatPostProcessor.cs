using System.Collections.Generic;
using UnityEditor;
using System.Linq;
using UnityEngine;

namespace SyskenTLib.UnityAutoFormatConfig.Editor
{
    public class AudioAufoFormatPostProcessor : AssetPostprocessor
    {
        private enum AudioUseKind
        {
            BGM,
            SE,
            Custom1,
            Custom2,
            Custom3,
            Unknown
        }
        



        void OnPreprocessAudio()
        {
            STAutoFormatConfig formatConfig = ConfigManager.GetGeneralRootConfig();
            
         
            if (formatConfig.compileTimingAudio == FormatTiming.None)
            {
                return;
            }


            AudioImporter nextImporter = assetImporter as AudioImporter;

            if (formatConfig.compileTimingAudio == FormatTiming.FirstOnly
                && nextImporter.importSettingsMissing == false)
            {
                //初回インポート以外ののとき
                //初回以外の場合は、すでに設定されていると思うので、なにも処理しない
                return;
            }

            
            STRootAudioConfig rootConfig = ConfigManager.GetAudioRootConfig();

            AudioUseKind nextUseKind = SearchUseKind(nextImporter.assetPath);

            switch (nextUseKind)
            {
                case AudioUseKind.BGM:
                {
                    //
                    // BGM
                    //
                    SetupConfig(nextImporter,rootConfig.bgmConfig);
                }
                    break;

                case AudioUseKind.SE:
                {
                    //SE
                    SetupConfig(nextImporter,rootConfig.seConfig);
                }
                    break;

                case AudioUseKind.Unknown:
                {
                    //
                    // その他、すべて
                    //
                    SetupConfig(nextImporter,rootConfig.defaultConfig);
                }
                    break;
                
                case AudioUseKind.Custom1:
                {
                    //
                    // 
                    //
                    SetupConfig(nextImporter,rootConfig.custom1Config);
                }
                    break;
                
                case AudioUseKind.Custom2:
                {
                    //
                    // 
                    //
                    SetupConfig(nextImporter,rootConfig.custom2Config);
                }
                    break;
                
                case AudioUseKind.Custom3:
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

        private void SetupConfig(AudioImporter targetImporter,STAudioConfig config)
        {
            

            //
            // BGM
            //
            //プラットフォーム共通
            targetImporter.loadInBackground = true;


            //Standalone(PC)
            AudioImporterSampleSettings nextImporterPlatformStanalone  = new AudioImporterSampleSettings()
            {
                compressionFormat = config.audioCompressionFormatOnStandalone,
                loadType = config.audioClipsLoadTypeOnStandalone,
                quality =config.audioQualityOnStandalone,
                sampleRateOverride = config.audioSampleRateOnStandalone, 
            };
            targetImporter.SetOverrideSampleSettings("Standalone",nextImporterPlatformStanalone);

            //iOS
            AudioImporterSampleSettings nextImporterPlatformiOS  = new AudioImporterSampleSettings()
            {
                compressionFormat = config.audioCompressionFormatOnIOS,
                loadType = config.audioClipsLoadTypeOnIOS,
                quality =config.audioQualityOnIOS,
                sampleRateOverride = config.audioSampleRateOnIOS
            };
            targetImporter.SetOverrideSampleSettings("iPhone",nextImporterPlatformiOS);

            //Android
            AudioImporterSampleSettings nextImporterPlatformAndroid  = new AudioImporterSampleSettings()
            {
                compressionFormat = config.audioCompressionFormatOnAndroid,
                loadType = config.audioClipsLoadTypeOnAndroid,
                quality =config.audioQualityOnAndroid,
                sampleRateOverride = config.audioSampleRateOnAndroid, 
            };
            targetImporter.SetOverrideSampleSettings("Android",nextImporterPlatformAndroid);

            //TVOS
            AudioImporterSampleSettings nextImporterPlatformtvOS  = new AudioImporterSampleSettings()
            {
                compressionFormat = config.audioCompressionFormatOnTVOS,
                loadType = config.audioClipsLoadTypeOnTVOS,
                quality =config.audioQualityOnTVOS,
                sampleRateOverride = config.audioSampleRateOnTVOS, 
            };
            targetImporter.SetOverrideSampleSettings("tvOS",nextImporterPlatformtvOS);

            //WEBGL
            AudioImporterSampleSettings nextImporterPlatformWebGL  = new AudioImporterSampleSettings()
            {
                compressionFormat = config.audioCompressionFormatOnWebGL,
                loadType = config.audioClipsLoadTypeOnWebGL,
                quality =config.audioQualityOnWebGL,
                // sampleRateOverride = config.audioSampleRateOnWebGL, 
            };
            targetImporter.SetOverrideSampleSettings("WebGL",nextImporterPlatformWebGL);
            
            // targetImporter.SaveAndReimport();
            // targetImporter.SetOverrideSampleSettings("WebPlayer",nextImporterPlatformWebGL);
        }


        #endregion


        private AudioUseKind SearchUseKind(string assetPath)
        {
            STAutoFormatConfig formatConfig = ConfigManager.GetGeneralRootConfig();

            
            if (formatConfig.bgmAudioDirectoryPathList.ToList()
                .Any(directoryPath => assetPath.Contains(directoryPath)))
            {
                return AudioUseKind.BGM;
            }

            if (formatConfig.seAudioDirectoryPathList.ToList()
                .Any(directoryPath => assetPath.Contains(directoryPath)))
            {
                return AudioUseKind.SE;
            }
            
            if (formatConfig.custom1AudioDirectoryPathList.ToList()
                .Any(directoryPath => assetPath.Contains(directoryPath)))
            {
                return AudioUseKind.Custom1;
            }
            
            if (formatConfig.custom2AudioDirectoryPathList.ToList()
                .Any(directoryPath => assetPath.Contains(directoryPath)))
            {
                return AudioUseKind.Custom2;
            }
            
            if (formatConfig.custom3AudioDirectoryPathList.ToList()
                .Any(directoryPath => assetPath.Contains(directoryPath)))
            {
                return AudioUseKind.Custom3;
            }


            return AudioUseKind.Unknown;
        }
    }
}