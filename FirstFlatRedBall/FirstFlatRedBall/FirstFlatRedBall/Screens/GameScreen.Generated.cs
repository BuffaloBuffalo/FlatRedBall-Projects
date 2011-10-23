using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall.Math.Geometry;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Input;
using FlatRedBall.IO;
using FlatRedBall.Instructions;
using FlatRedBall.Math.Splines;
using FlatRedBall.Utilities;
using BitmapFont = FlatRedBall.Graphics.BitmapFont;

using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;

#if XNA4
using Color = Microsoft.Xna.Framework.Color;
#else
using Color = Microsoft.Xna.Framework.Graphics.Color;
#endif

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
using Microsoft.Xna.Framework.Media;
#endif

// Generated Usings
using FlatRedBall.Broadcasting;
using FirstFlatRedBall.Entities;
using FlatRedBall;
using FlatRedBall.Math.Geometry;
using FlatRedBall.Graphics;

namespace FirstFlatRedBall.Screens
{
	public partial class GameScreen : Screen
	{
		// Generated Fields
		#if DEBUG
		static bool HasBeenLoadedWithGlobalContentManager = false;
		#endif
		private ShapeCollection CollectionFile;
		private ShapeCollection GoalAreaFile;
		
		private FirstFlatRedBall.Entities.PlayerBall PlayerBallInstance;
		static Microsoft.Xna.Framework.Vector3 PlayerBallInstancePositionReset;
		private FirstFlatRedBall.Entities.Puck PuckInstance;
		private AxisAlignedRectangle LeftGoal;
		private AxisAlignedRectangle RightGoal;
		private Layer HudLayer;
		private FirstFlatRedBall.Entities.ScoreHud ScoreHudInstance;

		public GameScreen()
			: base("GameScreen")
		{
		}

        public override void Initialize(bool addToManagers)
        {
			// Generated Initialize
			LoadStaticContent(ContentManagerName);
			CollectionFile = FlatRedBallServices.Load<ShapeCollection>("content/screens/gamescreen/collectionfile.shcx", ContentManagerName);
			GoalAreaFile = FlatRedBallServices.Load<ShapeCollection>("content/screens/gamescreen/goalareafile.shcx", ContentManagerName);
			PlayerBallInstance = new FirstFlatRedBall.Entities.PlayerBall(ContentManagerName, false);
			PlayerBallInstance.Name = "PlayerBallInstance";
			PuckInstance = new FirstFlatRedBall.Entities.Puck(ContentManagerName, false);
			PuckInstance.Name = "PuckInstance";
			LeftGoal = GoalAreaFile.AxisAlignedRectangles.FindByName("Left Goal");
			RightGoal = GoalAreaFile.AxisAlignedRectangles.FindByName("Right Goal");
			ScoreHudInstance = new FirstFlatRedBall.Entities.ScoreHud(ContentManagerName, false);
			ScoreHudInstance.Name = "ScoreHudInstance";
			
			
			PostInitialize();
			PlayerBallInstancePositionReset = PlayerBallInstance.Position;
			base.Initialize(addToManagers);
			if (addToManagers)
			{
				AddToManagers();
			}

        }
        
// Generated AddToManagers
		public override void AddToManagers ()
		{
			HudLayer = SpriteManager.AddLayer();
			HudLayer.UsePixelCoordinates();
			AddToManagersBottomUp();
			CustomInitialize();
		}


		public override void Activity(bool firstTimeCalled)
		{
			// Generated Activity
			if (!IsPaused)
			{
				
				PlayerBallInstance.Activity();
				PuckInstance.Activity();
				ScoreHudInstance.Activity();
			}
			else
			{
			}
			base.Activity(firstTimeCalled);
			if (!IsActivityFinished)
			{
				CustomActivity(firstTimeCalled);
			}


				// After Custom Activity
				
            
		}

		public override void Destroy()
		{
			// Generated Destroy
			if (PlayerBallInstance != null)
			{
				PlayerBallInstance.Destroy();
			}
			if (PuckInstance != null)
			{
				PuckInstance.Destroy();
			}
			if (HudLayer != null)
			{
				SpriteManager.RemoveLayer(HudLayer);
			}
			if (ScoreHudInstance != null)
			{
				ScoreHudInstance.Destroy();
			}
			CollectionFile.RemoveFromManagers(ContentManagerName != "Global");
			GoalAreaFile.RemoveFromManagers(ContentManagerName != "Global");
			

			base.Destroy();

			CustomDestroy();

		}

		// Generated Methods
public virtual void PostInitialize ()
{
}
public virtual void AddToManagersBottomUp ()
{
	CollectionFile.AddToManagers(mLayer);
	GoalAreaFile.AddToManagers(mLayer);
	PlayerBallInstance.AddToManagers(mLayer);
	PlayerBallInstance.Position = PlayerBallInstancePositionReset;
	PuckInstance.AddToManagers(mLayer);
	ScoreHudInstance.AddToManagers(HudLayer);
}
public virtual void ConvertToManuallyUpdated ()
{
	PlayerBallInstance.ConvertToManuallyUpdated();
	PuckInstance.ConvertToManuallyUpdated();
	ScoreHudInstance.ConvertToManuallyUpdated();
}
public static void LoadStaticContent (string contentManagerName)
{
	#if DEBUG
	if (contentManagerName == FlatRedBallServices.GlobalContentManager)
	{
		HasBeenLoadedWithGlobalContentManager = true;
	}
	else if (HasBeenLoadedWithGlobalContentManager)
	{
		throw new Exception("This type has been loaded with a Global content manager, then loaded with a non-global.  This can lead to a lot of bugs");
	}
	#endif
	FirstFlatRedBall.Entities.PlayerBall.LoadStaticContent(contentManagerName);
	FirstFlatRedBall.Entities.Puck.LoadStaticContent(contentManagerName);
	FirstFlatRedBall.Entities.ScoreHud.LoadStaticContent(contentManagerName);
	CustomLoadStaticContent(contentManagerName);
}
object GetMember (string memberName)
{
	switch(memberName)
	{
	}
	return null;
}


	}
}
