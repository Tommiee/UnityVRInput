using System.Collections.Generic;

public class StateMachine<TStateId>
{
    private readonly Dictionary<TStateId, State<TStateId>> _states = new Dictionary<TStateId, State<TStateId>>();

    private State<TStateId> _currentState;

    /// <summary>
    /// Update the current state with a specific amount
    /// </summary>
    /// <param name="amount"></param>
    public void Apply(float amount)
    {
        _currentState?.Apply(amount);
    }

    /// <summary>
    /// Start applying new values to the current state
    /// </summary>
    public void StartApply()
    {
        _currentState?.StartApply();
    }

    /// <summary>
    /// End applying values to the current state
    /// </summary>
    public void EndApply()
    {
        _currentState?.EndApply();
    }

    /// <summary>
    /// Method om de state te wijzigen
    /// </summary>
    public void SetState(TStateId stateId)
    {
        if (!_states.ContainsKey(stateId))
            return;

        if (_currentState != null) {
            _currentState.Leave();
            _currentState.enabled = false;
        }

        _currentState = _states[stateId];

        _currentState.Enter();

        _currentState.enabled = true;
    }

    /// <summary>
    /// Voeg een state toe aan de state machine
    /// </summary>
    /// <param name="stateId">Een integer die komt uit de ENUM StateID</param>
    /// <param name="state">Een component die State.cs extend (inheritance)</param>
    public void AddState(TStateId stateId, State<TStateId> state)
    {
        _states.Add(stateId, state);
        state.enabled = false;
        state.Init();
    }
}