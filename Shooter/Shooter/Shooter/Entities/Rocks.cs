using System;
using System.Reflection; // for MethodInfousing System.Collections.Generic;
using System.Text;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.Math;
using FlatRedBall.Graphics;
using FlatRedBall.Math.Geometry;
using FlatRedBall.Content.Polygon;
using FlatRedBall.Instructions; // for StaticMethodInstruction
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using FlatRedBall.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace Asteroids.Entities
{
    class Rocks
    {
        // We are going to use circles for the asteroids, for there collisions, for now they will be visiable. Later we add sprites.
        private PositionedObjectList<Circle> mAsteroids;
        private SoundEffect explosionSoundEffect;
        private int rockSize = 25;
        private int rockSpeed = 25;

        public int Speed
        {
            get { return rockSpeed; }
            set { rockSpeed = value; }
        }

        //We need to have outside access to the collisions so they can be tested against other entities inside the GameScreen.
        public PositionedObjectList<Circle> Collisions
        {
            get { return mAsteroids; }
        }

        public Rocks(string contentManagerName)
        {
            mAsteroids = new PositionedObjectList<Circle>();
           
        }

        public void Initialize(int numberOfRocks)
        {
            mAsteroids = new PositionedObjectList<Circle>();
            explosionSoundEffect = FlatRedBallServices.Game.Content.Load<SoundEffect>("sound/explosion");
            this.CreateAsteroids((int)SpriteManager.Camera.RelativeXEdgeAt(0) * 2, (int)SpriteManager.Camera.RelativeYEdgeAt(0) * 2, rockSize, rockSpeed, numberOfRocks);


            
        }

        public void Activity()
        {

            this.updateEachRockActivity();
        }

        public void Destroy()
        {
            while (mAsteroids.Count > 0)
            {
                mAsteroids.Last.RemoveSelfFromListsBelongingTo();
            }
        }

        public void createDefaultAsteroid()
        {
            this.CreateAsteroids((int)SpriteManager.Camera.RelativeXEdgeAt(0) * 2, (int)SpriteManager.Camera.RelativeYEdgeAt(0) * 2, rockSize, rockSpeed, 1);
        }

        public bool notInCenter(float x, float y, int RockSize)
        {
            return true;// (x < (RockSize * 4) && x > (-RockSize * 4)) && (y < (RockSize * 4) && y > (-RockSize * 4));
        }

        public bool notInOtherRocks(Circle circle)
        {
            foreach (Circle asteroid in mAsteroids)
            {
                if (asteroid.CollideAgainst(circle))
                {
                    // Console.WriteLine("avoiding collisions");
                    return false;
                }
            }
            return true;
        }

        public void CreateAsteroids(int MaxX, int MaxY, int RockSize, int RockSpeed, int NumberOfRocks)
        {
            MaxX = MaxX - RockSize;
            MaxY = MaxY - RockSize;
            int xCalc = (MaxX / 2);
            int yCalc = (MaxY / 2);
            for (int i = 0; i < NumberOfRocks; i++)
            {
                Circle circle = ShapeManager.AddCircle();
                // Give the circle a size, in pixels. We are using flat screen type for this game, so 1 = 1 pixel.
                circle.Radius = RockSize;
                //We use the RockSize so they don't go off the edge to begin with.

                //TODO: This causes a hang if lots of asteroids are created
                // Give it a location not at the center of the screen
                circle.X = (float)FlatRedBallServices.Random.NextDouble() * MaxX - xCalc;
                circle.Y = (float)FlatRedBallServices.Random.NextDouble() * MaxY - yCalc;
                int count = 0;
                int retries = 10;

                int centerRight = xCalc + 15;
                int centerLeft = xCalc - 15;
                int centerTop = yCalc + 15;
                int centerBottom = yCalc - 15;
                bool inCenter = (circle.X < centerRight && circle.X > centerLeft && circle.Y > centerBottom && circle.Y < centerTop);
                while (!inCenter && !notInOtherRocks(circle) && count < retries)
                {
                    circle.X = (float)FlatRedBallServices.Random.NextDouble() * MaxX - xCalc;
                    circle.Y = (float)FlatRedBallServices.Random.NextDouble() * MaxY - yCalc;
                    inCenter = (circle.X < centerRight && circle.X > centerLeft && circle.Y > centerBottom && circle.Y < centerTop);
                    count++;
                }

                if (count < retries)
                {
                    //Console.WriteLine("new circle x,y = ({0},{1})", circle.X, circle.Y);
                    // Give it a random speed in a random direction
                    float magnitude = (float)FlatRedBallServices.Random.NextDouble() * RockSpeed + 3;
                    float angle = (float)(FlatRedBallServices.Random.NextDouble() * 2 * Math.PI);
                    circle.XVelocity = (float)(Math.Cos(angle) * magnitude);
                    circle.YVelocity = (float)(Math.Sin(angle) * magnitude);
                    // And add them to the Asteroid List:
                    mAsteroids.Add(circle);
                }
                else
                {
                    //we aren't going to use this circle
                    SpriteManager.RemovePositionedObject(circle);
                }
            }
        }

        public void AsteroidHit(int Rock)
        {
            explosionSoundEffect.Play();
            ShapeManager.Remove(mAsteroids[Rock]);
        }

        public void updateEachRockActivity()
        {
            foreach (PositionedObject Rock in mAsteroids)
            {
                // Check each side and wrap if necessary
                if (Rock.X > SpriteManager.Camera.RelativeXEdgeAt(0))
                    Rock.X = -SpriteManager.Camera.RelativeXEdgeAt(0);

                if (Rock.X < -SpriteManager.Camera.RelativeXEdgeAt(0))
                    Rock.X = SpriteManager.Camera.RelativeXEdgeAt(0);

                if (Rock.Y > SpriteManager.Camera.RelativeYEdgeAt(0))
                    Rock.Y = -SpriteManager.Camera.RelativeYEdgeAt(0);

                if (Rock.Y < -SpriteManager.Camera.RelativeYEdgeAt(0))
                    Rock.Y = SpriteManager.Camera.RelativeYEdgeAt(0);
            }
        }
    }
}
