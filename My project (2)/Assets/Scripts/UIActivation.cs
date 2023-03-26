using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIActivation : MonoBehaviour
{
    public bool GameIsPaused = false;

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

        HealerUI.SetActive(false);
        ArrowsUI.SetActive(false);
        PauseMenuUI.SetActive(false);
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
        input.actions.Enable();
    }

    void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        input.actions.Disable();
    }
}
