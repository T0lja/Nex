using Nex.Data.Internal;
using Nex.Offsets;
using System;
using System.Windows.Controls;

namespace Nex.Objects;

static class CBasePlayer
{
    public static int LocalPlayerPtr
    {
        get
        {
            return Memory.Read<int>(Memory.clientBase + Offsets.Offsets.dwLocalPlayer);
        }
    }
    public static int GetViewModelIndex(int index)
    {
        return Memory.Read<int>(LocalPlayerPtr + Offsets.Offsets.m_hViewModel + index * 0x4) & 0xFFF;
    }
    public static int ActiveWeaponIndex
    {
        get
        {
            return Memory.Read<int>(LocalPlayerPtr + Offsets.Offsets.m_hActiveWeapon) & 0xFFF;
        }
    }
    public static int Team
    {
        get
        {
            return EntityBase.GetTeam(LocalPlayerPtr);

        }
    }
}
