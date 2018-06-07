using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogInstantiateEnemies : DialogBoxBase {
    [SerializeField] InstantiateByCall instantiator;

    protected override void CloseDialog()
    {
        base.CloseDialog();
        instantiator.CallInstantiator();
    }
}
