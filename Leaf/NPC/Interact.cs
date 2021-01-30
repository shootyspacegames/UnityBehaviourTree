using SSG.BehaviourTrees.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class MoveToInteractRange<T> : Leaf<T> where T: NpcContext, IMoveContext, IInteractContext
{
    public override NodeStatus OnBehave(T state)
    {
        if (!state.MoveTarget.HasValue)
            return NodeStatus.FAILURE;

        if(starting)
        {
            state.Me.NavMeshAgent.isStopped = false;
            state.Me.NavMeshAgent.destination = state.MoveTarget.Value;
            if (AtDestination(state))
                return NodeStatus.SUCCESS;
        }

        if(AtDestination(state)) 
        {
            return NodeStatus.SUCCESS;
        }

        return NodeStatus.RUNNING;
    }

    bool AtDestination(T context)
    {
        // If we're already really close, don't bother pathing
        if (Vector3.Distance(context.Me.transform.position, ((IMoveContext)context).MoveTarget.Value) < (context.Interactable.GetComponent<IInteractable>() ).Range)
        {
            return true;
        }
        return false;
    }

    public override void OnReset()
    {
    }
}

public class InteractWithTarget<T> : Leaf<T> where T : NpcContext, IInteractContext
{
    public override NodeStatus OnBehave(T state)
    {
        var interactable = state.Interactable.GetComponent<IInteractable>();
        if (interactable.IsBeingUsed)
            return NodeStatus.RUNNING;

        interactable.OnInteract(state.Me);
        return NodeStatus.SUCCESS;
    }

    public override void OnReset()
    {
    }
}

public class CleanInteractState<T> : Leaf<T> where T : NpcContext, IInteractContext, ICommandable
{
    public override NodeStatus OnBehave(T state)
    {
        state.Interactable = null;
        state.ClearCommandState();
        return NodeStatus.SUCCESS;
    }

    public override void OnReset()
    {
    }
}
