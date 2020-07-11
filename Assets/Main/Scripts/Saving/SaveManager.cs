using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    private struct SaveObject
    {
        public Vector3 Position;
        public int Gold;
        public int Experience;
        public int Difficulty;
        public string Name;
        public List<int> ItemAmounts;
    }

    public PlayerManager PM;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SaveSystem.Init();
    }

    public void SaveGame()
    {
        PM = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        SaveObject saveObj = new SaveObject
        {
            Position = PM.transform.position,
            Gold = PM.gold,
            Experience = PM.experience,
            Difficulty = PlayerPrefs.GetInt(PrefNames.difficulty),
            Name = PlayerPrefs.GetString(PrefNames.playerName),
            ItemAmounts = new List<int>(PM.invetory.Count)
        };
        foreach(inventorySlotProxy proxy in PM.invetory)
        {
            saveObj.ItemAmounts.Add(proxy.itemAmount);
        }
        string json = JsonUtility.ToJson(saveObj);
        SaveSystem.Save(json);
    }

    public void LoadGame()
    {
        SaveObject loadedSave = JsonUtility.FromJson<SaveObject>(SaveSystem.Load());
        PlayerPrefs.SetInt(PrefNames.difficulty, loadedSave.Difficulty);
        StartCoroutine(LoadingValues(loadedSave));
    }

    IEnumerator LoadingValues(SaveObject loadedSave)
    {
        while(SceneManager.GetActiveScene().buildIndex != 1)
        {
            Debug.Log("Wait");
            yield return new WaitForSecondsRealtime(0.5f);
        }

        PM = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        PM.gold = loadedSave.Gold;
        PM.experience = loadedSave.Experience;
        PM.gameObject.transform.position = loadedSave.Position;
        PM.name = loadedSave.Name;
        for(int i = 0; i < loadedSave.ItemAmounts.Count; i += 1)
        {
            PM.invetory[i].itemAmount = loadedSave.ItemAmounts[i];
        }
        PM.UpdateUI();
    }
}
