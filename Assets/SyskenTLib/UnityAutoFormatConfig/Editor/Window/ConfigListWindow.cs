using System;
using UnityEditor;
using UnityEngine;

namespace SyskenTLib.UnityAutoFormatConfig.Editor
{
    public class ConfigListWindow:EditorWindow
    {
        
        
        SaveDataManager _saveDataManager = new SaveDataManager();
        
        
        private bool isMustUpdate = true;
        
        private Vector2 _scrollPosition = Vector2.zero;

        
        //各パラメータ
        private string _textureNormalUIDirectoryPathText = "";
        private string _textureDotUIDirectoryPathText = "";
        private string _textureCustom1DirectoryPathText = "";
        private string _textureCustom2DirectoryPathText = "";
        private string _textureCustom3DirectoryPathText = "";
        
        private string _audioBGMDirectoryPathText = "";
        private string _audioSEDirectoryPathText = "";
        private string _audioCustom1DirectoryPathText = "";
        private string _audioCustom2DirectoryPathText = "";
        private string _audioCustom3DirectoryPathText = "";
        
        
        private string _videoNormalDirectoryPathText = "";
        private string _videoCustom1DirectoryPathText = "";
        private string _videoCustom2DirectoryPathText = "";
        private string _videoCustom3DirectoryPathText = "";
        
        
        /// <Summary>
        /// 設定を表示します。
        /// </Summary>
        [MenuItem("SyskenTLib/AutoFormatConfigWindow/ConfigList",priority = 10)]
        private static void OpenSetting()
        {
            var window = GetWindow<ConfigListWindow>();
            window.titleContent = new GUIContent("AutoFormat-ConfigList");
        }
        
        // <Summary>
        /// ウィンドウのパーツを表示します。
        /// </Summary>
        void OnGUI()
        {
            if (isMustUpdate)
            {
                //保存されていた設定値を読み込み
                _textureNormalUIDirectoryPathText=String.Join("\n", CommonConfig.textureNormalUIDirectoryPathList);
                _textureDotUIDirectoryPathText=String.Join("\n", CommonConfig.textureDotUIDirectoryPathList);
                _textureCustom1DirectoryPathText=String.Join("\n", CommonConfig.textureCustom1DirectoryPathList);
                _textureCustom2DirectoryPathText=String.Join("\n", CommonConfig.textureCustom2DirectoryPathList);
                _textureCustom3DirectoryPathText=String.Join("\n", CommonConfig.textureCustom3DirectoryPathList);
                
                
                _audioBGMDirectoryPathText=String.Join("\n", CommonConfig.audioBGMDirectoryPathList);
                _audioSEDirectoryPathText=String.Join("\n", CommonConfig.audioSEDirectoryPathList);
                _audioCustom1DirectoryPathText=String.Join("\n", CommonConfig.audioCustom1DirectoryPathList);
                _audioCustom2DirectoryPathText=String.Join("\n", CommonConfig.audioCustom2DirectoryPathList);
                _audioCustom3DirectoryPathText=String.Join("\n", CommonConfig.audioCustom3DirectoryPathList);
                
                _videoNormalDirectoryPathText=String.Join("\n", CommonConfig.videoNormalDirectoryPathList);
                _videoCustom1DirectoryPathText=String.Join("\n", CommonConfig.videoCustom1DirectoryPathList);
                _videoCustom2DirectoryPathText=String.Join("\n", CommonConfig.videoCustom2DirectoryPathList);
                _videoCustom3DirectoryPathText=String.Join("\n", CommonConfig.videoCustom3DirectoryPathList);

                isMustUpdate = false;
            }
            
            
            
            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition); 
            
            EditorGUILayout.BeginVertical("Box");

            if (GUILayout.Button("Update View"))
            {
                //表示更新
                isMustUpdate = true;
            }
            
            
            EditorGUILayout.LabelField("If you change target Directory , you see CommonConfig.cs please");
            EditorGUILayout.Space(30);
            
            
            EditorGUILayout.LabelField("Texture");
            EditorGUILayout.Space(3);
            EditorGUILayout.LabelField("NormalUI Directory List");
            EditorGUILayout.TextArea(_textureNormalUIDirectoryPathText);
            EditorGUILayout.Space(5);
            EditorGUILayout.LabelField("DotUI Directory List");
            EditorGUILayout.TextArea(_textureDotUIDirectoryPathText);
            EditorGUILayout.Space(5);
            EditorGUILayout.LabelField("Custom1 Directory List");
            EditorGUILayout.TextArea(_textureCustom1DirectoryPathText);
            EditorGUILayout.Space(5);
            EditorGUILayout.LabelField("Custom2 Directory List");
            EditorGUILayout.TextArea(_textureCustom2DirectoryPathText);
            EditorGUILayout.Space(5);
            EditorGUILayout.LabelField("Custom3 Directory List");
            EditorGUILayout.TextArea(_textureCustom3DirectoryPathText);
            EditorGUILayout.Space(5);
            EditorGUILayout.LabelField("Other Directory are Default Config");
            EditorGUILayout.Space(20);
            
            EditorGUILayout.LabelField("Audio");
            EditorGUILayout.Space(3);
            EditorGUILayout.LabelField("BGM Directory List");
            EditorGUILayout.TextArea(_audioBGMDirectoryPathText);
            EditorGUILayout.Space(5);
            EditorGUILayout.LabelField("SE Directory List");
            EditorGUILayout.TextArea(_audioSEDirectoryPathText);
            EditorGUILayout.Space(5);
            EditorGUILayout.LabelField("Custom1 Directory List");
            EditorGUILayout.TextArea(_audioCustom1DirectoryPathText);
            EditorGUILayout.Space(5);
            EditorGUILayout.LabelField("Custom2 Directory List");
            EditorGUILayout.TextArea(_audioCustom2DirectoryPathText);
            EditorGUILayout.Space(5);
            EditorGUILayout.LabelField("Custom3 Directory List");
            EditorGUILayout.TextArea(_audioCustom3DirectoryPathText);
            EditorGUILayout.Space(5);
            EditorGUILayout.LabelField("Other Directory are Default Config");
            EditorGUILayout.Space(20);
            
                        
            EditorGUILayout.LabelField("Video");
            EditorGUILayout.Space(3);
            EditorGUILayout.LabelField("Normal Directory List");
            EditorGUILayout.TextArea(_videoNormalDirectoryPathText);
            EditorGUILayout.Space(5);
            EditorGUILayout.LabelField("Custom1 Directory List");
            EditorGUILayout.TextArea(_videoCustom1DirectoryPathText);
            EditorGUILayout.Space(5);
            EditorGUILayout.LabelField("Custom2 Directory List");
            EditorGUILayout.TextArea(_videoCustom2DirectoryPathText);
            EditorGUILayout.Space(5);
            EditorGUILayout.LabelField("Custom3 Directory List");
            EditorGUILayout.TextArea(_videoCustom3DirectoryPathText);
            EditorGUILayout.Space(5);
            EditorGUILayout.LabelField("Other Directory are Default Config");
            EditorGUILayout.Space(20);

            
            
            EditorGUILayout.EndVertical();
            

            EditorGUILayout.Space();


            EditorGUILayout.EndScrollView();


            
            
        }
    }
}