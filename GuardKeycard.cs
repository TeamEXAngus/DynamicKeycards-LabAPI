using LabApi.Features.Wrappers;
using UnityEngine;
using Logger = LabApi.Features.Console.Logger;

namespace DynamicKeycards
{
    internal class GuardKeycard : Keycard
    {
        public string Label { get; }
        public Color TextColor { get; }
        public string OwnerNameFormat { get; }
        public byte WearLevel { get; }

        public GuardKeycard(string id, string itemName, int containment, int armory, int admin, Color primaryColor, Color permsColor, string label, Color textColor, string ownerNameFormat, byte wearLevel)
            : base(id, itemName, containment, armory, admin, primaryColor, permsColor)
        {
            Label = label;
            TextColor = textColor;
            OwnerNameFormat = ownerNameFormat;
            WearLevel = wearLevel;
        }

        public override Item Give(Player target, string owner)
        {
            var card = KeycardItem.CreateCustomKeycardMetal(
                target,
                ItemName,
                FormatOwnerName(OwnerNameFormat, owner),
                Label,
                Access,
                PrimaryColor,
                PermsColor,
                TextColor,
                WearLevel,
                RandomSerialLabel()
            );

            RegisterSerial(card.Serial, this);

            return card;
        }

        internal new static Keycard Parse(string id, Dictionary<string, string> properties)
        {
            bool allPropertiesExist = true;

            if (!properties.TryGetValue("item_name", out string itemName))
            {
                Logger.Error($"Error when loading keycard with id {id}: missing \"item_name\"");
                allPropertiesExist = false;
            }

            if (!properties.TryGetValue("armory", out string strArmory))
            {
                Logger.Error($"Error when loading keycard with id {id}: missing \"armory\"");
                allPropertiesExist = false;
            }

            if (!properties.TryGetValue("containment", out string strContainment))
            {
                Logger.Error($"Error when loading keycard with id {id}: missing \"containment\"");
                allPropertiesExist = false;
            }

            if (!properties.TryGetValue("admin", out string strAdmin))
            {
                Logger.Error($"Error when loading keycard with id {id}: missing \"admin\"");
                allPropertiesExist = false;
            }

            if (!properties.TryGetValue("primary_color", out string strPrimaryColor))
            {
                Logger.Error($"Error when loading keycard with id {id}: missing \"primary_color\"");
                allPropertiesExist = false;
            }

            if (!properties.TryGetValue("perms_color", out string strPermsColor))
            {
                Logger.Error($"Error when loading keycard with id {id}: missing \"perms_color\"");
                allPropertiesExist = false;
            }

            if (!properties.TryGetValue("owner_name", out string ownerNameFormat))
            {
                Logger.Error($"Error when loading keycard with id {id}: missing \"owner_name\"");
                allPropertiesExist = false;
            }

            if (!properties.TryGetValue("label", out string label))
            {
                Logger.Error($"Error when loading keycard with id {id}: missing \"label\"");
                allPropertiesExist = false;
            }

            if (!properties.TryGetValue("text_color", out string strTextColor))
            {
                Logger.Error($"Error when loading keycard with id {id}: missing \"text_color\"");
                allPropertiesExist = false;
            }

            if (!properties.TryGetValue("wear_level", out string strWearLevel))
            {
                Logger.Error($"Error when loading keycard with id {id}: missing \"wear_level\"");
                allPropertiesExist = false;
            }

            if (!allPropertiesExist) return null;

            bool allPropertiesValid = true;

            if (!int.TryParse(strContainment, out int containment) || containment < 0 || containment > 3)
            {
                Logger.Error($"Error when loading keycard with id {id}: \"containment\" must be an integer between 0-3 inclusive");
                allPropertiesValid = false;
            }

            if (!int.TryParse(strArmory, out int armory) || armory < 0 || armory > 3)
            {
                Logger.Error($"Error when loading keycard with id {id}: \"armory\" must be an integer between 0-3 inclusive");
                allPropertiesValid = false;
            }

            if (!int.TryParse(strAdmin, out int admin) || admin < 0 || admin > 3)
            {
                Logger.Error($"Error when loading keycard with id {id}: \"admin\" must be an integer between 0-3 inclusive");
                allPropertiesValid = false;
            }

            if (!ColorUtility.TryParseHtmlString(strPrimaryColor, out Color primaryColor))
            {
                Logger.Error($"Error when loading keycard with id {id}: \"primary_color\" must be a valid hexadecimal color code");
                allPropertiesValid = false;
            }

            if (!ColorUtility.TryParseHtmlString(strPermsColor, out Color permsColor))
            {
                Logger.Error($"Error when loading keycard with id {id}: \"perms_color\" must be a valid hexadecimal color code");
                allPropertiesValid = false;
            }

            if (!ColorUtility.TryParseHtmlString(strTextColor, out Color textColor))
            {
                Logger.Error($"Error when loading keycard with id {id}: \"text_color\" must be a valid hexadecimal color code");
                allPropertiesValid = false;
            }

            if (!byte.TryParse(strWearLevel, out byte wearLevel) || wearLevel < 0 || wearLevel > 3)
            {
                Logger.Error($"Error when loading keycard with id {id}: \"wear_level\" must be an integer between 0-3 inclusive");
                allPropertiesValid = false;
            }

            if (!allPropertiesValid) return null;

            return new GuardKeycard(id, itemName, containment, armory, admin, primaryColor, permsColor, label, textColor, ownerNameFormat, wearLevel);
        }
    }
}