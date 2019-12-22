using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

	public enum Menu
  {
		MainMenu,
		NewGame,
		Continue
	}

	public Menu currentMenu;

  void OnGUI ()
  {

		GUILayout.BeginArea(new Rect(0,0,Screen.width, Screen.height));
		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		GUILayout.BeginVertical();
		GUILayout.FlexibleSpace();

		if(currentMenu == Menu.MainMenu)
    {

			GUILayout.Box("The Game");
			GUILayout.Space(10);

			if(GUILayout.Button("New Game"))
      {
				Game.current = new Game();
				currentMenu = Menu.NewGame;
			}

			if(GUILayout.Button("Continue"))
      {
				SaveLoad.Load();
				currentMenu = Menu.Continue;
			}

			if(GUILayout.Button("Quit Game"))
      {
				Application.Quit();
			}
		}

		else if (currentMenu == Menu.NewGame)
    {

			GUILayout.Box("Name Your Character");
			GUILayout.Space(10);

			if(GUILayout.Button("Save"))
      {
				//Save the current Game as a new saved Game
				SaveLoad.Save();
				//Move on to game...
				SceneManager.LoadScene(1);
			}

			GUILayout.Space(10);
			if(GUILayout.Button("Cancel"))
      {
				currentMenu = Menu.MainMenu;
			}

		}

		else if (currentMenu == Menu.Continue)
    {

			GUILayout.Box("Select Save File");
			GUILayout.Space(10);

			foreach(Game g in SaveLoad.savedGames)
      {
				if(GUILayout.Button("HP: " + g.currentPlayerData.hp + " - Disease: " + g.currentPlayerData.disease))
        {
					Game.current = g;
					//Move on to game...
					SceneManager.LoadScene(1);
				}

			}

			GUILayout.Space(10);
			if(GUILayout.Button("Cancel"))
      {
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
