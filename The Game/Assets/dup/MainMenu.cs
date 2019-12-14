using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public enum Menu {
		MainMenu,
		NewGame,
		Continue
	}

	public Menu currentMenu;

	void OnGUI () {

		GUILayout.BeginArea(new Rect(0,0,Screen.width, Screen.height));
		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		GUILayout.BeginVertical();
		GUILayout.FlexibleSpace();

		if(currentMenu == Menu.MainMenu) {

			GUILayout.Box("Last Fantasy");
			GUILayout.Space(10);

			if(GUILayout.Button("New Game")) {
				Game1.current = new Game1();
				currentMenu = Menu.NewGame;
			}

			if(GUILayout.Button("Continue")) {
				SaveLoad1.Load();
				currentMenu = Menu.Continue;
			}

			if(GUILayout.Button("Quit")) {
				Application.Quit();
			}
		}

		else if (currentMenu == Menu.NewGame) {

			GUILayout.Box("Name Your Characters");
			GUILayout.Space(10);

			GUILayout.Label("Knight");
			Game1.current.knight.name = GUILayout.TextField(Game1.current.knight.name, 20);
			GUILayout.Label("Rogue");
			Game1.current.rogue.name = GUILayout.TextField(Game1.current.rogue.name, 20);
			GUILayout.Label("Wizard");
			Game1.current.wizard.name = GUILayout.TextField(Game1.current.wizard.name, 20);

			if(GUILayout.Button("Save")) {
				//Save the current Game as a new saved Game
				SaveLoad1.Save();
				//Move on to game...
				Application.LoadLevel(1);
			}

			GUILayout.Space(10);
			if(GUILayout.Button("Cancel")) {
				currentMenu = Menu.MainMenu;
			}

		}

		else if (currentMenu == Menu.Continue) {

			GUILayout.Box("Select Save File");
			GUILayout.Space(10);

			foreach(Game1 g in SaveLoad1.savedGames) {
				if(GUILayout.Button(g.knight.name + " - " + g.rogue.name + " - " + g.wizard.name)) {
					Game1.current = g;
					//Move on to game...
					Application.LoadLevel(1);
				}

			}

			GUILayout.Space(10);
			if(GUILayout.Button("Cancel")) {
				currentMenu = Menu.MainMenu;
			}

		}

		GUILayout.FlexibleSpace();
		GUILayout.EndVertical();
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();
		GUILayout.EndArea();

	}
}
