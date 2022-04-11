using System;
using Microsoft.DirectX;
using Nex.Data.Raw;
using Nex.Offsets;
using Nex.Utils;

namespace Nex.Data.Internal
{
    /// <summary>
    /// Base class for entities.
    /// </summary>
    public abstract class EntityBase
    {
        #region // storage

        /// <summary>
        /// Address base of entity.
        /// </summary>
        public IntPtr AddressBase { get; protected set; }

        /// <summary>
        /// Life state (true = dead, false = alive).
        /// </summary>
        public bool LifeState { get; protected set; }

        public int LocalTeam { get; protected set; }
        public IntPtr LocalPlayer { get; protected set; }

        /// <summary>
        /// Health points.
        /// </summary>
        public int Health { get; protected set; }

        /// <inheritdoc cref="Team"/>
        public Team Team { get; protected set; }

        /// <summary>
        /// Model origin (in world).
        /// </summary>
        public Vector3 Origin { get; private set; }

        #endregion

        #region // routines

        /// <summary>
        /// Is entity alive?
        /// </summary>
        public virtual bool IsAlive()
        {
            return AddressBase != IntPtr.Zero &&
                   !LifeState &&
                   Health > 0 &&
                   (Team == Team.Terrorists || Team == Team.CounterTerrorists);
        }

        /// <summary>
        /// Read <see cref="AddressBase"/>.
        /// </summary>
        protected abstract IntPtr ReadAddressBase(GameProcess gameProcess);

        /// <summary>
        /// Update data from process.
        /// </summary>
        public virtual bool Update(GameProcess gameProcess)
        {
            AddressBase = ReadAddressBase(gameProcess);
            if (AddressBase == IntPtr.Zero)
            {
                return false;
            }
            LocalPlayer = Memory.Read<IntPtr>(Memory.clientBase + Offsets.Offsets.dwLocalPlayer);
            LocalTeam = Memory.Read<int>((int)(LocalPlayer + Offsets.Offsets.m_iTeamNum));
            LifeState = gameProcess.Process.Read<bool>(AddressBase + Offsets.Offsets.m_lifeState);
            Health = gameProcess.Process.Read<int>(AddressBase + Offsets.Offsets.m_iHealth);
            Team = gameProcess.Process.Read<int>(AddressBase + Offsets.Offsets.m_iTeamNum).ToTeam();
            Origin = gameProcess.Process.Read<Vector3>(AddressBase + Offsets.Offsets.m_vecOrigin);
            return true;
        }

        #endregion
    }
}
