using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace FKHelper
{
    [Serializable]
    public class ReplaceWithPrefabsHelper : FkHelperModule
    {
        [SerializeField] private bool findClonesFromSelected = true;
        [SerializeField] private bool ignorePrefixPosition;
        [SerializeField] private string clonePostfix = "(Clone)";
        [SerializeField] private GameObject clonesPrefab;
        
        public override string GetClassName()
        {
            return "Replace Clones with Prefabs";
        }

        public override void RenderGUI()
        {
            findClonesFromSelected =
                GUILayout.Toggle(findClonesFromSelected, "Find Clones from selected objects");
            
            ignorePrefixPosition = GUILayout.Toggle(ignorePrefixPosition, "Ignore Prefix Position" );
            if (!findClonesFromSelected)
                GUILayout.Box(
                    "If 'Find Clones from selected objects' is disabled execution might crush Unity in case you have too many objects on scene!");

            GUILayout.BeginHorizontal();
            
            GUILayout.Label("Clones name postfix:");
            clonePostfix = GUILayout.TextField(clonePostfix, 25);
            
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            
            GUILayout.Label("Clones prefab:");
            clonesPrefab = EditorGUILayout.ObjectField(clonesPrefab, typeof(GameObject), true) as GameObject;
            
            GUILayout.EndHorizontal();
        }

        public override void DoAction()
        {
            ReplaceClonesWithPrefabs();
        }
        
        private void ReplaceClonesWithPrefabs()
        {
            List<Transform> operateOnObjects =
                findClonesFromSelected ? Selection.transforms.ToList() : Object.FindObjectsOfType<Transform>().ToList();
            List<GameObject> clones = GetClones(operateOnObjects);
            
            foreach (GameObject spawnedClone in clones)
            {
                GameObject newObject = PrefabUtility.InstantiatePrefab(clonesPrefab) as GameObject;
                Undo.RegisterCreatedObjectUndo(newObject, "Prefabs Replaced Clones");
                if (newObject != null)
                {
                    newObject.transform.position = spawnedClone.transform.position;
                    newObject.transform.rotation = spawnedClone.transform.rotation;
                    newObject.transform.parent = spawnedClone.transform.parent;
                }

                Undo.DestroyObjectImmediate(spawnedClone);
            }

            Debug.Log("Replaced!");
        }

        private List<GameObject> GetClones(List<Transform> operateOnObjects)
        {
            if (!ignorePrefixPosition)
            {
                return operateOnObjects
                    .Where(x => x.name.StartsWith(clonesPrefab.name + clonePostfix)).Select(x => x.gameObject).ToList();
            }

            return operateOnObjects
                .Where(x => x.name.Contains(clonePostfix)).Select(x => x.gameObject).ToList();
        }
    }
}