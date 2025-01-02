using UnityEditor;
using UnityEngine;

namespace MoreEditorShortcuts
{
    public static class EditorShortcutsDebug
    {
        private static bool _debugEnabled;

        [MenuItem("EditorUtils/Debug/Toggle Debug %&#d")]
        public static void ToggleDebugChangesState()
        {
            _debugEnabled = !_debugEnabled;
        }

        [MenuItem("EditorUtils/Debug/Debug Debug State")]
        public static void DebugCurrentDebugChangesState()
        {
            Debug.Log($"Debug Changes: {_debugEnabled}");
        }

        public static void Log(string message)
        {
            if (_debugEnabled)
                Debug.Log(message);
        }

        public static void LogWarning(string message)
        {
            if (_debugEnabled)
                Debug.LogWarning(message);
        }
    }
}