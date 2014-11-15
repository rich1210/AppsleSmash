using UnityEngine;
using System.Collections;

public class gameController : MonoBehaviour
{
		public GameObject Player;
		public GameObject Start_Menu;
		public GameObject Pause_Menu;
		public GameObject Settings_Menu;
		public GameObject inGameUI;
		public GameObject Game_Over_Screen;
		public GameObject Slider;
		GameObject currentPanel;
		PlayerMovement  PlayerMovementScript;
		
		void Start ()
		{
				PlayerMovementScript = Player.GetComponent<PlayerMovement> ();
				init ();
		}
		public void init ()
		{
				PlayerMovementScript.pause ();
				showPanel (Start_Menu);
				hidePanel (Pause_Menu);
				hidePanel (Settings_Menu);
				hidePanel (inGameUI);
				hidePanel (Game_Over_Screen);	
		}
		public void playGame ()
		{
				PlayerMovementScript.unPause ();
				hidePanel (Start_Menu);
				hidePanel (Pause_Menu);
				hidePanel (Settings_Menu);
				showPanel (inGameUI);
				hidePanel (Game_Over_Screen);			
		}
		public void pauseGame ()
		{
				PlayerMovementScript.pause ();
				hidePanel (Start_Menu);
				showPanel (Pause_Menu);
				hidePanel (Settings_Menu);
				hidePanel (inGameUI);
				hidePanel (Game_Over_Screen);		
		}
		public void openSettings (GameObject newCurrentPanel)
		{
				currentPanel = newCurrentPanel;
				PlayerMovementScript.pause ();
				hidePanel (Start_Menu);
				hidePanel (Pause_Menu);
				showPanel (Settings_Menu);
				hidePanel (inGameUI);
				hidePanel (Game_Over_Screen);			
		}
		public void closeSettings ()
		{
				if (currentPanel == Start_Menu) {
						init ();
				} else if (currentPanel == Pause_Menu) {
						pauseGame ();
				} else if (currentPanel == Game_Over_Screen) {
						endGame ();
				}
		}
		public void changeSpeed (float newSpeed)
		{
				PlayerMovementScript.defaultSpeed = newSpeed;
		}
		public void endGame ()
		{
				PlayerMovementScript.pause ();
				hidePanel (Start_Menu);
				hidePanel (Pause_Menu);
				hidePanel (Settings_Menu);
				hidePanel (inGameUI);
				showPanel (Game_Over_Screen);		
		}

		public void hidePanel (GameObject Panel)
		{
				Panel.SetActive (false);
		}
		public void showPanel (GameObject Panel)
		{
				Panel.SetActive (true);
		}
		public void turnRight ()
		{
				PlayerMovementScript.rightTurn = true;
		}
		public void turnLeft ()
		{
				PlayerMovementScript.leftTurn = true;
		}
		public void restart ()
		{
				PlayerMovementScript.reset ();
				init ();
		}
		public void quit ()
		{
				Application.Quit (); 
		}
		public void setScore (int score)
		{
	
		}

}
