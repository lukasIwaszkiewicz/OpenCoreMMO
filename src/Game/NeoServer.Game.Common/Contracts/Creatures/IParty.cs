﻿using System;
using System.Collections.Generic;
using NeoServer.Game.Common.Contracts.Chats;
using NeoServer.Game.Common.Contracts.Services;

namespace NeoServer.Game.Common.Contracts.Creatures
{
    public delegate void PlayerJoinedParty(IParty party, IPlayer player);
    public delegate void PlayerLeftParty(IParty party, IPlayer player);
    public interface IParty
    {
        IReadOnlyCollection<IPlayer> Members { get; }
        bool IsOver { get; }
        IPlayer Leader { get; }
        IReadOnlyCollection<uint> Invites { get; }
        IChatChannel Channel { get; }
        ISharedExperienceService SharedExperienceService { get; }

        event Action OnPartyOver;
        event PlayerJoinedParty OnPlayerJoin;
        event PlayerLeftParty OnPlayerLeave;

        Result ChangeLeadership(IPlayer from, IPlayer to);
        Result Invite(IPlayer by, IPlayer invitedPlayer);
        bool IsInvited(IPlayer player);
        bool IsLeader(IPlayer player);
        bool IsLeader(uint creatureId);
        bool IsMember(uint creatureId);
        bool IsMember(IPlayer player);
        bool JoinPlayer(IPlayer player);
        Result PassLeadership(IPlayer from);
        void RemoveMember(IPlayer player);
        void RevokeInvite(IPlayer by, IPlayer invitedPlayer);
    }
}