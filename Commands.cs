using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.Xna.Framework;

namespace urukx
{
    public class Commands
    {
        public Commands()
        {

        }

        public bool MoveBeingBy(Being being, Point position)
        {
            return being.MoveBy(position);
        }
    }
}
