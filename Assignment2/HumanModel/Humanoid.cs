using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Assignment2.HumanModel
{
    public abstract class Humanoid : IHumanoid
    {
        private static readonly Vector3 FRONT_TOP_LEFT = new Vector3(-1f, 1f, 1f);
        private static readonly Vector3 FRONT_TOP_RIGHT = new Vector3(1f, 1f, 1f);
        private static readonly Vector3 FRONT_BOTTOM_LEFT = new Vector3(-1f, -1f, 1f);
        private static readonly Vector3 FRONT_BOTTOM_RIGHT = new Vector3(1f, -1f, 1f);
        private static readonly Vector3 BACK_TOP_LEFT = new Vector3(-1f, 1f, -1f);
        private static readonly Vector3 BACK_TOP_RIGHT = new Vector3(1f, 1f, -1f);
        private static readonly Vector3 BACK_BOTTOM_LEFT = new Vector3(-1f, -1f, -1f);
        private static readonly Vector3 BACK_BOTTOM_RIGHT = new Vector3(1f, -1f, -1f);

        private static readonly Vector3 RIGHT = new Vector3(1, 0, 0);
        private static readonly Vector3 LEFT = new Vector3(-1, 0, 0);
        private static readonly Vector3 UP = new Vector3(0, 1, 0);
        private static readonly Vector3 DOWN = new Vector3(0, -1, 0);
        private static readonly Vector3 FORWARD = new Vector3(0, 0, 1);
        private static readonly Vector3 BACKWARD = new Vector3(0, 0, -1);

        public void DrawLimb(GameTime gameTime, Matrix World)
        {
            throw new NotImplementedException();
        }

        public void UpdateLimb(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
