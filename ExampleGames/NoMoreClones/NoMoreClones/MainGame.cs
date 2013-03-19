#region Using Statements
using System; //We need to access the system category (this provides us with basic classes and methods like Strings, Ints and Array Methods. This should always be listed.
using System.Collections.Generic;
using Microsoft.Xna.Framework; //We need XNA classes and methods to do stuff with our game to make it clever.
using Microsoft.Xna.Framework.Graphics; //Graphics Classes like Textures and Rectangles
using Microsoft.Xna.Framework.Input; //For getting keyboard, mouse and gamepad input.

#endregion

namespace NoMoreClones //Home sweet home.
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class MainGame : Game //Tells C# we are programming a new class called MainGame which is 'base'd off Game. Like a template.
	{ //Everything inside these curley braces are part of the class.
		GraphicsDeviceManager graphics; //Handles anything where we need to look at the graphics card. Not that important for us.
		SpriteBatch spriteBatch; //This is however, it is the interface we use to draw to the screen and to clear it again.
		public List<Entity> entities = new List<Entity>(); //A List is a array which can expand dynamically, simply put: its a endless pit. This one holds our entity classes.
		public List<Entity> entities_toremove = new List<Entity>();
		List<MovingTarget> targets = new List<MovingTarget>();
		Texture2D friendly_bullet;
		SpriteFont ft_score;
		bool Reloaded = true;
		Random randpath = new Random();
		public int score = 0;
		public MainGame ()
		{
			graphics = new GraphicsDeviceManager (this); //Create a graphics device manager
			Content.RootDirectory = "Content"; //Where the content for our game is stored, generally its the Content folder. 
			graphics.IsFullScreen = false; //Set it to fullscreen. Not a good idea if your trying to debug it :P.
		}


		protected override void Initialize ()
		{
			// Anything that needs to be done before loading content must be done here.
			//In this case, we are going to loop through all the entites in a list to initalise them as well as creating ones that are nessacery.
			//TODO: Create Entites
			List<Point> path = new List<Point>();
			for (int i = 0; i < randpath.Next(0,400); i++) {
				path.Add(new Point(randpath.Next(0,400),randpath.Next(0,400)));
			}

			for (int i = 0; i < 10; i++) {
				targets.Add(new MovingTarget(this,5,path.ToArray(),5,1,0,10));
				targets[i].position = new Rectangle(300,(i * 5),100,100);
			}

			foreach (Entity ent in entities) { //Loop through every entity object in our entitys list.
				ent.Initialize();	//Call the initialize method on each entity.
			}

			base.Initialize (); //Leave this as it is.
				
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent ()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch (GraphicsDevice);
			Ship ship = new Ship(this,new Rectangle(200,450,30,30),Content.Load<Texture2D>("newell.png"));
			entities.Add(ship);
			//Add Targets
			for (int i = 0; i < targets.Count; i++) {
				targets[i].def_texture = Content.Load<Texture2D>("newell.png");
			}
			ft_score = Content.Load<SpriteFont>("fonts/ammo_count.xnb");
			friendly_bullet = Content.Load<Texture2D>("bullet.png");
			//TODO: use this.Content to load your game content here 
			entities.AddRange(targets);
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update (GameTime gameTime)
		{
			// For Mobile devices, this logic will close the Game when the Back button is pressed
			if (GamePad.GetState (PlayerIndex.One).Buttons.Back == ButtonState.Pressed) {
				Exit ();
			}

			for (int i = 0; i < entities.Count; i++) {
				entities [i].Movement ();
				entities [i].Update (gameTime);
			}

			foreach (Entity ent in entities_toremove) {
				entities.Remove(ent);	
			}
			bool fire_down = Keyboard.GetState().IsKeyDown(Keys.Space);

            if (!fire_down) 
			{
				Reloaded = true; 
			}

            if(Reloaded && fire_down)
			{
				entities.Add (new FriendlyBullet (this, new Rectangle (entities [0].position.X + 10, entities [0].position.Y, 5, 10), Color.Orange, friendly_bullet,5));
				Reloaded = false;
			}

			base.Update (gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw (GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear (Color.CornflowerBlue);
		
			//TODO: Add your drawing code here
			spriteBatch.Begin();
			foreach (Entity ent in entities) {
				ent.Draw(spriteBatch);
				spriteBatch.DrawString(ft_score,"Score:" + MakeScore(),new Vector2(20,20),Color.Red);

			}
			spriteBatch.End();
			base.Draw (gameTime);
		}

		private string MakeScore()
		{
			string str = "" + score;
			while (str.Length < 6) {
				str = "0" + str;
			}
			return str;
		}

	}
}

