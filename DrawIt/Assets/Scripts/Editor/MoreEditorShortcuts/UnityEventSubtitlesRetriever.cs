using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace MoreEditorShortcuts
{
    public class UnityEventSubtitlesRetriever
    {
        public static  string targetClassName = "SubtitlesMediator"; // Name of the class
        public static  string targetMethodName = "AddSubtitlesToQueueIfNotPresent"; // Name of the method

        [MenuItem("EditorUtils/Localization/Find Subtitles On Scene")]
        private static void Retrieve()
        {
            // Iterate through all active GameObjects in the scene
            foreach (GameObject go in Object.FindObjectsOfType<GameObject>())
            {
                // Get all components attached to the GameObject
                Component[] components = go.GetComponents<Component>();

                foreach (Component component in components)
                {
                    // Iterate through all fields in the component
                    FieldInfo[] fields = component.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                    foreach (FieldInfo field in fields)
                    {
                        // Look for UnityEvents
                        if (!typeof(UnityEventBase).IsAssignableFrom(field.FieldType)) continue;
                        
                        UnityEvent unityEvent = field.GetValue(component) as UnityEvent;
                            
                        if (unityEvent == null) continue;
                            
                        // Check each listener in the UnityEvent
                        for (int i = 0; i < unityEvent.GetPersistentEventCount(); i++)
                        {
                            // Get the method and the target object (the class instance)
                            Object targetObject = unityEvent.GetPersistentTarget(i);
                            string methodName = unityEvent.GetPersistentMethodName(i);

                            // Check if the target object and method match the one you're looking for
                            if (targetObject == null || targetObject.GetType().Name != targetClassName ||
                                methodName != targetMethodName) continue;
                            
                            Debug.Log($"Found match on GameObject: {go.name}, Component: {component.GetType().Name}, Field: {field.Name}");

                            string stringParameter = GetStringParameter(component, i);
                            if (!string.IsNullOrEmpty(stringParameter))
                            {
                                Debug.Log($"String parameter: {stringParameter}");
                            }
                        }
                    }
                }
            }
        }
        
        private static string GetStringParameter(Component component, int index)
        {
            // The UnityEditor namespace is required to access serialized event parameters
#if UNITY_EDITOR
            // var serializedObject = new UnityEditor.SerializedObject(unityEvent);
            var serializedObject = new SerializedObject(component); // 'component' is the MonoBehaviour or ScriptableObject holding the UnityEvent
            var callsProperty = serializedObject.FindProperty("m_PersistentCalls.m_Calls");

            if (callsProperty != null && index < callsProperty.arraySize)
            {
                var callProperty = callsProperty.GetArrayElementAtIndex(index);
                var argumentsProperty = callProperty.FindPropertyRelative("m_Arguments.m_StringArgument");
                if (argumentsProperty != null)
                {
                    return argumentsProperty.stringValue;
                }
            }
#endif

            return null; // Return null if no string parameter is found
        }
    }
}