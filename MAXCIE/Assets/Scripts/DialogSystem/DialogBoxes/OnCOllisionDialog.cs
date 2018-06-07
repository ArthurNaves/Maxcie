using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCOllisionDialog : DialogBoxBase {
    protected override void OnTriggerEnter(Collider other)
    {
    }
    
    protected void OnCollisionEnter(Collision collision)
    {
        print("colidiu");
        if (collision.gameObject.CompareTag("Player"))
        {
            if (conditionMet)
            {
                player = Player.Instance;
                player.OnEnterDialog(this);
                DisplayDialogBox();
                StartCoroutine(ResetCondition());
            }
        }
    }
    IEnumerator ResetCondition()
    {
        yield return new WaitForSeconds(8);
        conditionMet = true;
    }
}
