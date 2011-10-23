using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shooter.Screens;



namespace Asteroids.Screens
{
    public class GameOver : Screen
    {

        #region Methods

        #region Constructor and Initialize

        public GameOver()
            : base("GameOver")
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
            base.Destroy();




        }

        #endregion


        #endregion
    }
}
