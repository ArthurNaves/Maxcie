using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ENDGAME : MonoBehaviour {

    [SerializeField] GameObject button;

	void Start () {
        if (gameObject.activeInHierarchy) StartCoroutine("ButtonEnabler");	
    }
	
    IEnumerator ButtonEnabler()
    {
        button.SetActive(false);
        yield return new WaitForSeconds(6);
        button.SetActive(true);
    }
}
