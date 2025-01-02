using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace FKHelper
{
    [Serializable]
    public class SelectRandomHelper : FkHelperModule
    {
        [SerializeField] private int selectionChance = 50;
        
        public override string GetClassName()
        {
            return "Select Random Selected";
        }

        public override void RenderGUI()
        {
            try
            {
                GUILayout.BeginHorizontal();
                
                GUILayout.Label("Selection chance:");
                
                string value = GUILayout.TextField("" + selectionChance, 3);
                if (value == String.Empty) value = "0";
                selectionChance = Int32.Parse(value);
                
                GUILayout.EndHorizontal();
            }
            catch (Exception e)
            {
                Debug.Log($"Wrong <start index> {e.Message}");
            }
            
        }

        public override void DoAction()
        {
            SelectRandomFromSelected();
        }
        
        private void SelectRandomFromSelected()
        {
            List<GameObject> selected = Selection.transforms.Select(x => x.gameObject).ToList();
            List<GameObject> chosen = selected.Where(_ => UnityEngine.Random.Range(0, 101) <= selectionChance).ToList();
            Undo.RecordObjects(chosen.ToArray(), "Select Random");
            Selection.objects = chosen.Select(chosenObject => chosenObject as Object).ToArray();
        }
    }
}