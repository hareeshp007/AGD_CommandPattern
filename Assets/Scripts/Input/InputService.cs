using Command.Main;
using Command.Player;
using Command.Actions;
using Unity.IO.LowLevel.Unsafe;

namespace Command.Input
{
    public class InputService
    {
        private MouseInputHandler mouseInputHandler;

        private InputState currentState;
        private CommandType selectedCommandType;
        private TargetType targetType;
        private bool isSuccessful;

        public InputService()
        {
            mouseInputHandler = new MouseInputHandler(this);
            SetInputState(InputState.INACTIVE);
            SubscribeToEvents();
        }

        public void SetInputState(InputState inputStateToSet) => currentState = inputStateToSet;

        private void SubscribeToEvents() => GameService.Instance.EventService.OnActionSelected.AddListener(OnActionSelected);

        public void UpdateInputService()
        {
            if(currentState == InputState.SELECTING_TARGET)
                mouseInputHandler.HandleTargetSelection(targetType);
        }

        public void OnActionSelected(CommandType selectedCommandType)
        {
            this.selectedCommandType = selectedCommandType;
            SetInputState(InputState.SELECTING_TARGET);
            TargetType targetType = SetTargetType(selectedCommandType);
            ShowTargetSelectionUI(targetType);
        }

        private void ShowTargetSelectionUI(TargetType selectedTargetType)
        {
            int playerID = GameService.Instance.PlayerService.ActivePlayerID;
            GameService.Instance.UIService.ShowTargetOverlay(playerID, selectedTargetType);
        }

        private TargetType SetTargetType(CommandType selectedCommandType) => targetType = GameService.Instance.ActionService.GetTargetTypeForAction(selectedCommandType);

        public void OnTargetSelected(UnitController targetUnit)
        {
            SetInputState(InputState.EXECUTING_INPUT);
            UnitCommand commandToProcess = CreateUnitCommand(targetUnit);
            GameService.Instance.ProcessUnitCommand(commandToProcess);
            GameService.Instance.PlayerService.PerformAction(selectedCommandType, targetUnit,isSuccessful);
        }
        private CommandData CreateCommandData(UnitController targetUnit)
        {
            return new CommandData(
                GameService.Instance.PlayerService.ActiveUnitID,
                targetUnit.UnitID,
                GameService.Instance.PlayerService.ActivePlayerID,
                targetUnit.Owner.PlayerID
            );
        }
        private UnitCommand CreateUnitCommand(UnitController targetUnit)
        {
            CommandData commandData = CreateCommandData(targetUnit);
            switch (selectedCommandType)
            {
                case CommandType.Attack:
                    return new AttackAction(commandData);
                case CommandType.Heal:
                    return new HealAction(commandData);
                case CommandType.AttackStance:
                    return new AttackStanceAction(commandData);
                case CommandType.Cleanse:
                    return new CleanseAction(commandData);
                case CommandType.BerserkAttack:
                    return new BerserkAttackAction(commandData);
                case CommandType.Meditate:
                    return new MeditateAction(commandData);
                case CommandType.ThirdEye:
                    return new ThirdEyeAction(commandData);
                default:
                    // If the selectedCommandType is not recognized, throw an exception.
                    throw new System.Exception($"No Command found of type: {selectedCommandType}");
            }
        }
    }
}