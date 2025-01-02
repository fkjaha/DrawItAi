using UnityEditor;
using UnityEngine;

namespace MoreEditorShortcuts
{
    public static class RememberEditorSelection
    {
        private static Object[] _rememberedSelection;

        [MenuItem("EditorUtils/Selection/Remember Selection %M3")]
        public static void RememberSelection()
        {
            _rememberedSelection = Selection.objects;

            EditorShortcutsDebug.Log("Remembered Selection");
        }

        [MenuItem("EditorUtils/Selection/Select Remembered Selection %M4")]
        public static void SelectRememberedSelection()
        {
            if (_rememberedSelection == null)
            {
                EditorShortcutsDebug.LogWarning("Remembered Selection doesnt exist");
                return;
            }

            Selection.objects = _rememberedSelection;

            EditorShortcutsDebug.Log("Remembered Selection Selected");
        }
    }
    
}