﻿using System;
using System.Collections.Generic;
using NeoServer.Game.Common.Contracts.Items;
using NeoServer.Game.Common.Contracts.Items.Types;
using NeoServer.Game.Common.Item;
using NeoServer.Game.Common.Location.Structs;
using NeoServer.Game.Items.Bases;
using NeoServer.Game.Items.Mappers;

namespace NeoServer.Game.Items.Items;

public class LiquidPool : BaseItem, ILiquid
{
    public LiquidPool(IItemType type, Location location, LiquidColor color): base(type, location)
    {
        LiquidColor = LiquidColor.Empty;
        LiquidColor = GetLiquidColor(color);
    }
    
    public LiquidPool(IItemType type, Location location,
        IDictionary<ItemAttribute, IConvertible> attributes) : base(type, location)
    {
        LiquidColor = LiquidColor.Empty;
        LiquidColor = GetLiquidColor(attributes);
    }

    public bool IsLiquidPool => Metadata.Group == ItemGroup.Splash;
    public bool IsLiquidSource => Metadata.Flags.Contains(ItemFlag.LiquidSource);
    public bool IsLiquidContainer => Metadata.Group == ItemGroup.ItemGroupFluid;
    public LiquidColor LiquidColor { get; }
    public ushort ClientId => Metadata.ClientId;

    private LiquidColor GetLiquidColor(LiquidColor color)
    {
        if (!IsLiquidPool && !IsLiquidContainer) return 0x00;

        return color;
    }

    private LiquidColor GetLiquidColor(IDictionary<ItemAttribute, IConvertible> attributes)
    {
        if (!IsLiquidPool && !IsLiquidContainer) return 0x00;
        if (attributes != null && attributes.TryGetValue(ItemAttribute.Count, out var count))
            return new LiquidTypeMap()[(byte)count];
        return LiquidColor.Empty;
    }
    public Span<byte> GetRaw()
    {
        Span<byte> cache = stackalloc byte[3];
        var idBytes = BitConverter.GetBytes(ClientId);

        cache[0] = idBytes[0];
        cache[1] = idBytes[1];
        cache[2] = (byte)LiquidColor;

        return cache.ToArray();
    }

    public static bool IsApplicable(IItemType type)
    {
        return type.Group == ItemGroup.Splash ||
               type.Flags.Contains(ItemFlag.LiquidSource) ||
               type.Group == ItemGroup.ItemGroupFluid;
    }
}