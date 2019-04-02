using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	private int TotalObjects;

	public void Spawn(GameObject _obj,int amount){
		for(int i = 0; i < (int)amount; i++){
			for(int j = 0; j < (int)amount;j++){
				Instantiate(_obj,new Vector3(0+(j/16), 2, 0+(i/16)),Quaternion.identity);
				TotalObjects++;
			}
		}
	}

	public void PrintTotal(){
		print("Totaal aantal objects: " + TotalObjects); 
	}
}
