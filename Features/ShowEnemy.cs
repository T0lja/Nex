using Nex.Data;
using Nex.Utils;
using Nex.Offsets;

namespace Nex.Features
{
    /// <summary>
    /// Show every enemies on minimap
    /// </summary>
    public class ShowEnemy :
        ThreadedComponent
    {
        #region // storage

        /// <inheritdoc />
        protected override string ThreadName => nameof(ShowEnemy);

        /// <inheritdoc cref="GameData"/>
        private GameData GameData { get; set; }

        #endregion

        #region // ctor

        /// <summary />
        public ShowEnemy(GameData gameData)
        {

            GameData = gameData;
        }

        /// <inheritdoc />
        public override void Dispose()
        {
            base.Dispose();

            GameData = default;
        }

        #endregion

        #region // routines

        /// <inheritdoc />
        protected override void FrameAction()
        {
            foreach (var entity in GameData.Entities)
            {
                // validate
                if (!entity.IsAlive() || entity.AddressBase == GameData.Player.AddressBase
                   || entity.Team == entity.LocalTeam.ToTeam() // No teammate
                   || entity.AddressBase == entity.LocalPlayer // No self
                   || entity.GetDistance() == 0f // No self cam (sep mode)
                   )
                {
                    continue;
                }
                Memory.Write<int>((int)entity.AddressBase + Offsets.Offsets.m_bSpotted, 1);
            }
        }

        #endregion
    }
}
