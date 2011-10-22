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

namespace FirstFlatRedBall.Screens
{
	public partial class GameScreen : Screen
	{
		// Generated Fields
		#if DEBUG
		static bool HasBeenLoadedWithGlobalContentManager = false;
		#endif
		private ShapeCollection CollectionFile;

		private FirstFlatRedBall.Entities.PlayerBall PlayerBallInstance;
		private FirstFlatRedBall.Entities.Puck PuckInstance;

		public GameScreen()
			: base("GameScreen")
		{
		}

        public override void Initialize(bool addToManagers)
        {
			// Generated Initialize
			LoadStaticContent(ContentManagerName);
			CollectionFile = FlatRedBallServices.Load<ShapeCollection>("content/screens/gamescreen/collectionfile.shcx", ContentManagerName);
			PlayerBallInstance = new FirstFlatRedBall.Entities.PlayerBall(ContentManagerName, false);
			PlayerBallInstance.Name = "PlayerBallInstance";
			PuckInstance = new FirstFlatRedBall.Entities.Puck(ContentManagerName, false);
			PuckInstance.Name = "PuckInstance";



			PostInitialize();
			if(addToManagers)
			{
				AddToManagers();
			}

        }
        
// Generated AddToManagers

        public override void AddToManagers()
        {
			AddToManagersBottomUp();
			CustomInitialize();

        }


		public override void Activity(bool firstTimeCalled)
		{
			// Generated Activity
			if(!IsPaused)
			{

				PlayerBallInstance.Activity();
				PuckInstance.Activity();
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
			if(PlayerBallInstance != null)
			{
				PlayerBallInstance.Destroy();
			}
			if(PuckInstance != null)
			{
				PuckInstance.Destroy();
			}
			CollectionFile.RemoveFromManagers(ContentManagerName != "Global");



			base.Destroy();

			CustomDestroy();

		}

		// Generated Methods
		public virtual void PostInitialize()
		{
		}
		public virtual void AddToManagersBottomUp()
		{
			CollectionFile.AddToManagers(mLayer);

			PlayerBallInstance.AddToManagers(mLayer);
			PuckInstance.AddToManagers(mLayer);
		}
		public virtual void ConvertToManuallyUpdated()
		{
			PlayerBallInstance.ConvertToManuallyUpdated();
			PuckInstance.ConvertToManuallyUpdated();
		}
		public static void LoadStaticContent(string contentManagerName)
		{
			#if DEBUG
			if(contentManagerName == FlatRedBallServices.GlobalContentManager)
			{
				HasBeenLoadedWithGlobalContentManager = true;
			}
			else if(HasBeenLoadedWithGlobalContentManager)
			{
				throw new Exception("This type has been loaded with a Global content manager, then loaded with a non-global.  This can lead to a lot of bugs");
			}
			#endif
			FirstFlatRedBall.Entities.PlayerBall.LoadStaticContent(contentManagerName);
			FirstFlatRedBall.Entities.Puck.LoadStaticContent(contentManagerName);
			CustomLoadStaticContent(contentManagerName);
		}
		object GetMember(string memberName)
		{
			switch(memberName)
			{
				case "CollectionFile":
					return CollectionFile;
			}
			return null;
		}


	}
}
