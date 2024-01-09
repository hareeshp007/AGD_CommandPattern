
using Command.Player;
using UnityEngine;

public abstract class UnitCommand : ICommand
{
    public int ActorUnitID;
    public int TargetUnitID;
    public int ActorPlayerID;
    public int TargetPlayerID;

    protected UnitController actorUnit;
    protected UnitController targetUnit;
    public void Execute()
    {
        throw new System.NotImplementedException();
    }

    public abstract bool WillHitTarget();
}
