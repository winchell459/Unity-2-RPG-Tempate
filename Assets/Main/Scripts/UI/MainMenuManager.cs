﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject newGamePanel;
    public GameObject GameDatabase;

    // Start is called before the first frame update
    void Start()
    {
        newGamePanel.SetActive(false);
        MakeDatabase();
        SetDefaults();
    }

    public void NewGame()
    {
        Debug.Log("Creating New Game");
        newGamePanel.SetActive(true);
    }

    public void ContinueGame()
    {
        Debug.Log("Continuing Game");

        //day 3 changed/added -------------------------------------------------------------------------------
        if (SaveSystem.SaveFound())
        {
            StartGame();
            GameDatabase.GetComponent<SaveManager>().LoadGame();
        }
        else
        {
            newGamePanel.SetActive(true);
        }
        
    }

    public void QuitGame()
    {
        Debug.Log("Quiting Game");
        DeleteInfo();
        Application.Quit();
    }

    public void StartGame()
    {
        Debug.Log(PlayerPrefs.GetInt(PrefNames.difficulty));
        Debug.Log(PlayerPrefs.GetString(PrefNames.playerName));
        SceneManager.LoadScene(1);
    }

    void MakeDatabase()
    {
        if(GameDatabase != null || GameObject.Find("GameData"))
        {
            //day 3 added -----------------------------------------------------------------------------------------
            GameDatabase = GameObject.Find("GameData");

            return;
        }

        GameDatabase = new GameObject();
        GameDatabase.name = "GameData";
        GameDatabase.AddComponent<DatabaseManager>();
    }

    // PlayerPref helper functions
    public void SetDifficulty(int multiplier)
    {
        PlayerPrefs.SetInt(PrefNames.difficulty, multiplier);
    }

    public void SetPlayerName(string pName)
    {
        PlayerPrefs.SetString(PrefNames.playerName, pName);
    }

    void SetDefaults()
    {
        PlayerPrefs.SetInt(PrefNames.difficulty, 0);
        PlayerPrefs.SetString(PrefNames.playerName, "Character");
    }

    void DeleteInfo()
    {
        PlayerPrefs.DeleteAll();
    }
}
