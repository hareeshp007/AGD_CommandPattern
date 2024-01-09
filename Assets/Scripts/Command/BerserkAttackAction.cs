using Command.Actions;
using Command.Main;

public class BerserkAttackAction : UnitCommand
{
    private bool willHitTarget;
    public BerserkAttackAction(CommandData commandData)
    {
        this.commandData = commandData;
        willHitTarget = WillHitTarget();
    }

    public override bool WillHitTarget() => true;

    public override void Execute() => GameService.Instance.ActionService.GetActionByType(CommandType.BerserkAttack).PerformAction(actorUnit, targetUnit, willHitTarget);

}
