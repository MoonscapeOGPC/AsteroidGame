using UnityEngine;
using System.Collections;
using System.IO.Ports;

public class SerialCommunicator : MonoBehaviour {

	/**
	 * COM port strings to be used by the SerialPorts.
	 * To be set in the Unity Settings before the game is run.
	 */
	public string gameOne = "/dev/tty.usbmodem1421";
	public string gameTwo = "COM2";

	/**
	 * The serial port used to access with the GameOne arduino.
	 */
	SerialPort gameOneSerial;
	/**
	 * Same as above but for GameTwo.
	 */
	SerialPort gameTwoSerial;

	/**
	 * False if GameOne is currently running. True otherwise.
	 */
	public bool gameOneDone;
	/**
	 * False if GameTwo is currently running. True otherwise.
	 */
	public bool gameTwoDone;

	/**
	 * The health component used to damage the player
	 * if the player messes up minigames.
	 */
	PlayerHealth playerHealth;

	/**
	 * Gets the GameOne arduino ready to go.
	 */
	public void initGameOne(){
		gameOneSerial.Open ();
		if (gameOneSerial.ReadByte () == 'b')
			gameOneSerial.Write ("r");
	}

	public void initGameTwo(){
		gameTwoSerial.Open ();
		if (gameTwoSerial.ReadByte () == 'b')
			gameTwoSerial.Write ("r");
	}

	/**
	 * Sends data to the GameOne arduino to
	 * kick off the minigame.
	 */
	public void triggerGameOne(){
		gameOneDone = false;
		int numGames = (int)Random.Range (5, 10);
		gameOneSerial.Write ("" + (char) numGames);
	}

	public void triggerGameTwo(){
		gameTwoDone = false;
		int numGames = (int)Random.Range (5, 10);
		gameTwoSerial.Write ("" + (char)numGames);
	}

	/**
	 * Triggers one or the other minigame randomly.
	 */
	public void triggerGame(){
		if (Random.Range (0, 2) <= 2)
			triggerGameOne ();
		/* else
			triggerGameTwo (); */
	}
		
	/**
	 * Processes data from GameOne arduino.
	 */
	void processByteGameOne(int newByte){
		if (newByte == 's') {
			print ("Success!");
			playerHealth.heal (50);
		} else if (newByte == 'f') {
			print ("Failure!");
			playerHealth.damage (50);
		} else if (newByte == 'b') {
			print ("Board Reset!");
			gameOneDone = true;
		} else if (newByte == 'd') {
			print ("Game One Finished!");
			gameOneDone = true;
		}
	}

	void processByteGameTwo(int newByte){
		if(newByte == 's'){
			print("Success!");
			playerHealth.heal(100);
		} else if(newByte == 'f'){
			print ("Failure!");
			playerHealth.damage (100);
		} else if(newByte == 'b'){
			print ("Board Reset!");
			gameTwoDone = true;	
		} else if(newByte == 'd'){
			print ("Game Two Finished!");
			gameTwoDone = true;
		}
	}
		
	void Start () {
		playerHealth = GetComponent<PlayerHealth> ();
		gameOneDone = true;
		gameTwoDone = true;
		gameOneSerial = new SerialPort (gameOne, 9600);
		// gameTwoSerial = new SerialPort (gameTwo, 9600);
		initGameOne ();
		// initGameTwo ();
	}

	void Update () {
		while (gameOneSerial.BytesToRead > 0)
			processByteGameOne (gameOneSerial.ReadByte ());
		/* while (gameTwoSerial.BytesToRead > 0)
			processByteGameTwo (gameTwoSerial.ReadByte ()); */
	}
}
