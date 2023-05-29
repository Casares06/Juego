using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : Menu
{
    [SerializeField] private SaveSlotsMenu saveSlotsMenu;
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueGameButton;
    UIActivation UI;

    private void Start()
    {
        if(!DataPersistenceManager.instance.HasGameData())
        {
            continueGameButton.interactable = false;
        }
    }

    public void OnNewGameClicked()
    {
        saveSlotsMenu.ActivateMenu(false);
        this.DeactivateMenu();
    }

    public void OnContinueGameClicked()
    {
        DisableMenuButtons();
        SceneManager.LoadSceneAsync("Tutorial");
        Time.timeScale = 1f;
    }

    public void OnTestZoneClicked()
    {
        DisableMenuButtons();
        SceneManager.LoadSceneAsync("TestZone");
        Time.timeScale = 1f;
    }

    public void onLoadClicked()
    {
        this.DeactivateMenu();
        saveSlotsMenu.ActivateMenu(true);
    }

    private void DisableMenuButtons()
    {
        newGameButton.interactable = false;
        continueGameButton.interactable = false;
    }

    public void ActivateMenu()
    {
        this.gameObject.SetActive(true);
    }

    public void DeactivateMenu()
    {
        this.gameObject.SetActive(false);
    }
}
