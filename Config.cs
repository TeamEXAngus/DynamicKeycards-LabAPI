namespace DynamicKeycards
{
    public class Config
    {
        public Dictionary<string, Dictionary<string, string>> Keycards { get; set; } = new Dictionary<string, Dictionary<string, string>>()
        {
            { "mtf_guard", new Dictionary<string, string>()
                {
                    { "type", "mtf" },
                    { "item_name", "MTF Guard Keycard" },
                    { "containment", "2" },
                    { "armory", "2" },
                    { "admin", "2" },
                    { "perms_color", "#222222" },
                    { "primary_color", "#cccccc" },
                    { "owner_name", "Pvt. {name}" },
                    { "rank_detail", "1" }
                }
            },

            { "guard_captain", new Dictionary<string, string>()
                {
                    { "type", "guard" },
                    { "item_name", "Guard Captain Keycard" },
                    { "containment", "1" },
                    { "armory", "2" },
                    { "admin", "1" },
                    { "perms_color", "#222222" },
                    { "primary_color", "#999999" },
                    { "label", "GUARD CAPTAIN" },
                    { "text_color", "#FFFFFF" },
                    { "owner_name", "Cpt. {name}" },
                    { "wear_level", "3" }
                }
            },

            { "checkpoint_access", new Dictionary<string, string>()
                {
                    { "type", "management" },
                    { "item_name", "Checkpoint Access Keycard" },
                    { "containment", "0" },
                    { "armory", "0" },
                    { "admin", "1" },
                    { "perms_color", "#000000" },
                    { "primary_color", "#99bbff" },
                    { "label", "CHECKPOINT ACCESS" },
                    { "text_color", "#000000" },
                }
            },

            { "credit_card", new Dictionary<string, string>()
                {
                    { "type", "standard" },
                    { "item_name", "Credit Card" },
                    { "containment", "0" },
                    { "armory", "0" },
                    { "admin", "0" },
                    { "perms_color", "#000000" },
                    { "primary_color", "#2222FF" },
                    { "label", "Credit Card" },
                    { "text_color", "#DDDD00" },
                    { "owner_name", "{digit}{digit}{digit}{digit}-{digit}{digit}{digit}{digit}-{digit}{digit}{digit}{digit}-{digit}{digit}{digit}{digit}" },
                    { "wear_level", "0" }
                }
            }
        };
    }
}