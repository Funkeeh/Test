using UnityEngine;
using System.Collections;

public class TrainingManager : MonoBehaviour 
{
	//Stages of the Training Session
	public static bool 		startStimulation 	= false;
	public static bool		sendStimulation		= false;
	public static bool 		visualiseButtons	= false;
	public static int		level 				= 1;
	public static int 		rounds				= 0;
	public static int		currentRound 		= 1;
	public static int 		amountOfIterations 	= 0;
	public static float		waitTimer			= 2.0f;

	//Stim button variables
	public GameObject		stimButton;
	public GameObject		stimButtonM;
	public GameObject		handLogo;
	public GameObject		handLogoM;
	public AudioSource 		stimSound;
	public Material[]		stimButtonMats		= new Material[3];
	private Color 			handInitial; 
	private Color 			handGrayColor		= new Color ( 0.5f, 0.5f, 0.5f, 1.0f );

	//Input button variables
	public GameObject 		level01;
	public GameObject 		level02;
	public GameObject 		level03;

	//Game Mechanic Variables
	public static int	lastStimulatedValue	= 0;			//Used in TrainingButtonAnimator
	private int			amountOfButtons		= 0;			//Used for sequence 
	private int 		onlyTrueOnce 		= 0;			//So beginSequence bool is only set to true once
	private int			findStimOnce 		= 0;
	private int 		findRandomOnce		= 0;
	private int			sendStimOnce		= 0;
	private int			placeHolder			= 0;
	private bool		s_01_beginSequence	= false;		//Swhich between sequences
	private bool		s_02_visualiseBtn	= false;
	private bool 		s_03_restart 		= false;

	private float waitBeforeStim 		= 1.00f;			//Wait timers (amount of time)
	private float waitBeforeRestart 	= 1.00f;
	private float waitBeforeStimActual	= 0.0f;				//Wait timers (initial start time)
	private float stimulate				= 0.0f;
	private float showButtonClr			= 0.0f;
	private float restart				= 0.0f;

	private int[] ascending   = { 1, 2, 3, 4 };
	private int[] ascending2  = { 1, 2, 3, 4, 5 };
	private int[] ascending3  = { 1, 2, 3, 4, 5, 6 };
	private int[] descending  = { 4, 3, 2, 1 };
	private int[] descending2 = { 5, 4, 3, 2, 1 };
	private int[] descending3 = { 6, 5, 4, 3, 2, 1 };
	private int[] randoms 	= new int [4];
	private int[] randoms2  = new int [5];
	private int[] randoms3 	= new int [6];

	private int	counter = 0;
	private int counterMax = 0;			//Counts the around of rounds/iterations the training game do
	private int stimulatorStyle	= 0; 	//1 = ascending, 2 = descending, 3 = random;

	void Start () 
	{
		handInitial = handLogo.GetComponent<Renderer> ().material.color;
		ChooseLevelTraining (0);
		//rounds = 6;
		stimulatorStyle = 1;

		//Initialise random numbers
		randoms 	= FindFourDifferentRand (randoms[0], randoms[1], randoms[2], randoms[3]);
		randoms2 	= FindFiveDifferentRand (randoms2[0], randoms2[1], randoms2[2], randoms2[3], randoms2[4]);
		randoms3 	= FindSixDifferentRand 	(randoms3[0], randoms3[1], randoms3[2], randoms3[3], randoms3[4], randoms3[5]);
		findRandomOnce = 1;
	}

	void Update () 
	{
		Debug.Log ("level chosen is: " + level + ", rounds set are: " + rounds + ", and stimulator style is: " + stimulatorStyle);

		if (findRandomOnce == 0)
		{
			for (int i = findRandomOnce; i < 1; i++) 
			{
				if (level == 1 && counterMax == 4 || level == 1 && counterMax == 8 || level == 1 && counterMax == 12) 
				{
					randoms 	= FindFourDifferentRand (randoms[0], randoms[1], randoms[2], randoms[3]);
				}
				if (level == 2 && counterMax == 5 || level == 2 && counterMax == 10 || level == 2 && counterMax == 15) 
				{
					randoms2 	= FindFiveDifferentRand (randoms2[0], randoms2[1], randoms2[2], randoms2[3],  randoms2[4]);
				}
				if (level == 3 && counterMax == 6 || level == 3 && counterMax == 12 || level == 3 && counterMax == 18) 
				{
					randoms3 	= FindSixDifferentRand (randoms3[0], randoms3[1], randoms3[2], randoms3[3], randoms3[4], randoms3[5]);
				}
				findRandomOnce = 1;
			}
		}

		//Find a stim value based on stimstyle and level---------------------------------//
		for (int i = findStimOnce; i < 1; i++) 
		{
			if ( stimulatorStyle == 1 ) //Ascending
			{
				if ( level == 1 ) { lastStimulatedValue = ascending  [counter]; }
				if ( level == 2 ) { lastStimulatedValue = ascending2 [counter]; }
				if ( level == 3 ) { lastStimulatedValue = ascending3 [counter]; }
			}
			if ( stimulatorStyle == 2 ) //Descending
			{
				if ( level == 1 ) { lastStimulatedValue = descending  [counter]; }
				if ( level == 2 ) { lastStimulatedValue = descending2 [counter]; }
				if ( level == 3 ) { lastStimulatedValue = descending3 [counter]; }
			}
			if ( stimulatorStyle == 3 ) //Random
			{
				if ( level == 1 ) { lastStimulatedValue = randoms  [counter]; }
				if ( level == 2 ) { lastStimulatedValue = randoms2 [counter]; }
				if ( level == 3 ) { lastStimulatedValue = randoms3 [counter]; }
			}
			AssignValueToStim ();	//Assigns the correct value to the stimulator (hopefully)
			UiController.freqSendVariables();
		}
		findStimOnce++;

		//Round Manager ----------------------------------------------------------------------------//
		if (rounds!= 0)
		{
			if (rounds == counterMax) 
			{
				startStimulation = false;
			}
		}
			
		//Stage 0: Initialise Training Session (FROM UI)---------------------------------------------//
		if (startStimulation == false) 
		{
			if (Client.startstimulation == true) 
			{
				Client.startstimulation = false;  	//Stop stimulation
			}

			stimButton.GetComponent<Renderer> ().material 		= stimButtonMats[2];
			stimButtonM.GetComponent<Renderer> ().material 		= stimButtonMats[2];

			handLogo.GetComponent<Renderer> ().material.color 	= handGrayColor;
			handLogoM.GetComponent<Renderer> ().material.color 	= handGrayColor;

			visualiseButtons 	= false;
			visualiseButtons 	= false;
			s_01_beginSequence 	= false;
			s_02_visualiseBtn 	= false;
			s_03_restart 		= false;
			onlyTrueOnce 		= 0;
			sendStimOnce 		= 0;
			counter 			= 0;
			counterMax 			= 0;
			currentRound 		= 1;
			findStimOnce	= 0;
			findRandomOnce 	= 0;

			waitBeforeStimActual 	= 0.0f;
			stimulate 				= 0.0f;
			showButtonClr 			= 0.0f;
			restart 				= 0.0f;
		}
		else
		{
			stimButton.GetComponent<Renderer> ().material 		= stimButtonMats[0];
			stimButtonM.GetComponent<Renderer> ().material 		= stimButtonMats[0];

			handLogo.GetComponent<Renderer> ().material.color 	= handInitial;
			handLogoM.GetComponent<Renderer> ().material.color 	= handInitial;

			visualiseButtons = false;

			for (int i = onlyTrueOnce; i < 1; i++) 
			{
				s_01_beginSequence = true;
			}
		}

		//Stage 1: Stimulation------------//
		if ( s_01_beginSequence == true ) 
		{
			ChooseSequence (1);
			amountOfIterations = amountOfButtons;
		}
		//Stage 2: Visualise Button-------//
		if ( s_02_visualiseBtn == true )
		{
			ChooseSequence (2);
		}
		//Stage 3: Restart----------------//
		if ( s_03_restart == true )
		{
			ChooseSequence (3);
		}
	}

	//Manage Levels-----------------------------------------------------------------------------//
	public void ChooseLevelTraining (int levelChoice)
	{
		switch (levelChoice) 
		{
		case 0:		//Level 1: 4 Buttons
			amountOfButtons = 4;
			level	= 1;

			level01.SetActive (true);
			level02.SetActive (false);
			level03.SetActive (false);

			break;
		case 1:		//Level 2: 5 Buttons
			amountOfButtons = 5;
			level	= 2;

			level01.SetActive (false);
			level02.SetActive (true);
			level03.SetActive (false);
			break;
		case 2:		//Level 3: 6 Buttons
			amountOfButtons = 6;
			level	= 3;

			level01.SetActive (false);
			level02.SetActive (false);
			level03.SetActive (true);
			break;
		default:
			//Nothing
			break;
		}
		amountOfIterations = amountOfButtons;
	}

	//Manage Sequence (And black box potential)--------------------------------------------------//
	public void ChooseSequence (int seqChoice)
	{
		switch (seqChoice) 
		{
		case 1:		//Stimulate Sequence-----//
			s_03_restart = false;
			restart = 0.0f;
							
			//Add value to wait timer until it is 1
			if (waitBeforeStimActual < waitBeforeStim) 
			{
				waitBeforeStimActual += Time.deltaTime;
			}

			if (waitBeforeStimActual > waitBeforeStim) 
			{
				stimButton.GetComponent<Renderer> ().material 	= stimButtonMats[1];
				stimButtonM.GetComponent<Renderer> ().material 	= stimButtonMats[1];

				if (stimulate >= waitTimer) 
				{
					Client.startstimulation = false;  	//Stop stimulation
					stimSound.Stop ();

					s_02_visualiseBtn 	= true;
					s_01_beginSequence 	= false;
				}

				else if (stimulate < waitTimer) 
				{
					for (int i = sendStimOnce; i < 1; i++) 
					{
						Client.startstimulation = true;		//Start stimulation
					}
					sendStimOnce = 1;
					stimulate += Time.deltaTime;

					if (!stimSound.isPlaying) 
					{
						stimSound.Play ();
					}
				} 
			}
			break;
		case 2:		//Button Sequence---//
			stimButton.GetComponent<Renderer> ().material = stimButtonMats [2];
			stimButtonM.GetComponent<Renderer> ().material = stimButtonMats [2];

			handLogo.GetComponent<Renderer> ().material.color = handGrayColor;
			handLogoM.GetComponent<Renderer> ().material.color = handGrayColor;

			visualiseButtons = true;

			if (showButtonClr < waitTimer) 
			{
				showButtonClr += Time.deltaTime;
			} 
			else 
			{
				s_02_visualiseBtn 	= false;
				s_03_restart 		= true;
			}
			break;
		case 3:		//Reset Sequence---//
			
			visualiseButtons = false;
			s_01_beginSequence = true;
			s_02_visualiseBtn = false;
			s_03_restart = false;

			waitBeforeStimActual = 0.0f;
			stimulate = 0.0f;
			showButtonClr = 0.0f;
			restart = 0.0f;
			findStimOnce = 0;
			sendStimOnce = 0;

			if (level == 1 && counterMax == 3 || level == 1 && counterMax == 7 ||  level == 1 && counterMax == 11 ) 
			{
				findRandomOnce  = 0;
			}
			if (level == 2 && counterMax == 4 || level == 2 && counterMax == 9 ||  level == 2 && counterMax == 14 ) 
			{
				findRandomOnce  = 0;
			}
			if (level == 3 && counterMax == 5 || level == 3 && counterMax == 11 ||  level == 3 && counterMax == 17 ) 
			{
				findRandomOnce  = 0;
			}

			//Values for stimulator
			counter++;
			counterMax++;
			currentRound++;	//For UI
			if (counter == amountOfButtons ) 
			{
				counter = 0;
			}

			if (restart < waitBeforeRestart ) 
			{
				restart += Time.deltaTime;
			} 
			else 
			{
				goto case 1;	//Go back to step 1
			}
			break;
		default:
			//Nothing
			break;
		}
	}

	//Choose Stimulation Style
	public void ChooseInputStyle (int val)
	{
		switch (val) 
		{
		case 0:		//Ascending
			stimulatorStyle = 1;
			break;
		case 1:		//Descending
			stimulatorStyle = 2;
			break;
		case 2:		//Random
			stimulatorStyle = 3;
			break;
		}
	}

	//Used to change amount of time for stimulation within UI
	public void SetTimer (string rnd)
	{
		waitTimer = System.Int32.Parse (rnd);

		if (waitTimer == 0)
		{
			waitTimer = 0.5f;
		} 
	}

	//Used to change amount of rounds within UI
	public void SetRnds (string rnd)
	{
		int iterations = System.Int32.Parse (rnd);
		placeHolder = iterations;

		if (level == 1) 
		{
			rounds = iterations * 4;
		} 
		else if (level == 2) 
		{
			rounds = iterations * 5;
		} 
		else if (level == 3) 
		{
			rounds = iterations * 6;
		}
	}

	//Start/Stop Functions
	public void StartTraining ()
	{
		startStimulation = true;

		if (level == 1) 
		{
			rounds = placeHolder * 4;
		} 
		else if (level == 2) 
		{
			rounds = placeHolder * 5;
		} 
		else if (level == 3) 
		{
			rounds = placeHolder * 6;
		}
	}
	public void StopTraining ()
	{
		startStimulation = false;
	}

	//Send Stimulation Function---------------------------------------------------//
	public void AssignValueToStim ()
	{
		switch( lastStimulatedValue )
		{
		case 1: 
			Client.neuroFrequency = FreqUI.frequencies[0];
			//Client.sendStimulatorVariables = true;
			break;
		case 2: 
			Client.neuroFrequency = FreqUI.frequencies[1];
			//Client.sendStimulatorVariables = true;
			break;
		case 3: 
			Client.neuroFrequency = FreqUI.frequencies[2];
			//Client.sendStimulatorVariables = true;
			break;
		case 4: 
			Client.neuroFrequency = FreqUI.frequencies[3];
			//Client.sendStimulatorVariables = true;
			break;
		case 5: 
			Client.neuroFrequency = FreqUI.frequencies[4];
			//Client.sendStimulatorVariables = true;
			break;
		case 6: 
			Client.neuroFrequency = FreqUI.frequencies[5];
			//Client.sendStimulatorVariables = true;
			break;
		}
	}

	//--------------RANDOMIZER FUNCTIONS------------------------------------------------------------//
	//Randomize 6 different values/Locations 
	public static int[] FindSixDifferentRand (int one, int two, int three, int four, int five, int six)
	{
		//One
		one 		= Random.Range (1, 7);

		//Two
		do {
			two 	= Random.Range (1, 7);
		} while (two == one);

		//Three
		do {
			three 	= Random.Range (1, 7);
		} while (three == one || three == two);

		//Four
		do {
			four 	= Random.Range (1, 7);
		} while (four == one || four == two || four == three);

		//Five
		do {
			five 	= Random.Range (1, 7);
		} while (five == one || five == two || five == three || five == four);

		//Six
		do {
			six 	= Random.Range (1, 7);
		} while (six == one || six == two || six == three || six == four || six == five);

		Debug.Log ("Numbers are: " + one + " " + two + " " + three + " " + four + " " + five + " " + six);
		int[] rvalues = {one, two, three, four, five, six};

		return rvalues;
	} 

	//Randomize 5 different values/Locations 
	public static int[] FindFiveDifferentRand (int one, int two, int three, int four, int five)
	{
		//One
		one 		= Random.Range (1, 6);

		//Two
		do {
			two 	= Random.Range (1, 6);
		} while (two == one);

		//Three
		do {
			three 	= Random.Range (1, 6);
		} while (three == one || three == two);

		//Four
		do {
			four 	= Random.Range (1, 6);
		} while (four == one || four == two || four == three);

		//Five
		do {
			five 	= Random.Range (1, 6);
		} while (five == one || five == two || five == three || five == four);

		Debug.Log ("Numbers are: " + one + " " + two + " " + three + " " + four + " " + five);
		int[] rvalues = {one, two, three, four, five};

		return rvalues;
	} 

	//Randomize 4 different values/Locations 
	public static int[] FindFourDifferentRand (int one, int two, int three, int four)
	{
		//One
		one 		= Random.Range (1, 5);

		//Two
		do {
			two 	= Random.Range (1, 5);
		} while (two == one);

		//Three
		do {
			three 	= Random.Range (1, 5);
		} while (three == one || three == two);

		//Four
		do {
			four 	= Random.Range (1, 5);
		} while (four == one || four == two || four == three);

		Debug.Log ("Numbers are: " + one + " " + two + " " + three + " " + four);
		int[] rvalues = {one, two, three, four};

		return rvalues;
	} 
}