using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace FKHelper
{
    [Serializable]
    public class GridSpawnHelper : FkHelperModule
    {
        [SerializeField] private Vector3Int gridSize;
        [SerializeField] private Vector3 gridSpacing;
        [SerializeField] private GameObject gridPrefab;
        [SerializeField] private Transform gridOriginAndParent;
        [SerializeField] private List<GameObject> gridObjects;
        [SerializeField] private bool gridSpawnAsPrefab = true;
        
        public override string GetClassName()
        {
            return "Grid Spawn";
        }

        public override void RenderGUI()
        {
            GUILayout.BeginHorizontal();
            
            GUILayout.Label("Spawn Prefab: ");
            gridPrefab = EditorGUILayout.ObjectField(gridPrefab, typeof(GameObject), true) as GameObject;
            
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            
            GUILayout.Label("Spawn Origin & Parent:");
            gridOriginAndParent =
                EditorGUILayout.ObjectField(gridOriginAndParent, typeof(Transform), true) as Transform;
            
            GUILayout.EndHorizontal();
            
            gridSpawnAsPrefab = GUILayout.Toggle(gridSpawnAsPrefab, "Grid spawn as prefab ");
            gridSize = EditorGUILayout.Vector3IntField("Grid size: ", gridSize);
            gridSpacing = EditorGUILayout.Vector3Field("Grid spacing: ", gridSpacing);
        }

        public override void DoAction()
        {
            GridSpawn();
        }
        
        private void GridSpawn()
        {
            if (gridPrefab == null || gridOriginAndParent == null) return;

            gridObjects = new();
            for (int i = 0; i < gridSize.x; i++)
            {
                for (int j = 0; j < gridSize.y; j++)
                {
                    for (int k = 0; k < gridSize.z; k++)
                    {
                        SpawnGridObject(i, j, k);
                    }
                }
            }
            // Selection.objects = _gridObjects.ToArray();
        }

        private void SpawnGridObject(int x, int y, int z)
        {
            Vector3 spawnPosition = GetSpawnPosition(x, y, z);

            GameObject spawnedObject = gridSpawnAsPrefab
                ? SpawnGridObjectAsPrefab(spawnPosition)
                : SpawnGridObjectAsInstance(spawnPosition);

            if (spawnedObject == null)
            {
                Debug.LogWarning("Spawned NULL object double-check all the fields.");
                return;
            }
            gridObjects.Add(spawnedObject);
            Undo.RegisterCreatedObjectUndo(spawnedObject, "Spawned Grid Objects");
        }

        private GameObject SpawnGridObjectAsPrefab(Vector3 spawnPosition)
        {
            GameObject spawned = PrefabUtility.InstantiatePrefab(gridPrefab) as GameObject;
            if (spawned != null)
            {
                spawned.transform.position = spawnPosition;
                spawned.transform.parent = gridOriginAndParent;
            }
            return spawned;
        }

        private GameObject SpawnGridObjectAsInstance(Vector3 spawnPosition)
        {
            GameObject spawned = Object.Instantiate(gridPrefab, spawnPosition, Quaternion.identity,
                gridOriginAndParent);
            return spawned;
        }

        private Vector3 GetSpawnPosition(int x, int y, int z)
        {
            return new Vector3(x * gridSpacing.x, y * gridSpacing.y, z * gridSpacing.z) +
                   gridOriginAndParent.position;
        }
    }
}