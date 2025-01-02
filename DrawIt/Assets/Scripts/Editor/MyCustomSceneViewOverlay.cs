using System;
using UnityEditor;
using UnityEditor.Overlays;
using UnityEngine;

[Overlay(typeof(SceneView), "My Overlay")]
public class MyCustomSceneViewOverlay : IMGUIOverlay
{
    private CameraState _savedState;

    public override void OnGUI()
    {
        if (GUILayout.Button("Save Preset"))
        {
            _savedState = new CameraState(GetSceneViewCamera().transform);
        }
        
        if (GUILayout.Button("Load Preset"))
        {
            LoadCameraPreset(_savedState);
        }
        
        GUILayout.Label($"{_savedState.position}");
        GUILayout.Label($"{_savedState.rotation}");
    }

    private void LoadCameraPreset(CameraState cameraPreset)
    {
        Transform cameraTransform = GetSceneViewCamera().transform;
        // cameraTransform.position = cameraPreset.position;
        // cameraTransform.rotation = cameraPreset.rotation;
        cameraTransform.SetPositionAndRotation(cameraPreset.position, cameraPreset.rotation);
        Debug.Log($"1: {cameraPreset.position} | {cameraPreset.rotation}");
        Debug.Log($"2: {cameraTransform.position} | {cameraTransform.rotation}");
        
        SceneView.lastActiveSceneView.LookAt(cameraPreset.position, cameraPreset.rotation, GetSceneViewCamera().orthographicSize, false, true);
    }

    private Camera GetSceneViewCamera()
    {
        return SceneView.lastActiveSceneView.camera;
    }
}

[Serializable]
public struct CameraState
{
    public Vector3 position;
    public Quaternion rotation;

    public CameraState(Transform cameraTransform)
    {
        position = cameraTransform.position;
        rotation = cameraTransform.rotation;
    }
}