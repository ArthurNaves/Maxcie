using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SavePoint : MonoBehaviour {
    static List<int> savePointsId = new List<int>();
    static SaveSystem.SaveInfo lastSave;

    [SerializeField] GameObject pointLight;

    [SerializeField] int savePointIdNum;

    [SerializeField] bool deleteAllSaves;//Variavel para debugar o sistema de save;
    [SerializeField] bool clearSave;     //Variavel para debugar o sistema de save;

    string pointId;

    void Awake()
    {
        pointId = savePointIdNum.ToString();

        CheckDebugOptions();

        if (PlayerPrefs.GetInt(pointId) == 0) LightOff();
        else LightUp();
    }
    void CheckDebugOptions()
    {
#if UNITY_EDITOR
        if (deleteAllSaves)
        {
            Debug.LogError("VOCE TEM CERTEZA QUE DESEJA DELETAR TODAS AS SAVES?");
            PlayerPrefs.DeleteAll();
        }
        else if (clearSave) PlayerPrefs.DeleteKey(pointId);

        if (savePointsId.Contains(savePointIdNum)) Debug.LogError("JA EXISTE UM SAVE POINT COM ESSE ID");
        else savePointsId.Add(savePointIdNum);
#endif
    }

    void ToggleLight()
    {
        pointLight.SetActive(!pointLight.activeSelf);
    }

    void LightUp()
    {
        pointLight.SetActive(true);
    }

    void LightOff()
    {
        pointLight.SetActive(false);
    }

    public void SaveGame(SaveSystem.SaveInfo saveInfo)
    {
        LightUp();
        PlayerPrefs.SetInt(pointId, 1);
        SaveSystem.SaveWriter(saveInfo);
         
    }

    void OnTriggerEnter(Collider other)
    {
        SaveSystem.SaveInfo newSave = Player.Instance.GetSaveInfo();
        if (newSave != lastSave)
        {
            lastSave = newSave;
            SaveGame(newSave);
            Debug.Log("GAME SAVED "+(newSave != lastSave));
        }
    }
}
