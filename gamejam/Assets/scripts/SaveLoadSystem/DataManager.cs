using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataManager : MonoBehaviour
{
    [SerializeField] private string fileName;
    private GameData gameData;
    private List<IDataManager> dataMangerObjects;
    public FileDataHandler dataHandler;
    public static DataManager instance {get; private set;}

    private void Awake() {
        if (instance != null) {
            Debug.LogError("More than one DataManager in the scene!");
        }
        instance = this;
    }

    public void Start() {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataMangerObjects = FindAllDataManagerObjects();
        LoadGame();
    }

    public void NewGame() {
        this.gameData = new GameData();
    }
    public void LoadGame() {
        this.gameData = dataHandler.Load();
        if (this.gameData == null) {
            Debug.Log("No data was found, set to default.");
            NewGame();
        }

        foreach (IDataManager dataManagerObj in dataMangerObjects) {
            dataManagerObj.LoadData(gameData);
        }

        Debug.Log("Load player position = " + gameData.playerPosition);
    }
    public void SaveGame() {
        foreach (IDataManager dataManagerObj in dataMangerObjects) {
            dataManagerObj.SaveData(ref gameData);
        }
        Debug.Log("Save player position = " + gameData.playerPosition);
        dataHandler.Save(gameData);
    }

    private List<IDataManager> FindAllDataManagerObjects() {
        IEnumerable<IDataManager> dataMangerObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataManager>();
        return new List<IDataManager>(dataMangerObjects);
    }
}
