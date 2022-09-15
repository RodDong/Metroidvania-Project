using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataManager : MonoBehaviour
{
    [SerializeField] private string fileName;
    [SerializeField] private Vector3 initialLocation;
    private GameData gameData;
    private List<IDataManager> dataMangerObjects;
    public FileDataHandler dataHandler;
    public static DataManager instance {get; private set;}

    private void Awake() {
        if (instance != null) {
        }
        instance = this;
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataMangerObjects = FindAllDataManagerObjects();
        LoadGame();
    }

    public void NewGame() {
        this.gameData = new GameData();
        this.gameData.playerPosition = initialLocation;
    }
    public void LoadGame() {
        this.gameData = dataHandler.Load();
        if (this.gameData == null) {
            NewGame();
        }

        foreach (IDataManager dataManagerObj in dataMangerObjects) {
            dataManagerObj.LoadData(gameData);
        }
    }
    public void SaveGame() {
        foreach (IDataManager dataManagerObj in dataMangerObjects) {
            dataManagerObj.SaveData(ref gameData);
        }
        dataHandler.Save(gameData);
    }

    private List<IDataManager> FindAllDataManagerObjects() {
        IEnumerable<IDataManager> dataMangerObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataManager>();
        return new List<IDataManager>(dataMangerObjects);
    }
}
