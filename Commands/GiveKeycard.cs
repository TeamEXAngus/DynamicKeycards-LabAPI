using System;
using System.Linq;
using CommandSystem;
using LabApi.Features.Wrappers;

namespace DynamicKeycards.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(ClientCommandHandler))]
    internal class GiveKeycard : ICommand
    {
        public string Command { get; } = "givekeycard";

        public string[] Aliases { get; } = new string[] { "gkeycard", "gck" };

        public string Description { get; } = "Dynamic SCP: gives a custom keycard to a player";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission(PlayerPermissions.GivingItems))
            {
                response = "You do not have permission to use this command!";
                return false;
            }

            if (arguments.Count != 2)
            {
                response = "Usage: givekeycard <player> <keycard name>";
                return false;
            }

            Player player;

            if (int.TryParse(arguments.ElementAt(0), out int id))
            {
                player = Player.Get(id);
            }
            else
            {
                player = Player.GetByDisplayName(arguments.ElementAt(0), true);
            }

            if (player is null)
            {
                response = "Could not find player matching " + arguments.ElementAt(0);
                return false;
            }

            Keycard card = DynamicKeycards.Instance.GetKeycard(arguments.ElementAt(1));

            if (card is null)
            {
                response = "Could not find keycard with id " + arguments.ElementAt(1);
                return false;
            }

            var item = card.Give(player, player.DisplayName);

            if (item is null)
            {
                response = $"Failed to give {card.ItemName} to {player.DisplayName}";
                return false;
            }

            response = $"Gave {card.ItemName} to {player.DisplayName}";
            return true;
        }
    }
}