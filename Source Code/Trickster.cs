using BepInEx.IL2CPP;
using HarmonyLib;
using PeasAPI;
using PeasAPI.Components;
using PeasAPI.CustomEndReason;
using PeasAPI.Roles;
using System.Collections.Generic;
using UnityEngine;

namespace FoolersMod
{
    [RegisterCustomRole]
    class Trickster : BaseRole
    {
        public Trickster(BasePlugin plugin) : base(plugin) { }

        public override string Name => "Trickster";

        public override string Description => "Get killed to win";

        public override string TaskText => "Get killed to win!";

        public override bool HasToDoTasks => false;

        public override bool CanVent => false;

        public override bool CanKill(PlayerControl victim = null) => false;

        public override bool CanSabotage(SystemTypes? sabotage) => false;

        public override Color Color => new Color(211f / 255f, 0f / 255f, 255f / 255f);

        public override int Limit => (int)FoolersModPlugin.TricksterAmount.GetValue();

        public override Team Team => Team.Alone;

        public override Visibility Visibility => Visibility.NoOne;

        public override void OnGameStart()
        {

        }

        public override void OnUpdate()
        {

        }

        public override void OnMeetingUpdate(MeetingHud meeting)
        {

        }

        [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.Die))]
        class IsDeadPatch
        {
            public static void Prefix(PlayerControl __instance)
            {
                if (__instance.IsRole<Trickster>())
                {
                    if (!PlayerControl.LocalPlayer.Data.IsDead != PlayerControl.LocalPlayer) return;
                    {
                        var winners = new List<byte>();
                        foreach (var player in PlayerControl.AllPlayerControls)
                        {
                            if (player.IsRole<Trickster>())
                                winners.Add(player.PlayerId);
                        }

                        new CustomEndReason(__instance);
                    }
                }
            }
        }
    }
}

