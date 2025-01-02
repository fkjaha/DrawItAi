using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace FKHelper
{
    [Serializable]
    public class AlignHelper : FkHelperModule
    {
        [SerializeField] private bool alignX;
        [SerializeField] private bool alignY;
        [SerializeField] private bool alignZ;

        [SerializeField] private Transform alignPointA;
        [SerializeField] private Transform alignPointB;

        [SerializeField] private Vector3 alignOffset;
    
        public override string GetClassName()
        {
            return "Align";
        }

        public override void RenderGUI()
        {
            GUILayout.BeginHorizontal();
            
            GUILayout.Label("Axis:");
            alignX = GUILayout.Toggle(alignX, "X");
            alignY = GUILayout.Toggle(alignY, "Y");
            alignZ = GUILayout.Toggle(alignZ, "Z");
            
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            
            GUILayout.Label("Point A:");
            alignPointA = EditorGUILayout.ObjectField(alignPointA, typeof(Transform), true) as Transform;
            
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            
            GUILayout.Label("Point B:");
            alignPointB = EditorGUILayout.ObjectField(alignPointB, typeof(Transform), true) as Transform;
            
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            
            GUILayout.Label("Align Offset:");
            alignOffset = EditorGUILayout.Vector3Field("", alignOffset);
            
            GUILayout.EndHorizontal();
        }

        public override void DoAction()
        {
            AlignSelected();
        }
    
        private void AlignSelected()
        {
            if (!alignX && !alignY && !alignZ) return;

            List<Transform> transforms = Selection.transforms.Where(t => t != alignPointA && t != alignPointB).ToList();
            int transformsCount = transforms.Count;
            Vector3 vector3Step = (alignPointB.position - alignPointA.position) / (transformsCount + 1);

            Debug.Log(transformsCount);
            Undo.RecordObjects(transforms.ToArray(), "Align Objects");

            for (int i = 0; i < transformsCount; i++)
            {
                transforms[i].position = new Vector3(
                    alignX ? alignPointA.position.x + vector3Step.x * (i + 1) : transforms[i].position.x,
                    alignY ? alignPointA.position.y + vector3Step.y * (i + 1) : transforms[i].position.y,
                    alignZ ? alignPointA.position.z + vector3Step.z * (i + 1) : transforms[i].position.z);
            }

            Debug.Log("Aligned!");
        }
    }
}