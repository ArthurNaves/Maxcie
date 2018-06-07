using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogBoxBase : MonoBehaviour {
    public delegate void PlayerEnteringDialog();
    public static PlayerEnteringDialog dialogEvent;

    public bool ConditionsMet{ get { return conditionMet; } }

    [SerializeField] protected GameObject dialogBox;
    [SerializeField] protected GameObject textsParent;

    [SerializeField] protected bool conditionMet;
    [SerializeField] protected float displaySpeed;

    protected Player player;

    protected Text[] names;
    protected Text[] texts;

    protected string textToDisplay;
    protected int index;
    protected bool textDisplayed;

    protected virtual void Awake()
    {
        dialogEvent = null;
        texts = new Text[textsParent.transform.childCount];
        names = new Text[textsParent.transform.childCount];

        for (int i = 0; i < texts.Length; i++)
        {
            texts[i] = textsParent.transform.GetChild(i).GetComponent<Text>();
            names[i] = texts[i].transform.GetChild(0).GetComponent<Text>();

            names[i].enabled = false;
            texts[i].enabled = false;
        }
    }

    // Use this for initialization
    protected virtual void Start () {
        textsParent.SetActive(true);
        dialogBox.SetActive(false);

        textDisplayed = false;
        textToDisplay = string.Empty;
        index = -1;
	}


    protected virtual void CloseDialog()
    {
        dialogBox.SetActive(false);
        conditionMet = false;
        CallDialogEvent();
        player.OnDialogClosed();
    }

    protected virtual void DisplayDialogBox()
    {
        CallDialogEvent();
        dialogBox.SetActive(true);
        ChangeDisplayedText();
            
    }

    protected IEnumerator DisplayText()
    {
        string spaces = string.Empty;
        int stringIndex = 0;

        foreach (char c in textToDisplay)
        {
            if (c == ' ')
            {
                stringIndex++;
                spaces += " ";
            }
            else break;
        }
        texts[index].text += spaces;
        while (stringIndex < textToDisplay.Length)
        {
            texts[index].text += textToDisplay[stringIndex++];
            yield return new WaitForSeconds(displaySpeed); 
        }

        textDisplayed = true;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (conditionMet)
        {
            player = Player.Instance;
            player.OnEnterDialog(this);
            DisplayDialogBox();
        }
    }

    protected virtual void CallDialogEvent()
    {
        if (dialogEvent != null) dialogEvent();
    }

    public virtual void ChangeDisplayedText()
    {
        StopCoroutine("DisplayText");

        if (textDisplayed || index == -1)
        {
            if (index > -1)
            {
                texts[index].enabled = false;
                names[index].enabled = false;
            }

            if ((index + 1) >= texts.Length) CloseDialog();
            else
            {
                index++;

                textToDisplay = texts[index].text;
                texts[index].text = string.Empty;
                texts[index].enabled = true;
                names[index].enabled = true;

                textDisplayed = false;
                StartCoroutine("DisplayText");
            }
        }
        else
        {
            texts[index].text = textToDisplay;
            textDisplayed = true;
        }
    }

    public virtual void SetConditionTrue()
    {
        conditionMet = true;
    }
}
