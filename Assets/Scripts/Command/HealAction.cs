using Command.Actions;
using Command.Main;

public class HealAction : UnitCommand
{
    private bool willHitTarget;
    public HealAction(CommandData commandData)
    {
        this.commandData = commandData;
        willHitTarget = WillHitTarget();
    }

    public override bool WillHitTarget() => true;

    public override void Execute() => GameService.Instance.ActionService.GetActionByType(CommandType.Heal).PerformAction(actorUnit, targetUnit, willHitTarget);

}
