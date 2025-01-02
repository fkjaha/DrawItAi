using System;

namespace FKHelper
{
    [Serializable]
    public abstract class FkHelperModule
    {
        public abstract string GetClassName();
    
        public abstract void RenderGUI();

        public abstract void DoAction();
    }
}