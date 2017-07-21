using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Diagnostics;

//Used for handling opening and exiting programs
public class QuitGame : MonoBehaviour 
{
	private Process myProcess;


	void Awake () 
	{
		//Place Server in Game Folder and this works
		myProcess = Process.Start( Application.dataPath + "\\Server2\\bin\\Release\\Server2.exe");
		//myProcess = Process.Start( "C:\\Users\\Ronni\\Desktop\\FullGame_Data\\Server2\\bin\\Release\\Server2.exe" );

		//myProcess = Process.Start(Application.dataPath + "/Server/Server.exe");
	}

	public void Quit ()
	{
		myProcess.Kill ();
		Application.Quit ();
	}
}
