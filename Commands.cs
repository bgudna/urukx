using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.Xna.Framework;

namespace urukx
{
    public class Commands
    {
        private Point _lastMoveBeingPoint;
        private Being _lastMoveBeing;

        public Commands()
        {

        }

        public bool MoveBeingBy(Being being, Point position)
        {
            // keeps the beings previous movement
            _lastMoveBeing = being;
            _lastMoveBeingPoint = position;

            return being.MoveBy(position);
        }

        public bool RedoMoveBeingBy()
        {
            if (_lastMoveBeing != null)
            {
                return _lastMoveBeing.MoveBy(_lastMoveBeingPoint);
            }
            else
                return false;
        }

        // Undo last being move / command
        // then clear the undo so it cannot be repeated
        public bool UndoMoveBeingBy()
        {

            if (_lastMoveBeing != null)
            {
                // reverse the directions of the last move
                _lastMoveBeingPoint = new Point(-_lastMoveBeingPoint.X, -_lastMoveBeingPoint.Y);

                if (_lastMoveBeing.MoveBy(_lastMoveBeingPoint))
                {
                    _lastMoveBeingPoint = new Point(0, 0);
                    return true;
                }
                else
                {
                    _lastMoveBeingPoint = new Point(0, 0);
                    return false;
                }
            }
            return false;
        }
    }
}
