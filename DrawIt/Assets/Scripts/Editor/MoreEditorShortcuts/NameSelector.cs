using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace MoreEditorShortcuts
{
    public class NameSelector
    {
        [MenuItem("EditorUtils/Select Names #z")]
        public static void SelectComponents()
        {
            InputWindow inputWindow = (InputWindow) EditorWindow.GetWindow(typeof(InputWindow));
            EditorWindow.FocusWindowIfItsOpen<InputWindow>();
            inputWindow.OnSubmit += SelectComponentsFromSelectedIfPossible;
            inputWindow.OnSubmit += _ => inputWindow.Close();
        }

        private static void SelectComponentsFromSelectedIfPossible(string objectsName)
        {
            List<GameObject> selected = new();
            foreach (Transform item in Selection.transforms)
            {
                selected.Add(item.gameObject);
                selected.AddRange(item.GetComponentsInChildren<Transform>(true).Select(x => x.gameObject));
            }
            List<GameObject> chosen = selected.Where(o => o.name == objectsName).ToList();
            Selection.objects = chosen.Select(chosenObject => chosenObject as Object).ToArray();
            
            EditorShortcutsDebug.Log($"Objects name: {objectsName}");
            EditorShortcutsDebug.Log($"Number of selected items (Before): {selected.Count}");
            EditorShortcutsDebug.Log($"Number of selected items (After): {chosen.Count}");
        }
    }
}