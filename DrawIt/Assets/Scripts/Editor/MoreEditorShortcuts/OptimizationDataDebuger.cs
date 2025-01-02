using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace MoreEditorShortcuts
{
    public class OptimizationDataDebuger
    {
        [MenuItem("EditorUtils/Calculate Triangles")]
        public static void CalculateTriangles()
        {
            StringBuilder newBuilder = new();
            MeshFilter[] allFilters = Object.FindObjectsOfType<MeshFilter>().OrderByDescending(filter => filter.sharedMesh.triangles.Length).ToArray();
            foreach (MeshFilter meshFilter in allFilters)
            {
                int triangleCount = meshFilter.sharedMesh.triangles.Length / 3;
                newBuilder.Append($"Nm: {meshFilter.gameObject.name} |Tr: {triangleCount} |MNm: {meshFilter.sharedMesh.name} \n");
            }
            Debug.Log($"Counted triangles for [{allFilters.Length}] Mesh Filters \n {newBuilder}");
        }
        
        [MenuItem("EditorUtils/Calculate Trees")]
        public static void CalculateTrees()
        {
            StringBuilder newBuilder = new();
            Terrain[] allTerrains = Object.FindObjectsOfType<Terrain>().ToArray();
            int overallCount = 0;
            foreach (Terrain terrain in allTerrains)
            {
                int treeCount = terrain.terrainData.treeInstanceCount;
                overallCount += treeCount;
                newBuilder.Append($"Terrain: {terrain.gameObject.name} | Trees: {treeCount}\n");
            }
            Debug.Log($"Counted Trees for [{allTerrains.Length}] Terrains: {overallCount} \n {newBuilder}");
        }
    }
}