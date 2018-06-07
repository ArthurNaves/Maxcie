using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogByCall : DialogBoxBase {
    protected override void OnTriggerEnter(Collider other)
    {
    }

    public virtual void CallDialog()
    {
        if (conditionMet)
        {
            player = Player.Instance;
            player.OnEnterDialog(this);
            DisplayDialogBox();
        }
    }
}
