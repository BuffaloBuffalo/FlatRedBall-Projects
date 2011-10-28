using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlatRedBall;
using Asteroids.Entities;

namespace Shooter.Screens
{
    class TestScreen : Screen
    {
        private Rocks RockSprites;
        public TestScreen()
            : base("TestScreen")
        {
            // Don't put initialization code here, do it in
            // the Initialize method below
            //   |   |   |   |   |   |   |   |   |   |   |
            //   |   |   |   |   |   |   |   |   |   |   |
            //   V   V   V   V   V   V   V   V   V   V   V

        }

        public override void Initialize(bool addToManagers)
        {
            // Set the screen up here instead of in the Constructor to avoid
            // exceptions occurring during the constructor.

            // AddToManagers should be called LAST in this method:
            if (addToManagers)
            {
                AddToManagers();
            }

            SpriteManager.Camera.UsePixelCoordinates();
            //We have a content manager in here for when we will be adding spritse later.
            RockSprites = new Rocks("Rocks");
            //I decided to seperate this for easier reading. This will be called every new level as well later on.
            RockSprites.Initialize(25);


        }


        public override void Activity(bool firstTimeCalled)
        {
            RockSprites.Activity();

        }
    }
}
