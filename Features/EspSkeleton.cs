using System.Drawing;
using Nex.Data.Internal;
using Nex.Data.Raw;
using Nex.Gfx;
using Nex.Utils;
using Graphics = Nex.Gfx.Graphics;

namespace Nex.Features
{
    /// <summary>
    /// ESP Skeleton.
    /// </summary>
    public static class EspSkeleton
    {
        /// <summary>
        /// Draw skeleton.
        /// </summary>
        public static void Draw(Graphics graphics)
        {
            foreach (var entity in graphics.GameData.Entities)
            {
                // validate
                if (!entity.IsAlive() || entity.AddressBase == graphics.GameData.Player.AddressBase
                   || entity.Team == entity.LocalTeam.ToTeam() // No teammate
                   || entity.AddressBase == entity.LocalPlayer // No self
                   || ((int)entity.GetDistance()) == 0 // No self cam (sep mode)
                   )
                {
                    continue;
                }

                // draw
                var color = entity.isSpottedByMask() == true ? Color.DodgerBlue : Color.Gold;
                Draw(graphics, entity, color);
            }
        }
        
        /// <summary>
        /// Draw skeleton of given entity.
        /// </summary>
        public static void Draw(Graphics graphics, Entity entity, Color color)
        {
            for (var i = 0; i < entity.SkeletonCount; i++)
            {
                var (from, to) = entity.Skeleton[i];

                // validate
                if (from == to || from < 0 || to < 0 || from >= Offsets.Offsets.MAXSTUDIOBONES || to >= Offsets.Offsets.MAXSTUDIOBONES)
                {
                    continue;
                }

                // draw
                graphics.DrawPolylineWorld(color, entity.BonesPos[from], entity.BonesPos[to]);
            }
        }
    }
}
