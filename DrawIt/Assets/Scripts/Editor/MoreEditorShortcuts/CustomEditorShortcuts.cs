using System;
using System.Reflection;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MoreEditorShortcuts
{
    public class CustomEditorShortcuts
    {
        [MenuItem("EditorUtils/Editor/Log Window Type Name &q")]
        public static void DebugWindowType()
        {
            EditorWindow currentWindow = EditorWindow.mouseOverWindow;
            
            if (currentWindow != null)
            {
                Debug.Log($"Window NAME: {currentWindow.GetType().Name}");
            }
        }
        
        [MenuItem("EditorUtils/Editor/Toggle Inspector Mode &d")]
        public static void ToggleInspectorMode()
        {
            if(!TryGetMouseOverWindowByName("InspectorWindow", out EditorWindow currentWindow)) return;
            
            Type inspectorWindowType =
                Assembly.GetAssembly(typeof(Editor)).GetType("UnityEditor.InspectorWindow");
            FieldInfo inspectorModeInfoField =
                inspectorWindowType.GetField("m_InspectorMode", BindingFlags.NonPublic | BindingFlags.Instance);

            InspectorMode currentMode = (InspectorMode)inspectorModeInfoField.GetValue(currentWindow);
            InspectorMode newMode = currentMode == InspectorMode.Normal ? InspectorMode.Debug : InspectorMode.Normal;

            MethodInfo changeTypeMethod =
                inspectorWindowType.GetMethod("SetMode", BindingFlags.NonPublic | BindingFlags.Instance);
            changeTypeMethod.Invoke(currentWindow, new object[] {newMode});
                
            currentWindow.Repaint();
                
            EditorShortcutsDebug.Log($"New Inspector mode > {newMode}");
        }
        
        [MenuItem("EditorUtils/Editor/Collapse Hierarchy &a")]
        public static void CollapseAll()
        {
            if(!TryGetMouseOverWindowByName("SceneHierarchyWindow", out EditorWindow currentWindow)) return;
            
            Type sceneHierarchyWindowType =
                Assembly.GetAssembly(typeof(Editor)).GetType("UnityEditor.SceneHierarchyWindow");
            FieldInfo sceneHierarchyField =
                sceneHierarchyWindowType.GetField("m_SceneHierarchy", BindingFlags.NonPublic | BindingFlags.Instance);
                
            Type sceneHierarchyType = Assembly.GetAssembly(typeof(Editor)).GetType("UnityEditor.SceneHierarchy");
            MethodInfo collapseWindowType =
                sceneHierarchyType.GetMethod("CollapseAll", BindingFlags.Public | BindingFlags.Instance);
                
            collapseWindowType.Invoke(sceneHierarchyField.GetValue(currentWindow), null);
        }

        private static bool TryGetMouseOverWindowByName(string windowName, out EditorWindow mouseOverWindow)
        {
            mouseOverWindow = EditorWindow.mouseOverWindow;
            
            if (mouseOverWindow == null)
            {
                EditorShortcutsDebug.LogWarning("Current Window was Null");
                return false;
            }
            
            if (mouseOverWindow.GetType().Name != windowName)
            {
                EditorShortcutsDebug.LogWarning("Wrong Window Type");
                return false;
            }
            
            return true;
        }
        
        [MenuItem("EditorUtils/Editor/Toggle Playmode _F5")]
        public static void TogglePlaymode()
        {
            if (!Application.isPlaying)
            {
                EditorSceneManager.SaveScene(SceneManager.GetActiveScene());
            }
            
            if(Application.isPlaying)
                EditorApplication.ExitPlaymode();
            else 
                EditorApplication.EnterPlaymode();
            
            EditorShortcutsDebug.Log("Playmode toggled!");
        }
    }
}