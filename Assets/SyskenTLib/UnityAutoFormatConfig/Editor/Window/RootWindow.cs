using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SyskenTLib.UnityAutoFormatConfig.Editor
{
    public class RootWindow : EditorWindow
    {
        /// <Summary>
        /// Rootの定義(らいぶらりごとにメニューに区切りをつけるためのダミー）
        /// </Summary>
        [MenuItem("SyskenTLib/AutoFormatConfigWindow/",priority = 30)]
        private static void OpenRoot()
        {
        }
    }
}
