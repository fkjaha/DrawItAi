using System;
using UnityEditor;
using UnityEngine;

namespace FKHelper
{
    [Serializable]
    public class RelativePlaceHelper : FkHelperModule
    {
        [SerializeField] private Transform objectToPlaceRelatively;
        [SerializeField] private Transform originalRelativeTransform;
        [SerializeField] private Transform newRelativeTransform;

        public override string GetClassName()
        {
            return "Relative Placer";
        }

        public override void RenderGUI()
        {
            GUILayout.BeginHorizontal();
            
            GUILayout.Label("Object To Move:");
            objectToPlaceRelatively = EditorGUILayout.ObjectField(objectToPlaceRelatively, typeof(Transform), true) as Transform;
            
            GUILayout.EndHorizontal();     
            GUILayout.BeginHorizontal();
            
            GUILayout.Label("Original Relative Transform:");
            originalRelativeTransform = EditorGUILayout.ObjectField(originalRelativeTransform, typeof(Transform), true) as Transform;
            
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            
            GUILayout.Label("New Relative Transform:");
            newRelativeTransform = EditorGUILayout.ObjectField(newRelativeTransform, typeof(Transform), true) as Transform;
            
            GUILayout.EndHorizontal();
        }

        public override void DoAction()
        {
            PlaceRelatively();
        }
        
        private void PlaceRelatively()
        {
            if (objectToPlaceRelatively == null || originalRelativeTransform == null || newRelativeTransform == null) return;

            Undo.RecordObject(objectToPlaceRelatively, "Object Replace");
            objectToPlaceRelatively.position =
                newRelativeTransform.TransformPoint(
                    originalRelativeTransform.InverseTransformPoint(objectToPlaceRelatively.position));
        }
    }
}