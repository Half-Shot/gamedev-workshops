using System;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
namespace NoMoreClones
{
	/// <summary>
	/// A base class that allows rapid development of ingame objects.
	/// Built in functions include drawing, velocity, health, damage and collision.
	/// </summary>
	public class Entity
	{
		/// <summary>
		/// Gets or sets the def_texture.
		/// </summary>
		/// <value>
		/// The default texture of the entity..
		/// </value>
		public Texture2D def_texture {
			get;
			set;
		}
		/// <summary>
		/// The friction
		/// </summary>
		const float friction = 0.1f; 
		public Rectangle position;
		public Vector2 velocity = Vector2.Zero;
		public string name;
		public int health = 1;
		public int damage = 0;
		public MainGame game;
		public int score_worth;
		public Entity(MainGame parent,int hlth = 1, int damage = 0, int score_worth = 0)
		{
			game = parent;
			this.health = health;
			this.damage = damage;
			this.score_worth = score_worth;
		}

		public virtual void Initialize ()
		{

		}

		public virtual void Update(GameTime gameTime)
		{

		}

		public virtual void Movement ()
		{
			if (velocity.X != 0) {
				position.X += (int)Math.Floor (velocity.X);
			}
			if (velocity.Y != 0) {
				position.Y += (int)Math.Floor (velocity.Y);
			}

			if (velocity.X > 0.1) {
				velocity.X -= friction;
			}

			if (velocity.X < -0.1) {
				velocity.X += friction;
			}

			
			//Safety precautions to reset value to zero if it falls between the 0-0.1 gap.
			if (velocity.X < 0.1 && velocity.X > -0.1) {
				velocity.X  = 0;
			}
			if (velocity.Y < 0.1 && velocity.Y > -0.1) {
				velocity.Y  = 0;
			}
			//This is space invaders
			if (velocity.Y > 0.1) {
				velocity.Y -= friction;
			}

			if (velocity.Y < -0.1) {
				velocity.Y += friction;
			}

		}

		public virtual void Draw(SpriteBatch sb)
		{
			sb.Draw(def_texture,position,Color.White);
		}

		public Entity CheckCollision(Entity[] otherEnts)
		{
			foreach (Entity ent in otherEnts) {
			if(this == ent){ continue; } //Don't want to check against the same entity XD.
				if(ent.position.Intersects(this.position))
				{ 
					return ent;
				}
			}

			return null;
		}

		/// <summary>
		/// Take damage.
		/// </summary>
		/// <returns>
		/// The damage recieved.
		/// </returns>
		public int TakeDamage (Entity inflictor)
		{
			health -= inflictor.damage;
			if (this.health < 1) {
				DieAHorribleDeath(inflictor);
			}
			inflictor.OnAttack();
			return inflictor.damage;
		}

		public int TakeDamage (int damage)
		{
			health -= damage;
			if (this.health < 1) {
				DieAHorribleDeath ();
			}
			return damage;
		}

		public virtual void OnAttack()
		{

		}

		public void DieAHorribleDeath(Entity murderer = null) //Aka Kill (Sam)
		{
			game.entities_toremove.Add (this);
			if (murderer == null) { return; }
			string name = murderer.GetType ().FullName;
			if (name == "NoMoreClones.FriendlyBullet") {
				game.score += score_worth;
			}
		}

	}
}

