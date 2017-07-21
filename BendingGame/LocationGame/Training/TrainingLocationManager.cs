using UnityEngine;
using System.Collections;
using System;

public class TrainingLocationManager : MonoBehaviour 
{
	//Stages of the Training Session
	public static bool 		startStimulation 	= false;
	public static bool 		currentlyStim 		= false;
	public static bool		sendStimulation		= false;
	public static bool 		visualiseButtons	= false;
	public static bool		handModebool		= true;
	public static int		level 				= 1;
	public static int 		rounds				= 0;
	public static int 		currentRound 		= 1;
	public static float		waitTimer			= 2.0f;
	public static bool 		newIteration 		= false;
	public static int temp = 0;

	public static int 	iterations = 0;

	public AudioSource 		stimSound;

	//HandModes
	public GameObject 	hand;
	public GameObject	handM;
	public GameObject	noHand;
	public GameObject 	noHandM;

	//Game Mechanic Variables
	public static int	lastStimuValue		= 0;			//Used in TrainingLocationHands
	public static int 	secondStimValue 	= 0;		
	private int			amountOfButtons		= 6;			//Used for sequence 
	private int 		onlyTrueOnce 		= 0;			//So beginSequence bool is only set to true once
	private int			findStimOnce 		= 0;
	private int 		findRandomOnce		= 0;
	private int			sendStimOnce		= 0;
	private bool		s_01_beginSequence	= false;		//Swhich between sequences
	private bool		s_02_visualiseBtn	= false;
	private bool 		s_03_restart 		= false;

	private float waitBeforeStim 		= 1.00f;			//Wait timers (amount of time)
	private float waitBeforeRestart 	= 1.00f;
	private float waitBeforeStimActual	= 0.0f;				//Wait timers (initial start time)
	private float stimulate				= 0.0f;
	private float showButtonClr			= 0.0f;
	private float restart				= 0.0f;

	private int[] ascending3  = { 1, 2, 3, 4, 5, 6 };
	private int[] descending3 = { 6, 5, 4, 3, 2, 1 };

	private int[] random 		= new int[6];
	private int[] randLvl201	= { 1, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 3, 4, 4, 5 };
	private int[] randLvl202	= { 2, 3, 4, 5, 6, 3, 4, 5, 6, 4, 5, 6, 5, 6, 6 };
	private int[] randLvl2 		= new int[15];

	private int[] descend01 	= { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2, 1, 1, 1, 1, 1 };
	private int[] descend02 	= { 6, 6, 5, 6, 5, 4, 6, 5, 4, 3, 6, 5, 4, 3, 2 };

	private int	counter  = 0;			//What values to assign buttonpressedone and buttonpressedtwo
	private int counter2 = 1;
	private int counterLvl2 = 0;
	private int counterMax = 0;
	private int stimulatorStyle	= 0; 	//1 = ascending, 2 = descending, 3 = random;

	void Start () 
	{
		ChooseLevelTraining (0);
		rounds = 6;
		stimulatorStyle = 1;
		handModebool = true;

		//Assign randoms
		random   = FindSixDifferentRand (random[0],   random[1],   random[2],   random[3],    random[4],    random[5]);
		randLvl2 = Find15DifferentRand 	(randLvl2[0], randLvl2[1], randLvl2[2], randLvl2[3],  randLvl2[4],  randLvl2[5],  randLvl2[6], 
										 randLvl2[7], randLvl2[8], randLvl2[9], randLvl2[10], randLvl2[11], randLvl2[12], randLvl2[13], randLvl2[14] );
		findRandomOnce = 1;
	}

	void Update () 
	{
		Debug.Log ("level chosen is: " + level + ", rounds set are: " + rounds + ", and stimulator style is: " + stimulatorStyle);
		Debug.Log ("Counter " + counter);

		//Find a stim value based on stimstyle and level---------------------------------//
		for (int i = findStimOnce; i < 1; i++) 
		{
			if ( stimulatorStyle == 1 ) //Ascending
			{
				if ( level == 1 ) { lastStimuValue = ascending3  [counter]; }
				if ( level == 2 ) 
				{ 
					lastStimuValue  = randLvl201 [ counterLvl2 ]; 
					secondStimValue = randLvl202 [ counterLvl2 ]; 
				}
			}
			if ( stimulatorStyle == 2 ) //Descending
			{
				if ( level == 1 ) { lastStimuValue = descending3  [counter]; }
				if ( level == 2 ) 
				{ 
					lastStimuValue  = descend01 [ counterLvl2 ]; 
					secondStimValue = descend02 [ counterLvl2 ]; 
				}
			}
			if ( stimulatorStyle == 3 ) //Random
			{
				if ( level == 1 ) { lastStimuValue = random  [counter]; }
				if ( level == 2 ) 
				{ 
					temp = randLvl2 [ counterLvl2 ] - 1; 
					lastStimuValue  = randLvl201[ temp ];
					secondStimValue = randLvl202[ temp ];
				}
				

			}
			AssignValueToStim ();	//Assigns the correct value to the stimulator (hopefully)
		}

		//Toggles handmode----------------------------------------------------//
		if (handModebool == true) 
		{
			hand.SetActive (true);
			handM.SetActive (true);
			noHand.SetActive (false);
			//noHandM.SetActive (false);
		} 
		else 
		{
			hand.SetActive (false);
			handM.SetActive (false);
			noHand.SetActive (true);
			//noHandM.SetActive (true);
		}

		//Round Manager -----------------------------------------------------------------------------//
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

			sendStimulation		= false;
			currentlyStim 		= false;
			visualiseButtons 	= false;
			s_01_beginSequence 	= false;
			s_02_visualiseBtn 	= false;
			s_03_restart 		= false;
			onlyTrueOnce 		= 0;
			sendStimOnce 		= 0;
			counter 			= 0;
			counter2 			= 1;
			counterLvl2 		= 0;
			counterMax 			= 0;
			currentRound 		= 1;
			findRandomOnce 		= 0;

			waitBeforeStimActual 	= 0.0f;
			stimulate 				= 0.0f;
			showButtonClr 			= 0.0f;
			restart 				= 0.0f;
		}
		else
		{
			//Initialise Randoms
			if (findRandomOnce == 0) 
			{
				randLvl2 = Find15DifferentRand 	(randLvl2[0], randLvl2[1], randLvl2[2], randLvl2[3],  randLvl2[4],  randLvl2[5],  randLvl2[6], 
											     randLvl2[7], randLvl2[8], randLvl2[9], randLvl2[10], randLvl2[11], randLvl2[12], randLvl2[13], randLvl2[14] );
				findRandomOnce = 1;
			}
			if (newIteration == true) 
			{
				randLvl2 = Find15DifferentRand 	(randLvl2[0], randLvl2[1], randLvl2[2], randLvl2[3],  randLvl2[4],  randLvl2[5],  randLvl2[6], 
												 randLvl2[7], randLvl2[8], randLvl2[9], randLvl2[10], randLvl2[11], randLvl2[12], randLvl2[13], randLvl2[14] );
				newIteration = false;
			}

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

	//Manage Sequence (And black box potential)--------------------------------------------------//
	public void ChooseSequence (int seqChoice)
	{
		switch (seqChoice) 
		{
		case 1:		//Stimulate Sequence-----//
			s_03_restart = false;
			restart = 0.0f;
			sendStimulation = true;

			if (handModebool == false) 
			{
				noHandM.SetActive ( false );
			}

			//Add value to wait timer until it is 1
			if (waitBeforeStimActual < waitBeforeStim) 
			{
				waitBeforeStimActual += Time.deltaTime;
			}

			if (waitBeforeStimActual > waitBeforeStim) 
			{
				if (stimulate >= waitTimer) 
				{
					currentlyStim = false;
					Client.startstimulation = false;  	//Stop stimulation
					stimSound.Stop ();

					s_02_visualiseBtn 	= true;
					s_01_beginSequence 	= false;
				}

				else if (stimulate < waitTimer) 
				{
					currentlyStim = true;

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
			visualiseButtons 	= true;
			sendStimulation 	= false;

			if (handModebool == false) 
			{
				//noHandM.SetActive ( true );
			}

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
			//findRandomOnce 		= 0;
			sendStimOnce = 0;

			if (level == 1 && counter == 5) 
			{
				newIteration = true;
			} 
			else if (level == 2 && counterLvl2 == 14) 
			{
				newIteration = true;
			}

			//Values for stimulator
			counter++;
			counter2++;
			counterMax++;
			counterLvl2++;
			currentRound++; //For UI
			if ( counter == amountOfButtons ) 
			{
				counter = 0;
			}
			if ( counter2 == amountOfButtons ) 
			{
				counter2 = 0;
			}
			if ( counterLvl2 == 15 )
			{
				counterLvl2 = 0;
			}

			if ( restart < waitBeforeRestart ) 
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

	//Manage Levels-----------------------------------------------------------------------------//
	public void ChooseLevelTraining (int levelChoice)
	{
		switch (levelChoice) 
		{
		case 0:		//Level 1: 1 stim
			level	= 1;
			break;
		case 1:		//Level 2: 2 stims
			level	= 2;
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
		iterations = System.Int32.Parse (rnd);

		if (level == 1) 
		{
			rounds = iterations * 6;
		} 
		else if (level == 2) 
		{
			rounds = iterations * 15;
		} 
	}

	//Start/Stop Functions
	public void StartTraining ()
	{
		if (level == 1) 
		{
			rounds = iterations * 6;
		} 
		else if (level == 2) 
		{
			rounds = iterations * 15;
		} 

		startStimulation = true;
	}
	public void StopTraining ()
	{
		startStimulation = false;
	}

	public void HandMode (bool toggle)
	{
		handModebool = toggle;
	}

	//Send Stimulation Function--------------------------------------------------------------//
	public void AssignValueToStim ()
	{
		//If only one channel is used
		if ( level == 1 ) 
		{
			Client.neuroMultichannels = false;

			switch ( lastStimuValue ) 
			{
			case 1: 
				Client.neuroChannel = 1;
				Client.neuroCurrent = UiController.currentArray [0];
				Client.neuroPulseWidth = (int)(uint) UiController.pulsewidthArray [0];
				Client.sendStimulatorVariables = true;
				break;
			case 2: 
				Client.neuroChannel = 2;
				Client.neuroCurrent = UiController.currentArray [1];
				Client.neuroPulseWidth = (int)(uint) UiController.pulsewidthArray [1];
				Client.sendStimulatorVariables = true;
				break;
			case 3: 
				Client.neuroChannel = 3;
				Client.neuroCurrent = UiController.currentArray [2];
				Client.neuroPulseWidth = (int)(uint) UiController.pulsewidthArray [2];
				Client.sendStimulatorVariables = true;
				break;
			case 4: 
				Client.neuroChannel = 4;
				Client.neuroCurrent = UiController.currentArray [3];
				Client.neuroPulseWidth = (int)(uint) UiController.pulsewidthArray [3];
				Client.sendStimulatorVariables = true;
				break;
			case 5: 
				Client.neuroChannel = 5;
				Client.neuroCurrent = UiController.currentArray [4];
				Client.neuroPulseWidth = (int)(uint) UiController.pulsewidthArray [4];
				Client.sendStimulatorVariables = true;
				break;
			case 6: 
				Client.neuroChannel = 6;
				Client.neuroCurrent = UiController.currentArray [5];
				Client.neuroPulseWidth = (int)(uint) UiController.pulsewidthArray [5];
				Client.sendStimulatorVariables = true;
				break;
			}
		} 
		//If multiple channels are used (level 2 or 3)
		else if ( level == 2) 
		{
			Client.neuroMultichannels = true;

			uint[] clearOrder = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
			UiController.channelOrderArray = clearOrder;

			int[] firstTwoValues 	= new int[2];

			if ( stimulatorStyle == 3) 
			{
				firstTwoValues [0] 		= randLvl201[ temp ];
				firstTwoValues [1] 		= randLvl202[ temp ];
			} 
			else if (stimulatorStyle == 2) 
			{
				firstTwoValues [0] 		= descend01[ counterLvl2 ];
				firstTwoValues [1] 		= descend02[ counterLvl2 ];
			}
			else 
			{
				firstTwoValues [0] 		= randLvl201[ counterLvl2 ];
				firstTwoValues [1] 		= randLvl202[ counterLvl2 ];
			}


			Array.Sort (firstTwoValues);

			uint[] order = new uint[2];
			double[] currents = new double[2];
			uint[] pulsewidth = new uint[2];

			order [0] = (uint)(int)firstTwoValues [0];
			order [1] = (uint)(int)firstTwoValues [1];
			Client.multichannel_order = order;

			currents [0] = UiController.currentArray [firstTwoValues[0]-1];
			currents [1] = UiController.currentArray [firstTwoValues[1]-1];
			Client.multichannel_currents = currents;

			pulsewidth [0] = UiController.pulsewidthArray [firstTwoValues [0]-1];
			pulsewidth [1] = UiController.pulsewidthArray [firstTwoValues [1]-1];
			Client.multichannel_pulseWdt = pulsewidth;
			Client.multichannel_number = 2;

			Client.sendStimulatorVariables = true;
		}
	}

	//--------------RANDOMIZER FUNCTIONS------------------------------------------------------------//
	//Randomize 6 different values/Locations 
	public static int[] FindSixDifferentRand (int one, int two, int three, int four, int five, int six)
	{
		//One
		one 		= UnityEngine.Random.Range (1, 7);

		//Two
		do {
			two 	= UnityEngine.Random.Range (1, 7);
		} while (two == one);

		//Three
		do {
			three 	= UnityEngine.Random.Range (1, 7);
		} while (three == one || three == two);

		//Four
		do {
			four 	= UnityEngine.Random.Range (1, 7);
		} while (four == one || four == two || four == three);

		//Five
		do {
			five 	= UnityEngine.Random.Range (1, 7);
		} while (five == one || five == two || five == three || five == four);

		//Six
		do {
			six 	= UnityEngine.Random.Range (1, 7);
		} while (six == one || six == two || six == three || six == four || six == five);

		Debug.Log ("Numbers are: " + one + " " + two + " " + three + " " + four + " " + five + " " + six);
		int[] rvalues = {one, two, three, four, five, six};

		return rvalues;
	} 

	//Randomize 15 different values/Locations 
	public static int[] Find15DifferentRand ( int one, int two, int three, int four, int five, int six, int seven, int eight, int nine, int ten, int eleven, int twelve, int thirteen, int fourteen, int fifteen )
	{
		//One
		one 		= UnityEngine.Random.Range (1, 16);

		//Two
		do {
			two 	= UnityEngine.Random.Range (1, 16);
		} while (two == one);

		//Three
		do {
			three 	= UnityEngine.Random.Range (1, 16);
		} while (three == one || three == two);

		//Four
		do {
			four 	= UnityEngine.Random.Range (1, 16);
		} while (four == one || four == two || four == three);

		//Five
		do {
			five 	= UnityEngine.Random.Range (1, 16);
		} while (five == one || five == two || five == three || five == four);

		//Six
		do {
			six 	= UnityEngine.Random.Range (1, 16);
		} while (six == one || six == two || six == three || six == four || six == five);

		//Seven
		do {
			seven 	= UnityEngine.Random.Range (1, 16);
		} while ( seven == one || seven == two || seven == three || seven == four || seven == five || seven == six );

		//Eight
		do {
			eight 	= UnityEngine.Random.Range (1, 16);
		} while ( eight == one || eight == two || eight == three || eight == four || eight == five || eight == six || eight == seven );

		//Nine
		do {
			nine 	= UnityEngine.Random.Range (1, 16);
		} while ( nine == one || nine == two || nine == three || nine == four || nine == five || nine == six || nine == seven || nine == eight );

		//Ten
		do {
			ten 	= UnityEngine.Random.Range (1, 16);
		} while ( ten == one || ten == two || ten == three || ten == four || ten == five || ten == six || 
			ten == seven || ten == eight || ten == nine );

		//Eleven
		do {
			eleven 	= UnityEngine.Random.Range (1, 16);
		} while ( eleven == one || eleven == two || eleven == three || eleven == four || eleven == five || eleven == six || 
			eleven == seven || eleven == eight || eleven == nine || eleven == ten );
		
		//Twelve
		do {
			twelve 	= UnityEngine.Random.Range (1, 16);
		} while ( twelve == one || twelve == two || twelve == three || twelve == four || twelve == five || twelve == six || 
			twelve == seven || twelve == eight || twelve == nine || twelve == ten || twelve == eleven );
		
		//Thirteen
		do {
			thirteen 	= UnityEngine.Random.Range (1, 16);
		} while ( thirteen == one || thirteen == two || thirteen == three || thirteen == four || thirteen == five || thirteen == six || 
			thirteen == seven || thirteen == eight || thirteen == nine || thirteen == ten || thirteen == eleven || thirteen == twelve );

		//Fourteen
		do {
			fourteen 	= UnityEngine.Random.Range (1, 16);
		} while ( fourteen == one || fourteen == two || fourteen == three || fourteen == four || fourteen == five || fourteen == six || 
			fourteen == seven || fourteen == eight || fourteen == nine || fourteen == ten || fourteen == eleven || fourteen == twelve ||
			fourteen == thirteen );

		//Fifteen
		do {
			fifteen 	= UnityEngine.Random.Range (1, 16);
		} while ( fifteen == one || fifteen == two || fifteen == three || fifteen == four || fifteen == five || fifteen == six || 
			fifteen == seven || fifteen == eight || fifteen == nine || fifteen == ten || fifteen == eleven || fifteen == twelve ||
			fifteen == thirteen || fifteen == fourteen );


		Debug.Log ("Numbers are: " + one + " " + two + " " + three + " " + four + " " + five + " " + six + " " + seven + " " + eight + " "
			+ nine + " " + ten + " " + eleven + " " + twelve + " " + thirteen + " " + fourteen + " " + fifteen );
		int[] rvalues = { one, two, three, four, five, six, seven, eight, nine, ten, eleven, twelve, thirteen, fourteen, fifteen };

		return rvalues;
	} 
}
