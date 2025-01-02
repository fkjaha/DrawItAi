using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace MoreEditorShortcuts
{
    public class InputWindow : EditorWindow
    {
        public event UnityAction<string> OnSubmit;
        public event UnityAction<List<string>> OnSubmitInputs;

        private int _inputsCount = 1;
        private string _input;
        private List<string> _inputs;
        private List<string> _inputsNames;
        
        private void OnGUI()
        {
            if (_inputs == null || _inputs.Count != _inputsCount)
            {
                _inputs = new string[_inputsCount].ToList();
            }
            
            for (int i = 0; i < _inputsCount; i++)
            {
                string inputName = $"Enter Input {i}:";
                if (_inputsNames != null && _inputsNames.Count > i)
                {
                    if (_inputsNames[i] != null) inputName = _inputsNames[i];
                }
                _inputs[i] = EditorGUILayout.TextField(inputName, _inputs[i]);
            }

            if (GUILayout.Button("Submit") || Input.GetKeyDown(KeyCode.Return))
            {
                Submit();
            }
        }

        public void SetExpectedInputsCount(int count)
        {
            _inputsCount = count;
        }
        
        public void SetInputsNames(List<string> names)
        {
            _inputsNames = names;
        }

        private void Submit()
        {
            OnSubmit?.Invoke(_inputs[0]);
            OnSubmitInputs?.Invoke(_inputs);
        }
        
        public static void OpenWithParameters(int paramsCount, List<string> paramsNames = null, Action<string> onSubmitString = null,
            Action<List<string>> onSubmitList = null, bool closeOnSubmit = true)
        {
            InputWindow inputWindow = (InputWindow) GetWindow(typeof(InputWindow));
            
            inputWindow.SetExpectedInputsCount(paramsCount);
            if (paramsNames != null) inputWindow.SetInputsNames(paramsNames);
            
            FocusWindowIfItsOpen<InputWindow>();
            
            if (onSubmitList != null) inputWindow.OnSubmitInputs += onSubmitList.Invoke;
            if (onSubmitString != null) inputWindow.OnSubmit += onSubmitString.Invoke;
            if (closeOnSubmit) inputWindow.OnSubmit += _ => inputWindow.Close();
        }
    }
}