using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace FKHelper
{
    [Serializable]
    public class RenamingHelper : FkHelperModule
    {
        [SerializeField] private string nameToAppendIndex = "";
        [SerializeField] private int startIndex;

        public override string GetClassName()
        {
            return "Renaming";
        }

        public override void RenderGUI()
        {
            try
            {
                GUILayout.BeginHorizontal();
                
                GUILayout.Label("Start index:");
                startIndex = Int32.Parse(GUILayout.TextField("" + startIndex, 6));
                
                GUILayout.EndHorizontal();
            }
            catch (Exception)
            {
                Debug.Log("Wrong <start index>");
            }

            GUILayout.BeginHorizontal();
            
            GUILayout.Label("Name to append index:");
            nameToAppendIndex = GUILayout.TextField(nameToAppendIndex, 50);
            
            GUILayout.EndHorizontal();
        }

        public override void DoAction()
        {
            RenameSelected();
        }
    
        private void RenameSelected()
        {
            List<GameObject> sortedList = Selection.transforms
                .OrderBy(selectedTransform => selectedTransform.transform.GetSiblingIndex())
                .Select(selectedTransform => selectedTransform.gameObject)
                .ToList();
        
            Undo.RecordObjects(sortedList.ToArray(), "Renamings");

            for (int i = 0; i < sortedList.Count; i++)
            {
                sortedList[i].name = nameToAppendIndex + (startIndex + i);
            }

            Debug.Log("Renamed!");
        }
    }
}