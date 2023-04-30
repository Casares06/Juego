using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveSlotsMenu : Menu
{
    [SerializeField] private MainMenu mainMenu;
    private SaveSlot[] saveSlots;

    private bool isLoadingGame = false;

    private void Awake()
    {
        saveSlots = this.GetComponentsInChildren<SaveSlot>();
    }

    public void OnSaveSlotClicked(SaveSlot saveSlot)
    {
        DataPersistenceManager.instance.ChangeSelectedProfileID(saveSlot.GetProfileID());

        if(!isLoadingGame)
        {
            DataPersistenceManager.instance.NewGame();
            DataPersistenceManager.instance.SaveGame();
        }
        SceneManager.LoadScene("Tutorial");
        PlayerPrefs.DeleteAll();
    }

    public void onBackClicked()
    {
        mainMenu.ActivateMenu();
        this.DeactivateMenu();
    }


    public void ActivateMenu(bool isLoadingGame)
    {
        this.gameObject.SetActive(true);
        this.isLoadingGame = isLoadingGame;
        
        Dictionary <string, GameData> profilesGameData = DataPersistenceManager.instance.GetAllProfilesGameData();

        foreach (SaveSlot saveSlot in saveSlots)
        {
            GameData profileData = null;
            profilesGameData.TryGetValue(saveSlot.GetProfileID(), out profileData);
            saveSlot.SetData(profileData);
            if(profileData == null && isLoadingGame)
            {
                saveSlot.SetInteractable(false);
            }
            else
            {
                saveSlot.SetInteractable(true);
    
            } 
        }

    }

    public void DeactivateMenu()
    {
        this.gameObject.SetActive(false);
    }
}
