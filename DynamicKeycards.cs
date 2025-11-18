using InventorySystem.Items;
using LabApi.Events.Handlers;
using LabApi.Features;
using LabApi.Loader.Features.Plugins;
using LabApi.Loader.Features.Plugins.Enums;

namespace DynamicKeycards
{
    public class DynamicKeycards : Plugin<Config>
    {
        public override string Name { get; } = "Dynamic Keycards";

        public override string Description { get; } = "Allows for creation and usage of custom keycards.";

        public override string Author { get; } = "TeamEXAngus";

        public override Version Version { get; } = new Version(1, 0, 0);

        public override Version RequiredApiVersion { get; } = new Version(LabApiProperties.CompiledVersion);

        public override LoadPriority Priority => LoadPriority.High;

        public static DynamicKeycards Instance { get; private set; }

        private Dictionary<string, Keycard> Keycards;

        public override void Enable()
        {
            Instance = this;

            LoadKeycards();

            ServerEvents.WaitingForPlayers += Keycard.WaitingForPlayers;
        }

        public override void Disable()
        {
            ServerEvents.WaitingForPlayers -= Keycard.WaitingForPlayers;

            Instance = null;
        }

        private void LoadKeycards()
        {
            Keycards = new Dictionary<string, Keycard>();

            foreach (var kvp in Config.Keycards)
            {
                Keycard card = Keycard.Parse(kvp.Key, kvp.Value);

                if (!(card is null)) Keycards.Add(kvp.Key, card);
            }
        }

        public Keycard GetKeycard(string id)
        {
            if (Keycards.ContainsKey(id))
            {
                return Keycards[id];
            }

            return null;
        }
    }
}