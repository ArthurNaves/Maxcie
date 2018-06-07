using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPostsManager : MonoBehaviour {
    [SerializeField] LightPosts[] lightPosts;
    int index = 0;
    public void OnPlayerCollectPage()
    {
        lightPosts[index++].TurnOnLights();
    }
}
