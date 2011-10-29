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
    class PlayerShip : PositionedObject
    {
        private Polygon pCollision;
        private PositionedObjectList<AxisAlignedRectangle> mBullets;
        private int numberOfLives = 3;
        private SoundEffect shootSoundEffect;
        private static int MaxBullets= 4;
        public int Lives { get { return numberOfLives; } }
        public Polygon Collision
        {
            get { return pCollision; }
        }

        public PositionedObjectList<AxisAlignedRectangle> Bullets
        {
            get { return mBullets; }
        }

        public PlayerShip(string contentManagerName)
        {
            SpriteManager.AddPositionedObject(this);
            mBullets = new PositionedObjectList<AxisAlignedRectangle>();
        }

        public void Initialize()
        {
            this.createCollision();
            shootSoundEffect = FlatRedBallServices.Game.Content.Load<SoundEffect>("sound/shoot");
            //base.Initialize();
        }

        public void Activity()
        {
            this.applyKeyboardInput();
            this.WrapPlayerObjects();
        }

        public void Destroy()
        {
            SpriteManager.RemovePositionedObject(this);
            ShapeManager.Remove(pCollision);

            while (mBullets.Count != 0)
            {
                mBullets.Last.RemoveSelfFromListsBelongingTo();
            }
        }

        public void BulletHit(int Bullet)
        {
            ShapeManager.Remove(mBullets[Bullet]);
        }

        public void Hit()
        {
            Reset();
            if (numberOfLives > 0)
                numberOfLives--;

            if (numberOfLives == 0)
                Dead();
        }

        public void NewLife()
        {
            numberOfLives++;
        }

        public void NewGame()
        {
            Reset();
            numberOfLives = 3;
        }

        private void createCollision()
        {
            // The polygon will be visible by default, so this will serve as both our visible representation
            // as well as collision.
            pCollision = ShapeManager.AddPolygon();
            Point[] polygonPoints = new Point[4];
            polygonPoints[0] = new Point(11, 0);
            polygonPoints[1] = new Point(-11, -8);
            polygonPoints[2] = new Point(-11, 8);
            polygonPoints[3] = polygonPoints[0];
            pCollision.Points = polygonPoints;
            // Finally attach the collision to this: That makes it eaasier to access all its properties.
            pCollision.AttachTo(this, false);
        }

        private void applyKeyboardInput()
        {
            if (InputManager.Keyboard.KeyPushed(Keys.Space))
            {
                if (mBullets.Count < MaxBullets)
                {
                    FireShot();
                }
            }

            RotationZVelocity = 0;

            if (XVelocity > 0)
                XAcceleration = -2;

            if (XVelocity < 0)
                XAcceleration = 2;

            if (YVelocity > 0)
                YAcceleration = -2;

            if (YVelocity < 0)
                YAcceleration = 2;

            if (InputManager.Keyboard.KeyDown(Keys.Left))
                ShipRotateLeft();

            if (InputManager.Keyboard.KeyDown(Keys.Right))
                ShipRotateRight();

            if (InputManager.Keyboard.KeyDown(Keys.Up))
                ShipThrust();
        }

        private void ShipRotateLeft()
        {
            // RotationZVelocity is a field that exists in the PositionedObject
            // class.  Since Ship inherits from PositionedObject, this field can be
            // set.
            RotationZVelocity = 2;
        }

        private void ShipRotateRight()
        {
            RotationZVelocity = -2;
        }

        private void ShipThrust()
        {
            // increase to make the ship accelerate faster
            if (XVelocity < 200 && YVelocity < 200 && XVelocity > -200 && YVelocity > -200)
                Acceleration = RotationMatrix.Right * 50;
        }

        private void FireShot()
        {
            AxisAlignedRectangle newBullet = ShapeManager.AddAxisAlignedRectangle();
            newBullet.ScaleX = 2;
            newBullet.ScaleY = 2;
            float secondsToLast = 0.65f;
            MethodInfo methodInfo = typeof(ShapeManager).GetMethod("Remove", new Type[] { typeof(AxisAlignedRectangle) });
            MethodInfo spritemethodInfo = typeof(SpriteManager).GetMethod("RemoveSprite", new Type[] { typeof(Sprite) });
            // This will remove the bullet after 0.65 seconds
            newBullet.Instructions.Add(new StaticMethodInstruction(methodInfo, new object[] { newBullet },
                    TimeManager.CurrentTime + secondsToLast));
            //This makes it so the bullet spawns at the nose of the ship.
            newBullet.Position = this.Position + this.RotationMatrix.Right * 11;
            //This sets the bullet speed.
            newBullet.Velocity = this.RotationMatrix.Right * 650 + (Velocity / 4);
            mBullets.Add(newBullet);
            shootSoundEffect.Play();

        }


        private void WrapPlayerObjects()
        {
            foreach (PositionedObject Shot in mBullets)
            {
                // Check each side and wrap if necessary
                if (Shot.X > SpriteManager.Camera.RelativeXEdgeAt(0))
                    Shot.X = -SpriteManager.Camera.RelativeXEdgeAt(0);

                if (Shot.X < -SpriteManager.Camera.RelativeXEdgeAt(0))
                    Shot.X = SpriteManager.Camera.RelativeXEdgeAt(0);

                if (Shot.Y > SpriteManager.Camera.RelativeYEdgeAt(0))
                    Shot.Y = -SpriteManager.Camera.RelativeYEdgeAt(0);

                if (Shot.Y < -SpriteManager.Camera.RelativeYEdgeAt(0))
                    Shot.Y = SpriteManager.Camera.RelativeYEdgeAt(0);
            }

            if (X > SpriteManager.Camera.RelativeXEdgeAt(0) && XVelocity > 0)
                X = -SpriteManager.Camera.RelativeXEdgeAt(0);

            if (X < -SpriteManager.Camera.RelativeXEdgeAt(0) && XVelocity < 0)
                X = SpriteManager.Camera.RelativeXEdgeAt(0);

            if (Y > SpriteManager.Camera.RelativeYEdgeAt(0) && YVelocity > 0)
                Y = -SpriteManager.Camera.RelativeYEdgeAt(0);

            if (Y < -SpriteManager.Camera.RelativeYEdgeAt(0) && YVelocity < 0)
                Y = SpriteManager.Camera.RelativeYEdgeAt(0);
        }

        private void Reset()
        {
            XVelocity = 0;
            YVelocity = 0;
            XAcceleration = 0;
            YAcceleration = 0;
            X = 0;
            Y = 0;
            RotationZ = 0;
        }

        private void Dead()
        {
            pCollision.Visible = false;
        }
    }
}
