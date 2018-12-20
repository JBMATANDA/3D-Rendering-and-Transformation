using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.HumanModel
{
    public interface IHumanoid
    {
        void DrawLimb(GameTime gameTime, Matrix World);
        void UpdateLimb(GameTime gameTime);
    }
}
