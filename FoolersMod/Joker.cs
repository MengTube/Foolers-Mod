using BepInEx.IL2CPP;
using PeasAPI;
using PeasAPI.Components;
using PeasAPI.CustomEndReason;
using PeasAPI.Roles;
using System.Collections.Generic;
using UnityEngine;

namespace FoolersMod
{
    [RegisterCustomRole]
    class Joker : BaseRole
    {
        public Joker(BasePlugin plugin) : base(plugin) { }

        public override string Name => "Joker";

        public override string Description => "Complete your tasks before death to win";

        public override string TaskText => "Complete your tasks before death to win!";

        public override bool HasToDoTasks => true;

        public override Color Color => new Color(0f / 255f, 167f / 255f, 255f / 255f);

        public override int Limit => (int)FoolersModPlugin.JokerAmount.GetValue();

        public override Team Team => Team.Alone;

        public override Visibility Visibility => Visibility.NoOne;

        public override void OnGameStart()
        {
            
        }

        public override void OnUpdate()
        {
            if (PeasApi.GameStarted)
            {
                if (!PlayerControl.LocalPlayer.Data.IsDead)
                {
                    foreach (var member in Members)
                    {
                        if (PlayerTask.AllTasksCompleted(member.GetPlayer()))
                        {
                            new CustomEndReason(member.GetPlayer());
                        }
                    }
                }
            }
        }

        public override void OnMeetingUpdate(MeetingHud meeting)
        {

        }

        class AllTaskCompleted
        {
            public static void Prefix(PlayerControl __instance)
            {
                if (__instance.IsRole<Joker>())
                {
                    if (PlayerControl.LocalPlayer.Data.IsDead)
                    {
                        if (PlayerTask.AllTasksCompleted(__instance))
                        {
                            var winners = new List<byte>();
                            foreach (var player in PlayerControl.AllPlayerControls)
                            {
                                if (player.IsRole<Joker>())
                                    winners.Add(player.PlayerId);
                            }
                        }
                    }

                    new CustomEndReason(__instance);
                }
            }
        }
    }
}
