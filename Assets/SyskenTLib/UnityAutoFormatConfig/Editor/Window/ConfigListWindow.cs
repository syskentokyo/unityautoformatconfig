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
        
        
        
        /// <Summary>
        /// 設定を表示します。
        /// </Summary>
        [MenuItem("SyskenTLib/AutoFormatConfigWindow/ConfigList",priority = 30)]
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

                isMustUpdate = false;
            }
            
            
            
            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition); 
            
            EditorGUILayout.BeginVertical("Box");

            if (GUILayout.Button("表示更新"))
            {
                //表示更新
                isMustUpdate = true;
            }

            EditorGUILayout.LabelField("Texture");
            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField("NormalUI Directory List");
            EditorGUILayout.TextArea(_textureNormalUIDirectoryPathText);
            EditorGUILayout.Space(5);
            EditorGUILayout.LabelField("DotUI Directory List");
            EditorGUILayout.TextArea(_textureDotUIDirectoryPathText);
            EditorGUILayout.Space(5);
            EditorGUILayout.LabelField("Other Directory are Default Config");
            EditorGUILayout.LabelField("If you change target Directory , you see CommonConfig.cs please");
            EditorGUILayout.Space(60);

            
            
            EditorGUILayout.EndVertical();
            

            EditorGUILayout.Space();


            EditorGUILayout.EndScrollView();


            
            
        }
    }
}