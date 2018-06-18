using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaticDialog : DialogInstantiateEnemies {
    [SerializeField] GameObject[] textParents;
    [SerializeField] BookPages bookPage;
    [SerializeField] SetGameObjectsActive dialog;
    [SerializeField] MouseAnimgController[] anim;

    static int dialogIndex = 0;


    protected override void Awake()
    {
    }

    protected override void Start()
    {
        dialogBox.SetActive(false);

        textDisplayed = false;
        textToDisplay = string.Empty;
        index = -1;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (dialogIndex < 3 && conditionMet)
        {
            if (BookPages.PagesCollected == 0)
            {
                dialog.SetConditionTrue();
            }
            bookPage.CollectPage(other);
            InitializeText();
            player = other.GetComponent<Player>();
            player.OnEnterDialog(this);
            DisplayDialogBox();
        }
    }

    protected virtual void InitializeText()
    {
        texts = new Text[textParents[dialogIndex].transform.childCount];
        names = new Text[textParents[dialogIndex].transform.childCount];

        for (int i = 0; i < texts.Length; i++)
        {
            texts[i] = textParents[dialogIndex].transform.GetChild(i).GetComponent<Text>();
            names[i] = texts[i].transform.GetChild(0).GetComponent<Text>();

            names[i].enabled = false;
            texts[i].enabled = false;
        }

        textParents[dialogIndex].SetActive(true);
        dialogIndex++;
    }
    //
    protected override void CallTuto()
    {
        anim[dialogIndex-1].ActivateTutorial(this);
    }
}
