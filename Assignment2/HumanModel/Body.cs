using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.HumanModel
{
    public class Body : Humanoid
    {
        List<Humanoid> humanoid = new List<Humanoid>();

        Game1 game;
        private Vector3 parentPosition;

        public Body(string limb) : base("body")
        {
            Load();

            
            scale = new Vector3(3, 5, 3);
            parentPosition = new Vector3(0, 1, 0);

            humanoid.Add(new Head(game, "head", new Vector3(0, 4.8f, 0)));
            humanoid.Add(new RightArm(game, "rightArm", new Vector3(2.2f, 1.5f, 0)));
            humanoid.Add(new LeftArm(game, "leftArm", new Vector3(-2.2f, 1.5f, 0)));
            humanoid.Add(new RightLeg(game, "rightArm", new Vector3(0.8f, -3.8f, 0)));
            humanoid.Add(new LeftLeg(game, "leftArm", new Vector3(-0.8f, -3.8f, 0)));
        }
    }
}
