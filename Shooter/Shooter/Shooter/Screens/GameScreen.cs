using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Asteroids.Screens;
using Asteroids.Entities;
using FlatRedBall;
using Shooter.Screens;


namespace Asteroids.Screens
{
    public class GameScreen : Screen
    {
        private Rocks RockSprites;

        #region Methods

        #region Constructor and Initialize

        public GameScreen()
            : base("GameScreen")
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

            RockSprites = new Rocks("Rocks"); //We have a content manager in here for when we will be adding spritse later.
            //I decided to seperate this for easier reading. This will be called every new level as well later on.
            RockSprites.Initialize();
            SpriteManager.Camera.UsePixelCoordinates(false);
        }

        public override void AddToManagers()
        {


        }

        #endregion

        #region Public Methods

        public override void Activity(bool firstTimeCalled)
        {
            base.Activity(firstTimeCalled);
        }

        public override void Destroy()
        {

            RockSprites.Destroy();
            base.Destroy();




        }

        #endregion


        #endregion
    }
}
