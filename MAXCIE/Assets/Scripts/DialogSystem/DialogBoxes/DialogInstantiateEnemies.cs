using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogInstantiateEnemies : DialogBoxBase {
    [SerializeField] InstantiateByCall instantiator;

    protected override void CloseDialog()
    {
        dialogBox.SetActive(false);
        conditionMet = false;
        CallDialogEvent();
        player.OnDialogClosed();
        instantiator.CallInstantiator();
        CallTuto();
    }
}
