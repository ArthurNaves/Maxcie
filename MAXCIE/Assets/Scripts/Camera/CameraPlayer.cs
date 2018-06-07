using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayer : MonoBehaviour {
    [SerializeField] Transform playerTrans;
    Vector3 dist;
    // Use this for initialization
    void Start () {
        dist = transform.position - playerTrans.transform.position ;
	}

    private void LateUpdate()
    {
        transform.position = playerTrans.position + dist;
    }
}
