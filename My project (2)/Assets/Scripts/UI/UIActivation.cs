using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIActivation : MonoBehaviour
{
    public bool GameIsPaused = false;
    public Button SaveGame;

    PlayerController Pc;
    private GameObject HealerUI;
    private GameObject ArrowsUI;
    private GameObject PauseMenuUI;
    PlayerInput input;

    void Start()
    {
        Pc = GameObject.Find("Player").GetComponent<PlayerController>();
        input = GameObject.Find("Player").GetComponent<PlayerInput>();
        HealerUI = GameObject.FindWithTag("HealerUI");
        ArrowsUI = GameObject.FindWithTag("ArrowsUI");
        PauseMenuUI = GameObject.FindWithTag("PauseMenuUI");
        SaveGame.onClick.AddListener(Save);

        HealerUI.SetActive(false);
        ArrowsUI.SetActive(false);
        PauseMenuUI.SetActive(false);
    }

    void Save()
    {
        DataPersistenceManager.instance.SaveGame();
    }

    void Update()
    {
        if (Pc.HasHealers)
        {
            HealerUI.SetActive(true);
        }
        if(Pc.HasBow)
        {
            ArrowsUI.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        input.SwitchCurrentActionMap("Player");
    }

    void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        input.SwitchCurrentActionMap("UI");
    }
}
