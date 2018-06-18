using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAnimgController : MonoBehaviour {

    [SerializeField] Animator anim;
    [SerializeField] string animParameter;
    [SerializeField] int spelll;

    DialogBoxBase dialog;
    public void ActivateTutorial(DialogBoxBase _dialog)
    {
        dialog = _dialog;
        _dialog.CallDialogEvent();
        anim.gameObject.SetActive(true);
        Default.inTutorial = true;
        Player.Instance.MouseAnim = this;
        anim.SetBool(animParameter, true);
    }
    public void OnPlayerSpell(int spell)
    {
        if (spelll == spell)
        {
            Default.inTutorial = false;
            anim.SetBool(animParameter, false);
            anim.gameObject.SetActive(false);
            dialog.CallDialogEvent();
        }
    }
}
