using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.Interfaces
{
    interface IModel
    {
        void Draw(Matrix view, Matrix projection);
    }
}
