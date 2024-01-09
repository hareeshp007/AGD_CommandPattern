using Command.Main;

namespace Command.Commands
{
    public class HealCommand : UnitCommand
    {
        private bool willHitTarget;
        private int previousHealth;

        public HealCommand(CommandData commandData)
        {
            this.commandData = commandData;
            willHitTarget = WillHitTarget();
        }
        public override void Undo()
        {
            if (willHitTarget)
            {
                targetUnit.RestoreHealth(previousHealth-targetUnit.CurrentHealth);
                actorUnit.Owner.ResetCurrentActiveUnit();
            }
        }
        public override void Execute()
        {
            previousHealth = targetUnit.CurrentHealth;
            GameService.Instance.ActionService.GetActionByType(CommandType.Heal).PerformAction(actorUnit, targetUnit, willHitTarget);
        }
        public override bool WillHitTarget() => true;
    }
}