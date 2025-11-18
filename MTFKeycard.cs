using LabApi.Features.Wrappers;
using System;
using UnityEngine;
using Logger = LabApi.Features.Console.Logger;

namespace DynamicKeycards
{
    internal class MTFKeycard : Keycard
    {
        public string OwnerNameFormat { get; }
        public int RankDetail { get; }

        public MTFKeycard(string id, string itemName, int containment, int armory, int admin, Color primaryColor, Color permsColor, string ownerNameFormat, int rankDetail)
            : base(id, itemName, containment, armory, admin, primaryColor, permsColor)
        {
            OwnerNameFormat = ownerNameFormat;
            RankDetail = rankDetail;
        }

        public override Item Give(Player target, string owner)
        {
            var card = KeycardItem.CreateCustomKeycardTaskForce(
                target,
                ItemName,
                FormatOwnerName(OwnerNameFormat, owner),
                Access,
                PrimaryColor,
                PermsColor,
                RandomSerialLabel(),
                RankDetail
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

            if (!properties.TryGetValue("rank_detail", out string strRankDetail))
            {
                Logger.Error($"Error when loading keycard with id {id}: missing \"rank_detail\"");
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

            if (!byte.TryParse(strRankDetail, out byte rankDetail) || rankDetail < 0 || rankDetail > 3)
            {
                Logger.Error($"Error when loading keycard with id {id}: \"rank_detail\" must be an integer between 0-3 inclusive");
                allPropertiesValid = false;
            }

            if (!allPropertiesValid) return null;

            return new MTFKeycard(id, itemName, containment, armory, admin, primaryColor, permsColor, ownerNameFormat, rankDetail);
        }
    }
}