﻿using NeoServer.Game.Enums;

namespace NeoServer.Networking.Packets.Outgoing
{
    public sealed class TextMessageOutgoingParser
    {
        public static string Parse(InvalidOperation error) =>
                error switch
                {
                    InvalidOperation.NotEnoughRoom => "There is not enough room.",
                    InvalidOperation.Impossible => "This is impossible",
                    InvalidOperation.BothHandsNeedToBeFree => "Both hands need to be free.",
                    InvalidOperation.CannotDress => "You cannot dress this object there.",
                    InvalidOperation.NotPossible => "Sorry, not possible.",
                    InvalidOperation.TooHeavy => "This object is too heavy for you to carry.",
                    _ => string.Empty
                };

    }
}
