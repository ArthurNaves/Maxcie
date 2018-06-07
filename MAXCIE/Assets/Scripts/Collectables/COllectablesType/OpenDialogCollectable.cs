using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDialogCollectable : Collectables {
    [SerializeField] DialogByCall dialogBox;
    
    protected override void OnTriggerEnter(Collider other)
    {
        dialogBox.CallDialog();
        Destroy(gameObject);
    }
}
