using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogCollectItemAndIntantiate : DialogInstantiateEnemies {
    [SerializeField] BookPages objectToCollect;
    [SerializeField] GameObject[] invizibleWalls;

    [SerializeField] string collectableType;

    protected override void DisplayDialogBox()
    {
        base.DisplayDialogBox();
        foreach (GameObject obj in invizibleWalls) obj.SetActive(true);
    }

    protected override void CloseDialog()
    {
        base.CloseDialog();
        Destroy(objectToCollect);
        
    }
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (conditionMet) objectToCollect.CollectPage(other);
    }

}
