﻿using NeoServer.Game.Common;
using NeoServer.Game.Common.Contracts.Creatures;
using NeoServer.Server.Model.Players.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoServer.Game.Creatures.Model.Players
{
    public class Party: IParty
    {
        public event RevokePartyInvite OnRevokePartyInvite;
        public event JoinParty OnPlayerJoinedParty;
        public event LeaveParty OnPlayerLeftParty;

        private uint leader;
        private HashSet<uint> members = new HashSet<uint>();
        private HashSet<uint> invites = new HashSet<uint>();

        public IReadOnlyCollection<uint> Members => invites.Append(leader).ToList();

        public Party(IPlayer player)
        {
            leader = player.CreatureId;   
        }

        public bool IsInvited(IPlayer player) => invites.Contains(player.CreatureId);
        public bool IsLeader(IPlayer player) => player.CreatureId == leader;
        public bool JoinPlayer(IPlayer player)
        {
            if(Guard.AnyNull(player)) return false;

            if (!IsInvited(player)) return false;

            members.Add(player.CreatureId);
            OnPlayerJoinedParty?.Invoke(player, this);
            return true;
        }
        public Result Invite(IPlayer by, IPlayer invitedPlayer)
        {
            if (invitedPlayer.IsInParty) return new Result(InvalidOperation.CannotInvite);
            if (!IsLeader(by)) return new Result(InvalidOperation.CannotInvite);

            invites.Add(invitedPlayer.CreatureId);

            return Result.Success;
        }
        public void RevokeInvite(IPlayer by, IPlayer invitedPlayer)
        {
            if (!IsLeader(by)) return;
            if (!invites.Remove(invitedPlayer.CreatureId)) return;

            OnRevokePartyInvite?.Invoke(by, invitedPlayer, this);
        }
        public void RemoveMember(IPlayer player)
        {
            if (player.InFight) return;

            members.Remove(player.CreatureId);
            OnPlayerLeftParty?.Invoke(player, this);
        }
    }
}