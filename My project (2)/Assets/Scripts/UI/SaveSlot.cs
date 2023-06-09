using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlot : MonoBehaviour
{
    [SerializeField] private string profileID = "";

    [SerializeField] private GameObject noDataContent;
    [SerializeField] private GameObject hasDataContent;

    private Button saveSlotButton;

    private void Awake()
    {
        saveSlotButton = this.GetComponent<Button>();
    }

    public void SetData(GameData data)
    {
        if(data == null)
        {
            noDataContent.SetActive(true);
            hasDataContent.SetActive(false);
        }
        else
        {
            noDataContent.SetActive(false);
            hasDataContent.SetActive(true);
        }
    }

    public string GetProfileID()
    {
        return this.profileID;
    }

    public void SetInteractable(bool interactable)
    {
        saveSlotButton.interactable =  interactable;
    }
}
