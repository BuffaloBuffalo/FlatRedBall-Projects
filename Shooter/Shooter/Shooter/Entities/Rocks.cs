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
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids.Entities
{
    class Rocks
    {
        // We are going to use circles for the asteroids, for there collisions, for now they will be visiable. Later we add sprites.
        private PositionedObjectList<Circle> mAsteroids;

        private int rockSize=25;
        private int rockSpeed = 10;

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
        public void CreateAsteroids(int MaxX, int MaxY, int RockSize, int RockSpeed, int NumberOfRocks)
        {
            mAsteroids = new PositionedObjectList<Circle>();
            for (int i = 0; i < NumberOfRocks; i++)
            {
                Circle circle = ShapeManager.AddCircle();
                // Give the circle a size, in pixels. We are using flat screen type for this game, so 1 = 1 pixel.
                circle.Radius = RockSize;
                //We use the RockSize so they don't go off the edge to begin with.
                MaxX = MaxX - RockSize;
                MaxY = MaxY - RockSize;
                //TODO: This causes a hang if lots of asteroids are created
                // Give it a location not at the center of the screen
                while (circle.X < (RockSize * 4) && circle.X > (-RockSize * 4))
                {
                    circle.X = (float)FlatRedBallServices.Random.NextDouble() * MaxX - (MaxX / 2);
                }
                while (circle.Y < (RockSize * 4) && circle.Y > (-RockSize * 4))
                {
                    circle.Y = (float)FlatRedBallServices.Random.NextDouble() * MaxY - (MaxY / 2);
                }
                // Give it a random speed in a random direction
                float magnitude = (float)FlatRedBallServices.Random.NextDouble() * RockSpeed + 3;
                float angle = (float)(FlatRedBallServices.Random.NextDouble() * 2 * Math.PI);
                circle.XVelocity = (float)(Math.Cos(angle) * magnitude);
                circle.YVelocity = (float)(Math.Sin(angle) * magnitude);
                // And add them to the Asteroid List:
                mAsteroids.Add(circle);
            }
        }

        public void AsteroidHit(int Rock)
        {
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
