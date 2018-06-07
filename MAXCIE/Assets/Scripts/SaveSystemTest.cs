#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[ExecuteInEditMode]
public class SaveSystemTest : MonoBehaviour {
    [SerializeField] int sceneToLoad;
    [SerializeField] bool loadSave;
    [SerializeField] bool changeScene;
    // Update is called once per frame

    void Start()
    {
        loadSave = false;
        changeScene = false;
    }
    void Update () {
        if (loadSave)
        {
            LoadSave();
            loadSave = false;
        }
        if (changeScene)
        {
            ChangeScene();
            changeScene = false; 
        }
	}

    void LoadSave()
    {
        if (Player.Instance == null) Debug.Log("no player found");
        else
        {
            Player.Instance.LoadPlayer(SaveSystem.SaveReader());
        }
    }

    void ChangeScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
#endif