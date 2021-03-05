﻿using System.Collections.Generic;

namespace NeoServer.Game.Contracts.Creatures
{
    public interface INpcType : ICreatureType
    {
        public string Script { get; set; }
        public INpcDialog[] Dialogs { get; init; }
        public IDictionary<string, dynamic> CustomAttributes { get;  }
        bool IsLuaScript { get; }
    }

    public interface INpcDialog
    {
        public string[] OnWords { get; init; }
        public string[] Answers { get; init; }
        public string Action { get; init; }
        public bool End { get; init; }
        public INpcDialog[] Then { get; init; }
        string StoreAt { get; init; }
    }
}