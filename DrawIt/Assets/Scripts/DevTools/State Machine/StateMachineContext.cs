using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.State_Machine
{
    public abstract class StateMachineContext : MonoBehaviour
    {
        public event UnityAction<State> OnStateChanged;

        public State GetPreviousState => _previousState;
        public State GetCurrentState => _currentState;

        [SerializeField] private bool debugStateChanges;
        
        private State _previousState;
        private State _currentState;
        
        public void SetState(State state)
        {
            if(debugStateChanges) Debug.Log(state != null ? $"State change request: {state.GetType()} | {state}" : "NULL STATE");
            
            if(_currentState == state) return;
        
            _previousState = _currentState;
            if (_previousState != null)
            {
                _previousState.OnFinish();
                DoOnStateFinished(_previousState);
            }
            
            _currentState = state;
            if (state == null)
            {
                OnStateNull();
                return;
            }
            _currentState.OnStart();
            
            DoOnStateChanged(_currentState);
            OnStateChanged?.Invoke(_currentState);
        }

        private void Update()
        {
            if(_currentState == null) return;
            
            _currentState.OnTick();
        }

        protected virtual void OnStateNull()
        {
            Debug.LogError("Tried Setting State to NULL!");
        }

        protected virtual void DoOnStateChanged(State changedToState)
        {
            
        }
        
        protected virtual void DoOnStateFinished(State finishedState)
        {
            
        }
    }
}