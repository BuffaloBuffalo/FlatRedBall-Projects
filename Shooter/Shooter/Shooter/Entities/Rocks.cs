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
        private int iNumberOfAsteroids = 4;

        //We need to have outside access to the collisions so they can be tested against other entities inside the GameScreen.
        public PositionedObjectList<Circle> Collisions
        {
            get { return mAsteroids; }
        }

        public Rocks(string contentManagerName)
        {
            mAsteroids = new PositionedObjectList<Circle>();
        }

        public void Initialize()
        {
            this.CreateAsteroids();
        }

        public void Activity()
        {

        }

        public void Destroy()
        {
            while (mAsteroids.Count > 0)
            {
                mAsteroids.Last.RemoveSelfFromListsBelongingTo();
            }
        }

        private void CreateAsteroids()
        {
            mAsteroids = new PositionedObjectList<Circle>();
            for (int i = 0; i < iNumberOfAsteroids; i++)
            {
                Console.WriteLine(ShapeManager.ToString());
                Circle circle = ShapeManager.AddCircle();
                // Give the circle a size, in pixels. We are using flat screen type for this game, so 1 = 1 pixel.
                circle.Radius = 15;
                // Give it a random speed in a random direction
                float magnitude = (float)FlatRedBallServices.Random.NextDouble() * 5 + 2;
                float angle = (float)(FlatRedBallServices.Random.NextDouble() * 2 * Math.PI);
                circle.XVelocity = (float)(Math.Cos(angle) * magnitude);
                circle.YVelocity = (float)(Math.Sin(angle) * magnitude);
                // And add them to the Asteroid List:
                mAsteroids.Add(circle);
            }
        }
    }
}
