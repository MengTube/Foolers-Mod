using BepInEx.IL2CPP;
using Hazel;
using PeasAPI;
using PeasAPI.Components;
using PeasAPI.Roles;
using PeasAPI.CustomButtons;
using System.Collections.Generic;
using Reactor;
using Reactor.Networking;
using UnityEngine;

namespace FoolersMod
{
    [RegisterCustomRole]
    class Troll : BaseRole
    {
        public Troll(BasePlugin plugin) : base(plugin) { }

        public override string Name => "Troll";

        public override string Description => "Troll everyone and ruin their day";

        public override string TaskText => "Troll everyone and ruin their day!";

        public override bool HasToDoTasks => false;

        public override bool CanVent => false;

        public override bool CanKill(PlayerControl victim = null) => false;

        public override bool CanSabotage(SystemTypes? sabotage) => false;

        public override Color Color => new Color(0f / 255f, 255f / 255f, 0f / 255f);

        public override int Limit => (int)FoolersModPlugin.TrollAmount.GetValue();

        public override Team Team => Team.Alone;

        public override Visibility Visibility => Visibility.NoOne;

        public RoleButton ConfuseButton;

        public RoleButton MeetingButton;

        public override void OnGameStart()
        {
            ConfuseButton = new RoleButton(() =>
            {
                foreach (var allPlayers in PlayerControl.AllPlayerControls)
                {
                    string[] playerName = new string[] { "i said i", "lol", "yo mama", "2thic2vent", "dababy", "yeah boiii", "demon", "Freddy", "Lanky", "carrot", "jesse", "haha i", "moon", "Pennywise", "Ghost420", "fuzzypear", "ur mom", "Shadow", "king", "SlimButFat" };
                    string randomName = playerName[Random.Range(0, playerName.Length)];

                    allPlayers.SetName(randomName, true);
                    allPlayers.SetColor(Random.Range(0, 18));
                    allPlayers.SetHat((uint)Random.Range(1, 115), allPlayers.Data.ColorId);
                    allPlayers.SetSkin((uint)Random.Range(1, 19));
                    allPlayers.SetPet((uint)Random.Range(1, 12));

                    Rpc<TrollConfuseAbilityRpc>.Instance.Send(new TrollConfuseAbilityRpc.Data(PlayerControl.LocalPlayer));
                }
            }, FoolersModPlugin.ConfuseCooldown.GetValue(),
            Utility.CreateSprite("FoolersMod.Resources.Confuse.png"), new Vector2(0.02f, 1.25f), true, this, true, "<size=90%>CONFUSE", new Vector2(-0.03f, -0.75f));

            MeetingButton = new RoleButton(() =>
            {
                var notTroll = new List<PlayerControl>();

                foreach (var player in PlayerControl.AllPlayerControls)
                {
                    if (!player.IsRole<Troll>())
                        notTroll.Add(player);
                }

                notTroll[Random.Range(0, notTroll.Count)].CmdReportDeadBody(null);
            }, FoolersModPlugin.MeetingCooldown.GetValue(),
            Utility.CreateSprite("FoolersMod.Resources.Meeting.png"), Vector2.zero, true, this, true, "<size=90%>MEETING", new Vector2(0f, -0.6f));
        }

        public override void OnUpdate()
        {

        }

        public override void OnMeetingUpdate(MeetingHud meeting)
        {

        }
    }

    [RegisterCustomRpc((uint)CustomRPC.TrollConfuseAbility)]
    public class TrollConfuseAbilityRpc : PlayerCustomRpc<FoolersModPlugin, TrollConfuseAbilityRpc.Data>
    {
        public TrollConfuseAbilityRpc(FoolersModPlugin plugin, uint id) : base(plugin, id)
        {
        }

        public readonly struct Data
        {
            public readonly PlayerControl Player;

            public Data(PlayerControl player)
            {
                Player = player;
            }
        }

        public override RpcLocalHandling LocalHandling => RpcLocalHandling.None;

        public override void Write(MessageWriter writer, Data data)
        {
            writer.Write(data.Player.PlayerId);
        }

        public override Data Read(MessageReader reader)
        {
            return new Data(reader.ReadByte().GetPlayer());
        }

        public override void Handle(PlayerControl innerNetObject, Data data)
        {
            foreach (var allPlayers in PlayerControl.AllPlayerControls)
            {
                string[] playerName = new string[] { "i said i", "lol", "yo mama", "2thic2vent", "dababy", "yeah boiii", "demon", "Freddy", "Lanky", "carrot", "jesse", "haha i", "moon", "Pennywise", "Ghost420", "fuzzypear", "ur mom", "Shadow", "king", "SlimButFat" };
                string randomName = playerName[Random.Range(0, playerName.Length)];

                allPlayers.SetName(randomName, true);
                allPlayers.SetColor(Random.Range(0, 18));
                allPlayers.SetHat((uint)Random.Range(1, 115), allPlayers.Data.ColorId);
                allPlayers.SetSkin((uint)Random.Range(1, 19));
                allPlayers.SetPet((uint)Random.Range(1, 12));
            }
        }
    }
}
