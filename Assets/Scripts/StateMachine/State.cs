using UnityEngine;

public abstract class State<T> : MonoBehaviour
{
    
    public abstract T Id { get; }

    public virtual void Enter()
    {
    }

    public virtual void Leave()
    {
    }

    public virtual void Init()
    {
    }

    public virtual void StartApply()
    {
    }

    public virtual void EndApply()
    {
    }

    public abstract void Apply(float amount);
}