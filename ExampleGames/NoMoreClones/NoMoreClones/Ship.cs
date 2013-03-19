using System;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace NoMoreClones
{
	public class Ship : Entity
	{
		const float player_speed = 0.2f;
		public Ship (MainGame parent,Rectangle position, Texture2D ship) : base(parent)
		{
			this.position = position;
			def_texture = ship;
			velocity.X = 0;
		}

		public override void Movement ()
		{
			base.Movement ();
			if (position.X < 20) {
				position.X = 21;
				//velocity.X = -velocity.X;
			}
			if (position.X > 780 - position.Width ) {
				position.X = 779 - position.Width;
				//velocity.X = -velocity.X ;
			}
		}

		public override void Update (GameTime gameTime)
		{
			//Movement controls
			if (Keyboard.GetState ().IsKeyDown (Keys.A)) { velocity.X -= player_speed; }
			if (Keyboard.GetState ().IsKeyDown (Keys.D)) { velocity.X += player_speed; }
			base.Update (gameTime);
		}
		public override void Draw (SpriteBatch sb)
		{
			base.Draw (sb);
		}
	}
}