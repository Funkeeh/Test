using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class OpenServer : MonoBehaviour 
{
	void Awake () 
	{
		//Place Server in Game Folder and this works
		//Process.Start(Application.dataPath + "\\Server2\\bin\\release\\Server2.exe");
		//Process.Start("C:\\Users\\Ronni\\Desktop\\FullGame_Data\\Server\\Server.exe");
	}
}
