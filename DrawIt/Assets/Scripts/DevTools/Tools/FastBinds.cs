using System.Collections.Generic;
using UnityEngine;

public class FastBinds : MonoBehaviour
{
    [SerializeField] private List<FastBind> binds;

    private void Update()
    {
        foreach (FastBind fastBind in binds)
        {
            if (fastBind.IsAnyKey && Input.anyKeyDown)
            {
                fastBind.ExecuteEvent();
                continue;
            }

            if (fastBind.IsUsingCombination)
            {
                bool bindDetected = false;
                for (int i = 0; i < fastBind.GetCombinationKeyCodes.Length; i++)
                {
                    KeyCode combinationKeyCode = fastBind.GetCombinationKeyCodes[i];
                    
                    if (Input.GetKey(combinationKeyCode) && i != (fastBind.GetCombinationKeyCodes.Length - 1))
                    {
                        continue;
                    }

                    if(Input.GetKeyDown(combinationKeyCode) && i == (fastBind.GetCombinationKeyCodes.Length - 1))
                    {
                        bindDetected = true;
                    }
                    
                    break;
                }

                if(bindDetected) fastBind.ExecuteEvent();
                
                continue;
            }
            
            if (Input.GetKeyDown(fastBind.GetKeyCode))
            {
                fastBind.ExecuteEvent();
            }
        }
    }
}