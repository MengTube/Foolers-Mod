using BepInEx;
using BepInEx.IL2CPP;
using Essentials.Options;
using HarmonyLib;
using PeasAPI;
using PeasAPI.Components;
using PeasAPI.Managers;
using Reactor;
using UnityEngine;

namespace FoolersMod
{
    [BepInPlugin(Id)]
    [BepInProcess("Among Us.exe")]
    [BepInDependency(ReactorPlugin.Id)]
    [BepInDependency(PeasApi.Id)]
    public class FoolersModPlugin : BasePlugin
    {
        public const string Id = "mengtube.amongus.foolersmod";

        public Harmony Harmony { get; } = new Harmony(Id);

        public static CustomOptionHeader FoolersOptions = CustomOption.AddHeader("<color=#2294E6>Foolers Mod Options:</color>");

        public static CustomOptionHeader Jester = CustomOption.AddHeader("<color=#ff00ff>Jester:</color>");
        public static CustomNumberOption JesterAmount = CustomOption.AddNumber("JesterAmount", "Jester Amount", true, 1, 0, 3, 1);

        public static CustomOptionHeader Trickster = CustomOption.AddHeader("<color=#d400ff>Trickster:</color>");
        public static CustomNumberOption TricksterAmount = CustomOption.AddNumber("TricksterAmount", "Trickster Amount", true, 1, 0, 3, 1);

        public static CustomOptionHeader Joker = CustomOption.AddHeader("<color=#00a6ff>Joker:</color>");
        public static CustomNumberOption JokerAmount = CustomOption.AddNumber("JokerAmount", "Joker Amount", true, 1, 0, 3, 1);

        public static CustomOptionHeader Troll = CustomOption.AddHeader("<color=#00ff00>Troll:</color>");
        public static CustomNumberOption TrollAmount = CustomOption.AddNumber("TrollAmount", "Troll Amount", true, 1, 0, 3, 1);
        public static CustomNumberOption ConfuseCooldown = CustomOption.AddNumber("ConfuseCooldown", "Confuse Cooldown", true, 50, 40, 60, 5);
        public static CustomNumberOption MeetingCooldown = CustomOption.AddNumber("MeetingCooldown", "Meeting Cooldown", true, 50, 40, 60, 5);
        public override void Load()
        {
            WatermarkManager.VersionText = "\n<color=#00FA9A>Foolers Mod</color> \n<color=#FFD11AFF>By MengTube</color> \n<color=#18d5b9>Button Art By C.A 100</color>";
            WatermarkManager.VersionTextOffset = new Vector3(0f, -0.65f, 0f);
            WatermarkManager.PingText = "\nMade By <color=#FFD11AFF>MengTube</color> \n<color=#2eadff>youtube.com/mengtube</color>";
            WatermarkManager.PingTextOffset = new Vector3(-1.4f, 0f, 0f);
            PeasApi.AccountTabOffset = new Vector3(0f, -0.9f, 0f);
            PeasApi.AccountTabOnlyChangesName = true;

            CustomServerManager.RegisterServer("matux.fr", "152.228.160.91", 22023);

            CustomOption.ShamelessPlug = false;
            CustomOption.HudTextScroller = false;

            RegisterCustomRoleAttribute.Register(this);

            Harmony.PatchAll();
        }
    }
}


