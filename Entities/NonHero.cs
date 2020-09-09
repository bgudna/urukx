using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace urukx.Entities
{
    public class NonHero : Being
    {
        public NonHero(Color foreground, Color background) : base(foreground, background, 'M')
        {
            Random rndNum = new Random();

            //number of loot to spawn for monster
            int lootNum = rndNum.Next(1, 4);

            for (int i = 0; i < lootNum; i++)
            {
                // monsters are made out of spork, obvs.
                Item newLoot = new Item(Color.HotPink, Color.Transparent, "spork", 'L', 2);
                newLoot.Components.Add(new SadConsole.Components.EntityViewSyncComponent());
                Inventory.Add(newLoot);
            }
        }
    }
}
