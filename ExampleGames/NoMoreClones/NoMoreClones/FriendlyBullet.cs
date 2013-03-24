using System;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
namespace NoMoreClones
{
	public class FriendlyBullet : Entity
	{
		Color BulletColour;
		public FriendlyBullet (MainGame parent,Rectangle pos, Color col,Texture2D tex,int damage) : base(parent,1,damage)
		{
			def_texture = tex;
			position = pos;
			BulletColour = col;

		}


		public override void Update (GameTime gameTime)
		{
			base.Update (gameTime);
			//Fire upwards fast
			velocity.Y -= 0.3f;
			if (position.Y < 0) {
				game.entities.Remove (this);
			}
			Entity collision = CheckCollision (game.entities.ToArray());
			if (collision != null && collision.GetType() != Type.GetType("NoMoreClones.Ship")) {
				collision.TakeDamage(this);
			}
		}

		public override void OnAttack ()
		{
			base.OnAttack ();
			game.entities_toremove.Add(this);
		}

		public override void Draw (SpriteBatch sb)
		{
			//base.Draw(sb); Since we are creating our own draw method, we comment this out.
			sb.Draw(def_texture,position,BulletColour);
		}
	}
}

