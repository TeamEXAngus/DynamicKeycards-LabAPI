using LabApi.Features.Wrappers;
using Interactables.Interobjects.DoorUtils;
using UnityEngine;
using Logger = LabApi.Features.Console.Logger;
using Random = UnityEngine.Random;

namespace DynamicKeycards
{
    public abstract class Keycard
    {
        private static Dictionary<ushort, Keycard> _trackedSerials = new();

        public virtual string ID { get; }
        public virtual string ItemName { get; }
        public virtual KeycardLevels Access { get; }
        public virtual Color PrimaryColor { get; }
        public virtual Color PermsColor { get; }

        protected Keycard(string id, string itemName, int containment, int armory, int admin, Color primaryColor, Color permsColor)
        {
            ID = id;
            ItemName = itemName;
            Access = new KeycardLevels(containment, armory, admin);
            PrimaryColor = primaryColor;
            PermsColor = permsColor;
        }

        public abstract Item Give(Player target, string owner);

        public Pickup Spawn(Vector3 position, string owner)
        {
            var host = Player.Host;

            var item = Give(host, owner);

            var pickup = host.DropItem(item);

            pickup.Position = position;

            return pickup;
        }

        public static bool ItemIsDynamicKeycard(Item i, out Keycard k)
        {
            bool result = _trackedSerials.TryGetValue(i.Serial, out k);
            return result;
        }

        public static bool PickupIsDynamicKeycard(Pickup p, out Keycard k)
        {
            bool result = _trackedSerials.TryGetValue(p.Serial, out k);
            return result;
        }

        internal static string RandomSerialLabel()
        {
            return Random.Range(0, 999_999_999_999).ToString();
        }

        internal static Keycard Parse(string id, Dictionary<string, string> properties)
        {
            if (!properties.TryGetValue("type", out string type))
            {
                Logger.Error($"Error when loading keycard with id {id}: missing \"type\"");
                return null;
            }

            switch (type)
            {
                case "mtf":
                    return MTFKeycard.Parse(id, properties);

                case "guard":
                    return GuardKeycard.Parse(id, properties);

                case "management":
                    return ManagementKeycard.Parse(id, properties);

                case "standard":
                    return StandardKeycard.Parse(id, properties);

                default:
                    Logger.Error($"Error when loading keycard with id {id}: invalid type \"{type}\". Valid options are \"mtf\" or \"guard\" or \"management\" or \"standard\"");
                    return null;
            }
        }

        internal string FormatOwnerName(string format, string owner)
        {
            string result = format;

            while (result.Contains("{digit}"))
            {
                result = ReplaceFirst(result, "{digit}", Random.Range(0, 10).ToString());
            }

            result = result.Replace("{name}", owner);

            return result;
        }

        //Copied from http://stackoverflow.com/questions/141045/how-do-i-replace-the-first-instance-of-a-string-in-net/141076#141076
        private static string ReplaceFirst(string text, string search, string replace)
        {
            int pos = text.IndexOf(search);
            if (pos < 0)
            {
                return text;
            }
            return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
        }

        protected static void RegisterSerial(ushort serial, Keycard card)
        {
            if (_trackedSerials.ContainsKey(serial))
            {
                Logger.Error($"Tried to register serial {serial} but that serial has already been registered!");
                return;
            }

            _trackedSerials.Add(serial, card);
        }

        internal static void WaitingForPlayers()
        {
            _trackedSerials.Clear();
        }
    }
}