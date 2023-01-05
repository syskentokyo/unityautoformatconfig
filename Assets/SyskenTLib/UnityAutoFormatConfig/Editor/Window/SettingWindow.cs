using UnityEditor;
using UnityEngine;

namespace SyskenTLib.UnityAutoFormatConfig.Editor
{
    public class SettingWindow:EditorWindow
    { 
        
        
        
        
        SaveDataManager _saveDataManager = new SaveDataManager();
        
        
        private bool isMustUpdate = true;
        
       
        
        //各パラメータ
        private bool _isAllSkipTexture = false;
        private bool _isEveryImportTimeChangeConfigTexture = false;
        private bool _isAllSkipAudio = false;
        private bool _isEveryImportTimeChangeConfigAudio = false;
        private bool _isAllSkipVideo = false;
        private bool _isEveryImportTimeChangeConfigVideo = false;
        
        
        
        /// <Summary>
        /// 設定を表示します。
        /// </Summary>
        [MenuItem("SyskenTLib/AutoFormatConfigWindow/Setting",priority = 8)]
        private static void OpenSetting()
        {
            var window = GetWindow<SettingWindow>();
            window.titleContent = new GUIContent("AutoFormat-Setting");
        }
        
        // <Summary>
        /// ウィンドウのパーツを表示します。
        /// </Summary>
        void OnGUI()
        {
            if (isMustUpdate)
            {
                //保存されていた設定値を読み込み
                _isAllSkipTexture = _saveDataManager.ReadUserConfigBool(CommonDefine.isAllSkipTextureKEY);
                _isEveryImportTimeChangeConfigTexture = _saveDataManager.ReadUserConfigBool(CommonDefine._isEveryImportTimeChangeConfigTextureKey);
                _isAllSkipAudio = _saveDataManager.ReadUserConfigBool(CommonDefine.isAllSkipAudioKEY);
                _isEveryImportTimeChangeConfigAudio = _saveDataManager.ReadUserConfigBool(CommonDefine._isEveryImportTimeChangeConfigAudioeKey);
                _isAllSkipVideo = _saveDataManager.ReadUserConfigBool(CommonDefine.isAllSkipVideoKEY);
                _isEveryImportTimeChangeConfigVideo = _saveDataManager.ReadUserConfigBool(CommonDefine._isEveryImportTimeChangeConfigVideoKey);

                isMustUpdate = false;
            }
            
            
            EditorGUILayout.BeginVertical("Box");

            
            
            EditorGUILayout.LabelField("Texture Config");
            _isAllSkipTexture= EditorGUILayout.Toggle ("SkipAll", _isAllSkipTexture);
            _isEveryImportTimeChangeConfigTexture=EditorGUILayout.Toggle ("EveryImportTime", _isEveryImportTimeChangeConfigTexture);
            EditorGUILayout.Space(30);
            
            EditorGUILayout.LabelField("Audio Config");
            _isAllSkipAudio=EditorGUILayout.Toggle ("SkipAll", _isAllSkipAudio);
            _isEveryImportTimeChangeConfigAudio=EditorGUILayout.Toggle ("EveryImportTime", _isEveryImportTimeChangeConfigAudio);
            EditorGUILayout.Space(30);
            
            EditorGUILayout.LabelField("Video Config");
            _isAllSkipVideo=EditorGUILayout.Toggle ("SkipAll", _isAllSkipVideo);
            _isEveryImportTimeChangeConfigVideo= EditorGUILayout.Toggle ("EveryImportTime", _isEveryImportTimeChangeConfigVideo);
            EditorGUILayout.Space(30);
            
            
            
            EditorGUILayout.EndVertical();

            EditorGUILayout.Space();



            if(EditorGUI.EndChangeCheck ())
            {
                //Editor設定値に変更があった場合
                
                //
                // 保存
                // 
                _saveDataManager.SaveUserConfigBool(CommonDefine.isAllSkipTextureKEY,_isAllSkipTexture);
                _saveDataManager.SaveUserConfigBool(CommonDefine._isEveryImportTimeChangeConfigTextureKey,_isEveryImportTimeChangeConfigTexture);
                _saveDataManager.SaveUserConfigBool(CommonDefine.isAllSkipAudioKEY,_isAllSkipAudio);
                _saveDataManager.SaveUserConfigBool(CommonDefine._isEveryImportTimeChangeConfigAudioeKey,_isEveryImportTimeChangeConfigAudio);
                _saveDataManager.SaveUserConfigBool(CommonDefine.isAllSkipVideoKEY,_isAllSkipVideo);
                _saveDataManager.SaveUserConfigBool(CommonDefine._isEveryImportTimeChangeConfigVideoKey,_isEveryImportTimeChangeConfigVideo);
                
                //設定からGUIに反映させる
                isMustUpdate = true;
            }

            
            
        }
    }
}