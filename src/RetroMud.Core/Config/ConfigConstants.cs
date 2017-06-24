using System;
using System.Configuration;

namespace RetroMud.Core.Config
{
    public class ConfigConstants
    {
        public static readonly string InstanceName = "Instance:Name";

        public static readonly string MapMoveUpKey = "Map:MoveUpKey";
        public static readonly string MapMoveDownKey = "Map:MoveDownKey";
        public static readonly string MapMoveLeftKey = "Map:MoveLeftKey";
        public static readonly string MapMoveRightKey = "Map:MoveRightKey";
        public static readonly string MapInventoryKey = "Map:InventoryKey";
        public static readonly string SavedStatePath = "SavedStatePath";

        public static int MaxGameFrameRate = Convert.ToInt32(ConfigurationManager.AppSettings["Tick:MaxGameFrameRate"]);
        public static string MapMetaBoundary = "<!!>";

        public static bool DebugEnabled = Convert.ToBoolean(ConfigurationManager.AppSettings["Debug"]);
    }
}
