using UnityEngine;

public class InteractionMachine : MonoBehaviour
{
    private readonly StateMachine<InteractionStates> _interactionMachine = new StateMachine<InteractionStates>();
    private float _time = 0;

    private void Start()
    {
        InitializeStates();
    }

    private void InitializeStates()
    {
        var allStates = GetComponents<State<InteractionStates>>();
        foreach (var stateComponent in allStates)
        {
            _interactionMachine.AddState(stateComponent.Id, stateComponent);
        }
    }

    public void SetState(InteractionStates stateId)
    {
        Debug.Log("changing to" + stateId);
        _interactionMachine.SetState(stateId);
    }

    public void StartApply()
    {
        _interactionMachine.StartApply();
    }

    public void EndApply()
    {
        _interactionMachine.EndApply();
    }

    public void Apply(float amount)
    {
        _interactionMachine.Apply(amount);
    }
}