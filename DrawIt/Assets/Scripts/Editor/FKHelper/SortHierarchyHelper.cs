using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace FKHelper
{
    [Serializable]
    public class SortHierarchyHelper : FkHelperModule
    {
        [SerializeField] private bool invertSorting;

        public override string GetClassName()
        {
            return "Sort Hierarchy Elements";
        }

        public override void RenderGUI()
        {
            GUILayout.Label("Sorts only selected elements!");
            
            GUILayout.BeginHorizontal();
            
            invertSorting = GUILayout.Toggle(invertSorting, "Invert sorting");
            
            GUILayout.EndHorizontal();
        }

        public override void DoAction()
        {
            SortSelectedObjectsInHierarchy();
        }
    
        private void SortSelectedObjectsInHierarchy()
        {
            Transform[] selectedElements = Selection.transforms;
            Transform[] sortedElements = selectedElements.OrderBy(element =>
            {
                string elementName = element.name;

                if (int.TryParse(Regex.Match(elementName, @"\d+").Value, out int number)) return number;
                
                return 0;
            }).ToArray();
            
            Undo.RegisterFullObjectHierarchyUndo(sortedElements[0].parent, "Sort Hierarchy");
            
            for (int i = 0; i < sortedElements.Length; i++)
            {
                Transform element = sortedElements[i];
                element.SetSiblingIndex(i);
            }
            
            Debug.Log("Sorted!");
        }
    }
}