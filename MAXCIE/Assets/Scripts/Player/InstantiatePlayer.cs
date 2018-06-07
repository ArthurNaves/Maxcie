using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatePlayer : MonoBehaviour {
    [SerializeField] GameObject playerFab;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void InstatiatePlayer(SaveSystem.SaveInfo saveInfo)
    {
        Player newPlayer;

        newPlayer = Instantiate(playerFab, saveInfo.playerPos, playerFab.transform.rotation, transform).GetComponent<Player>();
    }

}
