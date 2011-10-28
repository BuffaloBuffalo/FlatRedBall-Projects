using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Asteroids.Screens;
using Asteroids.Entities;
using FlatRedBall;
using Shooter.Screens;
using FlatRedBall.Graphics;
using FlatRedBall.Math.Geometry;


namespace Asteroids.Screens
{
    public class GameScreen : Screen
    {

        private PlayerShip PlayerSprite;
        private Rocks RockSprites;
        private int numberOfRocks = 25;
        private int score;
        private int newLives = 1;
        private Text textScore = TextManager.AddText("");
        private Text textLives = TextManager.AddText("");


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

            SpriteManager.Camera.UsePixelCoordinates();

            //We have a content manager in here for when we will be adding sprites later.
            PlayerSprite = new PlayerShip("Player");
            //I decided to seperate this for easier reading. This will be called every time the player dies as well later on. That is where the defaults go.
            PlayerSprite.Initialize();

            //We have a content manager in here for when we will be adding spritse later.
            RockSprites = new Rocks("Rocks");
            //I decided to seperate this for easier reading. This will be called every new level as well later on.
            RockSprites.Initialize(numberOfRocks);


            buildText();


        }

        /**
         * Adds to the ScreenManager
         * */
        public override void AddToManagers()
        {


        }

        #endregion

        #region Public Methods

        public override void Activity(bool firstTimeCalled)
        {
            if (PlayerSprite.Lives > 0)
            {
                PlayerSprite.Activity();
                ShipVsRocks();
            }
            BulletVsRocks();
            //CollideRocks();
            RockSprites.Activity();
            KeepTrackOfRocks();
            UpdateText();
            base.Activity(firstTimeCalled);

        }

        public override void Destroy()
        {
            PlayerSprite.Destroy();

            RockSprites.Destroy();
            base.Destroy();




        }

        #endregion
        //Our first private method for the screen class. We need to go in reverse because if one is removed, they are shifted up. These are lists.
        private void BulletVsRocks()
        {
            bool HitRock = false;
            int RockHit = 0;
            int BulletHit = 0;

            for (int Bullet = PlayerSprite.Bullets.Count - 1; Bullet > -1; Bullet--)
            {
                for (int Rock = RockSprites.Collisions.Count - 1; Rock > -1; Rock--)
                {
                    if (PlayerSprite.Bullets[Bullet].CollideAgainst(RockSprites.Collisions[Rock]))
                    {
                        RockHit = Rock;
                        BulletHit = Bullet;
                        HitRock = true;
                        break;
                    }
                }
            }
            //I'm not sure why this logic is separate
            if (HitRock)
            {
                RockSprites.AsteroidHit(RockHit);
                PlayerSprite.BulletHit(BulletHit);
                score += 10;

                if (score / 1000 * newLives == 1)
                {
                    PlayerSprite.NewLife();
                    newLives++;
                }
            }
        }

        private void ShipVsRocks()
        {
            //going backwards because a rock can be removed from the list during iteration
            for (int Rock = RockSprites.Collisions.Count - 1; Rock > -1; Rock--)
            {
                if (PlayerSprite.Collision.CollideAgainst(RockSprites.Collisions[Rock]))
                {
                    RockSprites.AsteroidHit(Rock);
                    PlayerSprite.Hit();
                    break;
                }
            }
        }

        private void CollideRocks()
        {
            for (int outerRockIndex = RockSprites.Collisions.Count - 1; outerRockIndex > -1; outerRockIndex--)
            {
                Circle outerRock = RockSprites.Collisions[outerRockIndex];
                for (int innerRockIndex = outerRockIndex-1; innerRockIndex > -1; innerRockIndex--)
                {
                    Circle innerRock = RockSprites.Collisions[innerRockIndex];
                    if (outerRock.CollideAgainst(innerRock))
                    {

                        innerRock.XVelocity = -innerRock.XVelocity;
                        innerRock.YVelocity = -innerRock.YVelocity;


                        outerRock.XVelocity = -outerRock.XVelocity;
                        outerRock.YVelocity = -outerRock.YVelocity;
                    }
                }
            }
        }

        private void KeepTrackOfRocks()
        {
            if (RockSprites.Collisions.Count == 0 /*|| RockSprites.Collisions.Count < numberOfRocks-3*/)
            {
                numberOfRocks++;
                if (score > 5 * RockSprites.Speed && RockSprites.Speed < 150)
                {
                    //increase ze speed!
                    RockSprites.Speed = (int)(RockSprites.Speed * 2.5f);
                }
                RockSprites.Initialize(numberOfRocks);
            }
        }


        private void UpdateText()
        {
            textScore.DisplayText = "Score: " + score;
            textLives.DisplayText = " Lives: " + PlayerSprite.Lives;
        }


        private void buildText()
        {
            textScore.Scale = 10;
            textScore.Spacing = 10.7f;
            textLives.Scale = 10;
            textLives.Spacing = 10.7f;
            textScore.X = -SpriteManager.Camera.RelativeXEdgeAt(0) + 10;
            textScore.Y = SpriteManager.Camera.RelativeYEdgeAt(0) - 10;
            textLives.X = -SpriteManager.Camera.RelativeXEdgeAt(0) + 10;
            textLives.Y = SpriteManager.Camera.RelativeYEdgeAt(0) - 30;
        }
        #endregion
    }
}
