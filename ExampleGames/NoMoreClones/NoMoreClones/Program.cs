#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
#endregion

//Here we have the namespace. It basically means the category the class you are working on is in.
//In this case it is the 'NoMoreClones' area, which means the root of the project.
namespace NoMoreClones
{
	static class Program //This is a simple class that is run when we load up the game. Its designed to load up our game and then dissapear.
	//For this reason, we don't do much in this area.
	{
		private static MainGame game; //Tells C# we need a variable to hold the MainGame object and call it game. The private part means that no other class
									  //can access it. We will discuss static later.

		[STAThread] //Not important, just telling C# your using a single thread.
		static void Main () //This is important, when you run your application. This method is looked at first before anything else.
							//If it was blank the program would do nothing.
		{
			game = new MainGame(); //Different from creating the class. This is the part where the class is acutally made so variables are assigned in here.
								   //In this example, the game variable (of the type MainGame) is being assigned to as a new MainGame.
			game.Run(); //Finally, call the method to run the game. Please now focus your attention to the MainGame class (inside MainGame.cs)
		}
	}
}
