using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace FKHelper 
{
    public class FKHelperWindow : EditorWindow
    {
        private List<FkHelperModule> _helperModules;

        private const string SaveKey = "FkhelperWindows";
        private int _modeIndex;

        [MenuItem("Window/FK-HELPER")]
        public static void ShowWindow()
        {
            GetWindow(typeof(FKHelperWindow), false, "FK-HELPER");
        }

        private void TryInitializeClasses()
        {
            EditorPrefs.DeleteKey(SaveKey);

            if(HelperModulesInitialized()) return;
            
            _helperModules = new List<FkHelperModule>
            {
                new RenamingHelper(),
                new AlignHelper(),
                new ReplaceWithPrefabsHelper(),
                new GridSpawnHelper(),
                new RelativePlaceHelper(),
                new SelectRandomHelper(),
                new SortHierarchyHelper()
            };

            LoadModulesData();
        }

        private bool HelperModulesInitialized()
        {
            return _helperModules != null && _helperModules.Count > 0 && !_helperModules.Contains(null);
        }

        private void OnGUI()
        {
            TryInitializeClasses();
            
            GUILayout.BeginVertical();
            GUILayout.Space(5);
        
            _modeIndex = EditorGUILayout.Popup("Helper mode: ", _modeIndex, _helperModules.Select(c => c.GetClassName()).ToArray());

            FkHelperModule helperModule = _helperModules[_modeIndex];

            if (GUILayout.Button("EXECUTE HELPER ACTION"))
            {
                helperModule.DoAction();
            }

            helperModule.RenderGUI();

            GUILayout.EndVertical();
        }
        
        private void OnLostFocus()
        {
            SaveModulesData();
        }

        private void SaveModulesData()
        {
            if(!HelperModulesInitialized()) return;
            
            foreach (FkHelperModule fkHelperModule in _helperModules)
            {
                PlayerPrefs.SetString(SaveKey + fkHelperModule.GetClassName(), JsonUtility.ToJson(fkHelperModule));
            }
        }

        private void LoadModulesData()
        {
            for (int i = 0; i < _helperModules.Count; i++)
            {
                if (!PlayerPrefs.HasKey(SaveKey + _helperModules[i].GetClassName())) return;

                _helperModules[i] = JsonUtility.FromJson(PlayerPrefs.GetString(SaveKey + _helperModules[i].GetClassName()),
                    _helperModules[i].GetType()) as FkHelperModule;
            }
        }
    }
}