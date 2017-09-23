using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class DataController : MonoBehaviour {

	public static DataController instance;
	string saveLocation;
	Gamestate data;

	void Awake(){
		instance = this;
		saveLocation = Application.persistentDataPath + "/player.sav";

	}

	public void Load(){
		if(File.Exists (saveLocation)){
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream fm = new FileStream (saveLocation,FileMode.Open);
			Gamestate loadedData = bf.Deserialize (fm) as Gamestate;
			fm.Close ();
			if(loadedData != null){
				data = loadedData;
			}
		} else{
			// init gamedata
		}


	}


	public void Save(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream fm = new FileStream (saveLocation, FileMode.Create);
		bf.Serialize (fm, data);
		fm.Close ();
		print ("saved");
	}


//	void SaveData(){
//		BinaryFormatter bf = new BinaryFormatter ();
//		FileStream stream = new FileStream (playerDataLocation, FileMode.Create);
//		bf.Serialize (stream, data);
//		stream.Close ();
//		print ("game saved.");
//	}
//
//	void LoadData(){
//		if(File.Exists (playerDataLocation)){
//			BinaryFormatter bf = new BinaryFormatter ();
//			FileStream stream = new FileStream (playerDataLocation, FileMode.Open);
//			PlayerData loadedData = bf.Deserialize (stream) as PlayerData;
//			stream.Close ();
//			data = loadedData;
//			if(data != null){
//				print ("loaded save file");
//				//				printStats ();
//			}
//		} else {
//			print ("no save file found. making a new one.");
//			initGameData ();
//			SaveData ();
//			//			printStats ();
//		}
//	}



}

[Serializable]
public class Gamestate{
	public int numberOfCoins;
}
