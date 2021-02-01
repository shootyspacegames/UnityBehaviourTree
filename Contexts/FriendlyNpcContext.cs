using UnityEngine;

public class FriendlyNpcContext : NpcContext, IMoveContext, IHasEnemyContext, IHasResourceTarget, ILook, ICommandable, IInteractContext
{
    public GameObject Enemy { get; set; }
    public ResourceNode ResourceNode { get; set; }
    public Vector3? MoveTarget { get; set; }
    public GameObject LookTarget { get; set; }
    public bool AwaitingCommand { get; set; }
    public NpcCommands RecievedCommand { get; set; }
    public Vector3? MoveCommandTarget { get; set; }
    public bool IsRetreating { get; set; }
    public bool UpdatedCommand { get; set; }
    public GameObject Interactable { get; set; }
    public GameObject CommandTarget { get; set; }
    public Vector3? CommandHitPoint { get; set; }

    public void ClearCommandState()
    {
        this.RecievedCommand = NpcCommands.IDLE;
        this.AwaitingCommand = true;
    }
}