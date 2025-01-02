using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace MoreEditorShortcuts
{
    public class ComponentsSelector
    {
        [MenuItem("EditorUtils/Select Components &z")]
        public static void SelectComponents()
        {
            InputWindow inputWindow = (InputWindow) EditorWindow.GetWindow(typeof(InputWindow));
            EditorWindow.FocusWindowIfItsOpen<InputWindow>();
            inputWindow.OnSubmit += SelectComponentsFromSelectedIfPossible;
            inputWindow.OnSubmit += _ => inputWindow.Close();
        }

        private static void SelectComponentsFromSelectedIfPossible(string componentName)
        {
            List<GameObject> selected = new();
            foreach (Transform item in Selection.transforms)
            {
                selected.Add(item.gameObject);
                selected.AddRange(item.GetComponentsInChildren<Transform>(true).Select(x => x.gameObject));
            }
            List<GameObject> chosen = selected.Where(o => o.GetComponent(componentName)).ToList();
            Selection.objects = chosen.Select(chosenObject => chosenObject as Object).ToArray();
            
            EditorShortcutsDebug.Log($"Component name: {componentName}");
            EditorShortcutsDebug.Log($"Number of selected items (Before): {selected.Count}");
            EditorShortcutsDebug.Log($"Number of selected items (After): {chosen.Count}");
        }
    }
}