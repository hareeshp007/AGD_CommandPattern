

using Command.Actions;
using Command.Main;

public class AttackAction : UnitCommand
{
    private bool willHitTarget;

    public AttackAction(CommandData commandData)
    {
        this.commandData = commandData;
        willHitTarget = WillHitTarget();
    }

    public override bool WillHitTarget() => true;

    public override void Execute() => GameService.Instance.ActionService.GetActionByType(CommandType.Attack).PerformAction(actorUnit, targetUnit, willHitTarget);
}
