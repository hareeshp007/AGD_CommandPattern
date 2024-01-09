using Command.Actions;
using Command.Main;

public class CleanseAction : UnitCommand
{
    private bool willHitTarget;
    public void AttackCommand(CommandData commandData)
    {
        this.commandData = commandData;
        willHitTarget = WillHitTarget();
    }

    public override bool WillHitTarget() => true;

    public override void Execute() => GameService.Instance.ActionService.GetActionByType(CommandType.Cleanse).PerformAction(actorUnit, targetUnit, willHitTarget);

}
