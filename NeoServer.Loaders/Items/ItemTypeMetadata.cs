using System;
using System.Collections.Generic;

namespace NeoServer.Loaders.Items
{
    public class ItemTypeMetadata
    {
        public ushort? Id { get; set; }
        public string Name { get; set; }
        public ushort? Fromid { get; set; }
        public ushort? Toid { get; set; }
        public IEnumerable<Attribute> Attributes { get; set; }
        public string Article { get; set; }
        public string Plural { get; set; }
        public string Editorsuffix { get; set; }

        public class Attribute
        {
            public string Key { get; set; }
            public string Value { get; set; }
            public IEnumerable<Attribute> Attributes { get; set; }

        }
    }
}