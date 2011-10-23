using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;

using FlatRedBall.Graphics.Model;
using FlatRedBall.Math.Geometry;
using FlatRedBall.Math.Splines;

using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;
using FlatRedBall.Localization;

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
#endif

namespace FirstFlatRedBall.Screens
{
	public partial class GameScreen
	{
        int mScoreForTeam0 = 0;
        int mScoreForTeam1 = 0;

		void CustomInitialize()
		{


		}

		void CustomActivity(bool firstTimeCalled)
		{

            this.CollisionActivity();
		}

		void CustomDestroy()
		{


		}

        static void CustomLoadStaticContent(string contentManagerName)
        {


        }

        private void CollisionActivity()
        {
            //wall collisions
            PlayerBallInstance.Body.CollideAgainstBounce(CollectionFile, 0, 1,1);
            PuckInstance.Body.CollideAgainstBounce(CollectionFile, 0, 1, 1);

            //player->goal
            PlayerBallInstance.Body.CollideAgainstBounce(GoalAreaFile, 0, 1, 1);

            //player-puck collision
            PlayerBallInstance.Body.CollideAgainstBounce(PuckInstance.Body, 1, .3f, 1);

            //puck-goal
            if (PuckInstance.Body.CollideAgainst(LeftGoal))
            {
                AssignGoalToTeam(0);
            }
            if (PuckInstance.Body.CollideAgainst(RightGoal))
            {
                AssignGoalToTeam(1);
            }
        }

        private void AssignGoalToTeam(int teamIndex)
        {
            // Award points to the appropriate team...
            switch (teamIndex)
            {
                case 0:
                    mScoreForTeam0++;
                    break;
                case 1:
                    mScoreForTeam1++;
                    break;
                default:
                    throw new ArgumentException("Team index must be either 0 or 1");
                    break;
            }

            // and move all Entities back to their starting spots:
            ResetAllPositionsAndStates();
        }

        private void ResetAllPositionsAndStates()
        {
            PlayerBallInstance.Position = PlayerBallInstancePositionReset;
            PlayerBallInstance.Velocity = Vector3.Zero;
            PlayerBallInstance.Acceleration = Vector3.Zero;

            PuckInstance.X = 0;
            PuckInstance.Y = 0;
            PuckInstance.Velocity = Vector3.Zero;
        }
	}
}
