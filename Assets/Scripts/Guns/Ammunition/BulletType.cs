using System;
using System.Collections.Generic;
using System.Linq;

namespace Guns.Ammunition {
    public enum BulletType {
        A,
        B
    }

    internal static class BulletTypeExtensions {
        internal static Dictionary<BulletType, int> GetAmmoInit() => Enum.GetValues(typeof(BulletType))
            .Cast<BulletType>()
            .ToDictionary(type => type, type => type.DefaultAmount());

        internal static int DefaultAmount(this BulletType bulletType) => bulletType switch {
            BulletType.A => 36,
            BulletType.B => 100,
            _            => throw new ArgumentOutOfRangeException()
        };
    }
}