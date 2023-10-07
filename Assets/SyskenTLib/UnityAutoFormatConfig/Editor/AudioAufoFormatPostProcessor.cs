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

        //設定の定義
        private readonly AudioCompressionFormat STANDALONEPlatformFormat = AudioCompressionFormat.Vorbis;
        private readonly AudioCompressionFormat IOSPlatformFormat = AudioCompressionFormat.Vorbis;
        private readonly AudioCompressionFormat ANDROIDPlatformFormat = AudioCompressionFormat.Vorbis;
        private readonly AudioCompressionFormat TVOSPlatformFormat = AudioCompressionFormat.Vorbis;
        private readonly AudioCompressionFormat WEBGLPlatformFormat = AudioCompressionFormat.Vorbis;



        void OnPreprocessAudio()
        {
            STAutoFormatConfig formatConfig = ConfigManager.GetConfig();
            
         
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


            AudioUseKind nextUseKind = SearchUseKind(nextImporter.assetPath);

            switch (nextUseKind)
            {
                case AudioUseKind.BGM:
                {
                    //
                    // BGM
                    //
                    SetupBGM(nextImporter);
                }
                    break;

                case AudioUseKind.SE:
                {
                    //SE
                    SetupSE(nextImporter);
                }
                    break;

                case AudioUseKind.Unknown:
                {
                    //
                    // その他、すべて
                    //
                    SetupOther(nextImporter);
                }
                    break;
                
                case AudioUseKind.Custom1:
                {
                    //
                    // 
                    //
                    SetupCustom1(nextImporter);
                }
                    break;
                
                case AudioUseKind.Custom2:
                {
                    //
                    // 
                    //
                    SetupCustom2(nextImporter);
                }
                    break;
                
                case AudioUseKind.Custom3:
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

        private void SetupBGM(AudioImporter targetImporter)
        {
            

            //
            // BGM
            //
            //プラットフォーム共通
            targetImporter.loadInBackground = true;


            //Standalone(PC)
            AudioImporterSampleSettings nextImporterPlatformStanalone  = new AudioImporterSampleSettings()
            {
                compressionFormat = STANDALONEPlatformFormat,
                loadType = AudioClipLoadType.Streaming,
                quality =1.0f,
                sampleRateOverride = 44100, 
            };
            targetImporter.SetOverrideSampleSettings("Standalone",nextImporterPlatformStanalone);

            //iOS
            AudioImporterSampleSettings nextImporterPlatformiOS  = new AudioImporterSampleSettings()
            {
                compressionFormat = IOSPlatformFormat,
                loadType = AudioClipLoadType.Streaming,
                quality =1.0f,
                sampleRateOverride = 44100, 
            };
            targetImporter.SetOverrideSampleSettings("iPhone",nextImporterPlatformiOS);

            //Android
            AudioImporterSampleSettings nextImporterPlatformAndroid  = new AudioImporterSampleSettings()
            {
                compressionFormat = ANDROIDPlatformFormat,
                loadType = AudioClipLoadType.Streaming,
                quality =1.0f,
                sampleRateOverride = 44100, 
            };
            targetImporter.SetOverrideSampleSettings("Android",nextImporterPlatformAndroid);

            //TVOS
            AudioImporterSampleSettings nextImporterPlatformtvOS  = new AudioImporterSampleSettings()
            {
                compressionFormat = TVOSPlatformFormat,
                loadType = AudioClipLoadType.Streaming,
                quality =1.0f,
                sampleRateOverride = 44100, 
            };
            targetImporter.SetOverrideSampleSettings("tvOS",nextImporterPlatformtvOS);

            //WEBGL
            AudioImporterSampleSettings nextImporterPlatformWebGL  = new AudioImporterSampleSettings()
            {
                compressionFormat = WEBGLPlatformFormat,
                loadType = AudioClipLoadType.Streaming,
                quality =1.0f,
                sampleRateOverride = 44100, 
            };
            targetImporter.SetOverrideSampleSettings("WebGL",nextImporterPlatformWebGL);
        }


        private void SetupSE(AudioImporter targetImporter)
        {
             

            //
            // SE
            //
            //プラットフォーム共通
            targetImporter.loadInBackground = true;


            //Standalone(PC)
            AudioImporterSampleSettings nextImporterPlatformStanalone  = new AudioImporterSampleSettings()
            {
                compressionFormat = STANDALONEPlatformFormat,
                loadType = AudioClipLoadType.CompressedInMemory,
                quality =1.0f,
                sampleRateOverride = 44100, 
            };
            targetImporter.SetOverrideSampleSettings("Standalone",nextImporterPlatformStanalone);

            //iOS
            AudioImporterSampleSettings nextImporterPlatformiOS  = new AudioImporterSampleSettings()
            {
                compressionFormat = IOSPlatformFormat,
                loadType = AudioClipLoadType.CompressedInMemory,
                quality =1.0f,
                sampleRateOverride = 44100, 
            };
            targetImporter.SetOverrideSampleSettings("iPhone",nextImporterPlatformiOS);

            //Android
            AudioImporterSampleSettings nextImporterPlatformAndroid  = new AudioImporterSampleSettings()
            {
                compressionFormat = ANDROIDPlatformFormat,
                loadType = AudioClipLoadType.CompressedInMemory,
                quality =1.0f,
                sampleRateOverride = 44100, 
            };
            targetImporter.SetOverrideSampleSettings("Android",nextImporterPlatformAndroid);

            //TVOS
            AudioImporterSampleSettings nextImporterPlatformtvOS  = new AudioImporterSampleSettings()
            {
                compressionFormat = TVOSPlatformFormat,
                loadType = AudioClipLoadType.CompressedInMemory,
                quality =1.0f,
                sampleRateOverride = 44100, 
            };
            targetImporter.SetOverrideSampleSettings("tvOS",nextImporterPlatformtvOS);

            //WEBGL
            AudioImporterSampleSettings nextImporterPlatformWebGL  = new AudioImporterSampleSettings()
            {
                compressionFormat = WEBGLPlatformFormat,
                loadType = AudioClipLoadType.CompressedInMemory,
                quality =1.0f,
                sampleRateOverride = 44100, 
            };
            targetImporter.SetOverrideSampleSettings("WebGL",nextImporterPlatformWebGL);
        }


        private void SetupOther(AudioImporter targetImporter)
        {
            
            //
            //その他
            //
            //プラットフォーム共通
            targetImporter.loadInBackground = true;


            //Standalone(PC)
            AudioImporterSampleSettings nextImporterPlatformStanalone  = new AudioImporterSampleSettings()
            {
                compressionFormat = STANDALONEPlatformFormat,
                loadType = AudioClipLoadType.CompressedInMemory,
                quality =1.0f,
                sampleRateOverride = 44100, 
            };
            targetImporter.SetOverrideSampleSettings("Standalone",nextImporterPlatformStanalone);

            //iOS
            AudioImporterSampleSettings nextImporterPlatformiOS  = new AudioImporterSampleSettings()
            {
                compressionFormat = IOSPlatformFormat,
                loadType = AudioClipLoadType.CompressedInMemory,
                quality =1.0f,
                sampleRateOverride = 44100, 
            };
            targetImporter.SetOverrideSampleSettings("iPhone",nextImporterPlatformiOS);

            //Android
            AudioImporterSampleSettings nextImporterPlatformAndroid  = new AudioImporterSampleSettings()
            {
                compressionFormat = ANDROIDPlatformFormat,
                loadType = AudioClipLoadType.CompressedInMemory,
                quality =1.0f,
                sampleRateOverride = 44100, 
            };
            targetImporter.SetOverrideSampleSettings("Android",nextImporterPlatformAndroid);

            //TVOS
            AudioImporterSampleSettings nextImporterPlatformtvOS  = new AudioImporterSampleSettings()
            {
                compressionFormat = TVOSPlatformFormat,
                loadType = AudioClipLoadType.CompressedInMemory,
                quality =1.0f,
                sampleRateOverride = 44100, 
            };
            targetImporter.SetOverrideSampleSettings("tvOS",nextImporterPlatformtvOS);

            //WEBGL
            AudioImporterSampleSettings nextImporterPlatformWebGL  = new AudioImporterSampleSettings()
            {
                compressionFormat = WEBGLPlatformFormat,
                loadType = AudioClipLoadType.CompressedInMemory,
                quality =1.0f,
                sampleRateOverride = 44100, 
            };
            targetImporter.SetOverrideSampleSettings("WebGL",nextImporterPlatformWebGL);
        }
        
         private void SetupCustom1(AudioImporter targetImporter)
        {
            
            //
            //
            //
            //プラットフォーム共通
            targetImporter.loadInBackground = true;


            //Standalone(PC)
            AudioImporterSampleSettings nextImporterPlatformStanalone  = new AudioImporterSampleSettings()
            {
                compressionFormat = STANDALONEPlatformFormat,
                loadType = AudioClipLoadType.CompressedInMemory,
                quality =1.0f,
                sampleRateOverride = 44100, 
            };
            targetImporter.SetOverrideSampleSettings("Standalone",nextImporterPlatformStanalone);

            //iOS
            AudioImporterSampleSettings nextImporterPlatformiOS  = new AudioImporterSampleSettings()
            {
                compressionFormat = IOSPlatformFormat,
                loadType = AudioClipLoadType.CompressedInMemory,
                quality =1.0f,
                sampleRateOverride = 44100, 
            };
            targetImporter.SetOverrideSampleSettings("iPhone",nextImporterPlatformiOS);

            //Android
            AudioImporterSampleSettings nextImporterPlatformAndroid  = new AudioImporterSampleSettings()
            {
                compressionFormat = ANDROIDPlatformFormat,
                loadType = AudioClipLoadType.CompressedInMemory,
                quality =1.0f,
                sampleRateOverride = 44100, 
            };
            targetImporter.SetOverrideSampleSettings("Android",nextImporterPlatformAndroid);

            //TVOS
            AudioImporterSampleSettings nextImporterPlatformtvOS  = new AudioImporterSampleSettings()
            {
                compressionFormat = TVOSPlatformFormat,
                loadType = AudioClipLoadType.CompressedInMemory,
                quality =1.0f,
                sampleRateOverride = 44100, 
            };
            targetImporter.SetOverrideSampleSettings("tvOS",nextImporterPlatformtvOS);

            //WEBGL
            AudioImporterSampleSettings nextImporterPlatformWebGL  = new AudioImporterSampleSettings()
            {
                compressionFormat = WEBGLPlatformFormat,
                loadType = AudioClipLoadType.CompressedInMemory,
                quality =1.0f,
                sampleRateOverride = 44100, 
            };
            targetImporter.SetOverrideSampleSettings("WebGL",nextImporterPlatformWebGL);
        }
         
          private void SetupCustom2(AudioImporter targetImporter)
        {
            
            //
            //
            //
            //プラットフォーム共通
            targetImporter.loadInBackground = true;


            //Standalone(PC)
            AudioImporterSampleSettings nextImporterPlatformStanalone  = new AudioImporterSampleSettings()
            {
                compressionFormat = STANDALONEPlatformFormat,
                loadType = AudioClipLoadType.CompressedInMemory,
                quality =1.0f,
                sampleRateOverride = 44100, 
            };
            targetImporter.SetOverrideSampleSettings("Standalone",nextImporterPlatformStanalone);

            //iOS
            AudioImporterSampleSettings nextImporterPlatformiOS  = new AudioImporterSampleSettings()
            {
                compressionFormat = IOSPlatformFormat,
                loadType = AudioClipLoadType.CompressedInMemory,
                quality =1.0f,
                sampleRateOverride = 44100, 
            };
            targetImporter.SetOverrideSampleSettings("iPhone",nextImporterPlatformiOS);

            //Android
            AudioImporterSampleSettings nextImporterPlatformAndroid  = new AudioImporterSampleSettings()
            {
                compressionFormat = ANDROIDPlatformFormat,
                loadType = AudioClipLoadType.CompressedInMemory,
                quality =1.0f,
                sampleRateOverride = 44100, 
            };
            targetImporter.SetOverrideSampleSettings("Android",nextImporterPlatformAndroid);

            //TVOS
            AudioImporterSampleSettings nextImporterPlatformtvOS  = new AudioImporterSampleSettings()
            {
                compressionFormat = TVOSPlatformFormat,
                loadType = AudioClipLoadType.CompressedInMemory,
                quality =1.0f,
                sampleRateOverride = 44100, 
            };
            targetImporter.SetOverrideSampleSettings("tvOS",nextImporterPlatformtvOS);

            //WEBGL
            AudioImporterSampleSettings nextImporterPlatformWebGL  = new AudioImporterSampleSettings()
            {
                compressionFormat = WEBGLPlatformFormat,
                loadType = AudioClipLoadType.CompressedInMemory,
                quality =1.0f,
                sampleRateOverride = 44100, 
            };
            targetImporter.SetOverrideSampleSettings("WebGL",nextImporterPlatformWebGL);
        }
          
           private void SetupCustom3(AudioImporter targetImporter)
        {
            
            //
            //
            //
            //プラットフォーム共通
            targetImporter.loadInBackground = true;


            //Standalone(PC)
            AudioImporterSampleSettings nextImporterPlatformStanalone  = new AudioImporterSampleSettings()
            {
                compressionFormat = STANDALONEPlatformFormat,
                loadType = AudioClipLoadType.CompressedInMemory,
                quality =1.0f,
                sampleRateOverride = 44100, 
            };
            targetImporter.SetOverrideSampleSettings("Standalone",nextImporterPlatformStanalone);

            //iOS
            AudioImporterSampleSettings nextImporterPlatformiOS  = new AudioImporterSampleSettings()
            {
                compressionFormat = IOSPlatformFormat,
                loadType = AudioClipLoadType.CompressedInMemory,
                quality =1.0f,
                sampleRateOverride = 44100, 
            };
            targetImporter.SetOverrideSampleSettings("iPhone",nextImporterPlatformiOS);

            //Android
            AudioImporterSampleSettings nextImporterPlatformAndroid  = new AudioImporterSampleSettings()
            {
                compressionFormat = ANDROIDPlatformFormat,
                loadType = AudioClipLoadType.CompressedInMemory,
                quality =1.0f,
                sampleRateOverride = 44100, 
            };
            targetImporter.SetOverrideSampleSettings("Android",nextImporterPlatformAndroid);

            //TVOS
            AudioImporterSampleSettings nextImporterPlatformtvOS  = new AudioImporterSampleSettings()
            {
                compressionFormat = TVOSPlatformFormat,
                loadType = AudioClipLoadType.CompressedInMemory,
                quality =1.0f,
                sampleRateOverride = 44100, 
            };
            targetImporter.SetOverrideSampleSettings("tvOS",nextImporterPlatformtvOS);

            //WEBGL
            AudioImporterSampleSettings nextImporterPlatformWebGL  = new AudioImporterSampleSettings()
            {
                compressionFormat = WEBGLPlatformFormat,
                loadType = AudioClipLoadType.CompressedInMemory,
                quality =1.0f,
                sampleRateOverride = 44100, 
            };
            targetImporter.SetOverrideSampleSettings("WebGL",nextImporterPlatformWebGL);
        }

        #endregion


        private AudioUseKind SearchUseKind(string assetPath)
        {
            STAutoFormatConfig formatConfig = ConfigManager.GetConfig();

            
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