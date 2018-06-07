using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGameObjectsActive : DialogBoxBase {
    [SerializeField] GameObject[] objs;
    void AvtivateGameObjs()
    {
        foreach (GameObject obj in objs) obj.SetActive(true);
    }
    protected override void OnTriggerEnter(Collider other)
    {
        if (conditionMet)
        {
            AvtivateGameObjs();
            player = Player.Instance;
            player.OnEnterDialog(this);
            DisplayDialogBox();
        }
    }
}
