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
                                    "初回インポート時のみ、最低限のフォーマット設定のみを行うようになっています。\n" +
                                    "CommonConfig.csの定義によって、どのディレクトリをどのフォーマット設定にするか決めています。\n" +
                                    "上記ディレクトリに該当しないファイルには、最低限の共通設定を行います。\n" +
                                    "\n\n" +
                                    "設定について\n" +
                                    "settingwindowのSkipAllは、このEditor拡張のフォーマットをスキップするか。\n" +
                                    "settingwindowのEveryImportTimeは、初回インポート以外でもフォーマット設定するか。",MessageType.None);
            
            EditorGUILayout.HelpBox("This Editor extension automatically sets up video, music, and textures\n" +
                                    "Only minimal formatting is required for the first time import.\n" +
                                    "The definition of CommonConfig.cs determines which directory is used for which format settings.\n" +
                                    "For files that do not fall under the above directories, a minimum common configuration is performed.\n" +
                                    "\n\n" +
                                    "About Settings\n" +
                                    "Does SkipAll in the settingwindow skip the formatting of this Editor extension?\n" +
                                    "Does the EveryImportTime in the settingwindow set the formatting for other than the first import?",MessageType.None);
            
            
            EditorGUILayout.Space(30);
            
            
            
            EditorGUILayout.EndVertical();

            EditorGUILayout.Space();
            

            
            
        }
    }
}