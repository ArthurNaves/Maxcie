using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Buttons : MonoBehaviour {
    [SerializeField] GameObject firstDialog;
    [SerializeField] GameObject menuParent;
    [SerializeField] GameObject[] buttons;
    [SerializeField] GameObject[] blur;
    [SerializeField] AudioSource auds;


    private void Update()
    {
        InputCheck();
    }

    public void AcordarLucileButton()
    {
        buttons[0].SetActive(false);
        buttons[1].SetActive(true);
    }

    public void DoInicioButton()
    {
        Player.Instance.ChangeState();
        menuParent.SetActive(false);
    }

    public void Quit()
    {
        StartCoroutine("QuitScene");
    }

    public void DoUltimoPontoButton()
    {
        SaveSystem.SaveInfo saveInfo = SaveSystem.SaveReader();
        Player.Instance.LoadPlayer(saveInfo);
        if (SceneManager.GetActiveScene().buildIndex != saveInfo.currentLevel) SceneManager.LoadScene(saveInfo.currentLevel);
        Player.Instance.ChangeState();
        firstDialog.SetActive(true);
        menuParent.SetActive(false);
    }

    private void InputCheck()
    {
        if (Input.GetKeyDown("escape"))
        {
            buttons[0].SetActive(true);
            buttons[1].SetActive(false);
        }
    }

    IEnumerator QuitScene()
    {
        buttons[0].SetActive(false);
        buttons[1].SetActive(false);
        yield return new WaitForSeconds(1f);
        blur[0].SetActive(true);
        yield return new WaitForSeconds(1f);
        blur[1].SetActive(true);
        auds.Play();
        yield return new WaitForSeconds(1f);
        blur[2].SetActive(true);
        yield return new WaitForSeconds(1f);
        blur[3].SetActive(true);
        yield return new WaitForSeconds(1f);
        blur[4].SetActive(true);
        yield return new WaitForSeconds(1f);
        Application.Quit();
    }
}
