using UnityEditor;
using UnityEngine;

namespace SyskenTLib.UnityAutoFormatConfig.Editor
{
    public class AboutWindow:EditorWindow
    {
        
        /// <Summary>
        /// 設定を表示します。
        /// </Summary>
        [MenuItem("SyskenTLib/AutoFormatConfigWindow/About",priority = 4)]
        private static void OpenSetting()
        {
            var window = GetWindow<AboutWindow>();
            window.titleContent = new GUIContent("AutoFormat-About");
        }
        
        // <Summary>
        /// ウィンドウのパーツを表示します。
        /// </Summary>
        void OnGUI()
        {

            
            EditorGUILayout.BeginVertical("Box");

            
            
            EditorGUILayout.LabelField("About");
            
            
            EditorGUILayout.HelpBox("このEditor拡張は、自動で動画、音楽、テクスチャの設定を行います。\n" +
                                    "https://github.com/syskentokyo/unityautoformatconfig/wiki",MessageType.None);
            
            EditorGUILayout.HelpBox("This Editor extension automatically sets up video, music, and textures\n" +
                                    "https://github.com/syskentokyo/unityautoformatconfig/wiki",MessageType.None);
            
            
            EditorGUILayout.Space(30);
            
            
            
            EditorGUILayout.EndVertical();

            EditorGUILayout.Space();
            

            
            
        }
    }
}