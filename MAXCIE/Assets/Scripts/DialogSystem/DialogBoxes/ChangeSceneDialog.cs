using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneDialog : DialogBoxBase {
    [SerializeField] GameObject[] notDestroy;
    [SerializeField] GameObject playerSpawnPos;

    [SerializeField] int sceneToGo;

    protected override void Awake()
    {
        base.Awake();
        foreach (GameObject obj in notDestroy) DontDestroyOnLoad(obj);
        DontDestroyOnLoad(gameObject);
    }
    protected override void CloseDialog()
    {
        base.CloseDialog();
        SceneManager.LoadScene(sceneToGo);
        Player.Instance.transform.position = playerSpawnPos.transform.position;
        Destroy(gameObject);
    }
    public override void CallDialogEvent()
    {
    }
}
