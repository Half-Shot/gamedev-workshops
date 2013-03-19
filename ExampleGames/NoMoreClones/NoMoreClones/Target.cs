using System;
using Microsoft.Xna.Framework;
namespace NoMoreClones
{
	public class MovingTarget : Entity
	{
		int current_pos;
		Point[] nav;
		int move_speed;
		public MovingTarget (MainGame parent,int Health, Point[] path, int startat = 0, int speed = 1,int damage = 0, int worth = 0) : base(parent,Health,0,10)
		{
			current_pos = startat;
			nav = path;
			move_speed = speed;
		}

		public override void Update (GameTime gameTime)
		{
			Point curpos = new Point (position.X, position.Y);

			if (curpos == nav [current_pos]) {
				current_pos++;
				if (current_pos > nav.Length - 1) {
					current_pos = 0;
				}
			}

			if (curpos.X > nav [current_pos].X) {
				curpos.X -= 1 * move_speed;
			}

			if (curpos.Y > nav [current_pos].Y) {
				curpos.Y -= 1 * move_speed;
			}

			if (curpos.X < nav [current_pos].X) {
				curpos.X += 1 * move_speed;
			}

			if (curpos.Y < nav [current_pos].Y) {
				curpos.Y += 1 * move_speed;
			}

			position.X = curpos.X;
			position.Y = curpos.Y;


			base.Update (gameTime);
		}
	}

	public class Target:Entity
	{
		public Target (MainGame parent,int Health) : base(parent,Health)
		{
		}
	}

	public class OrbitTarget : Entity
	{
			Vector2 Mvment = Vector2.Zero;
			Point Orbit;
			int Radius;
			int Speed;
		public OrbitTarget (MainGame parent,int Health, Point orbit, int radius, int speed) : base(parent,Health)
		{
			Orbit = orbit;
			Radius = radius;
			Speed = speed;
		}

		public override void Movement ()
		{

			//Is X more than radius
	
			//base.Movement ();
		}
	}


}

