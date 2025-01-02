namespace _Scripts.State_Machine
{
    public abstract class State<TContext> : State
    { 
        protected readonly TContext Context;

        public State(TContext context)
        {
            Context = context;
        }
    }
    
    public abstract class State
    { 
        public virtual void OnTick()
        {
        }

        public virtual void OnStart()
        {
        }

        public virtual void OnFinish()
        {
        }
    }
}