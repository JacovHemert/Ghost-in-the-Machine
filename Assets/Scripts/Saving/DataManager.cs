using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string _fileName;


    private GameData _gameData;
    private List<IData> dataObjects;

    private FileDataHandler _dataHandler;

    public static DataManager instance { get; private set; }


    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Another DataManager exists already.");
        }
        instance = this;
    }

    public static DataManager GetInstance()
    {
        return instance;
    }


    private IEnumerator Start()
    {
        //FindObjectOfType<HUDController>().BlackScreen();
        
        this._dataHandler = new FileDataHandler(Application.persistentDataPath, _fileName);
        this.dataObjects = FindAllDataObjects();
        LoadGame();
        yield return new WaitForSecondsRealtime(0.5f);
    }

    public void NewGame()
    {
        this._gameData = new GameData();
    }

    public void SaveGame()
    {
        Debug.Log("Saving");
        foreach (IData dataObject in dataObjects)
        {
            dataObject.SaveData(_gameData);
        }

        //Debug.Log("Saved data: " + _gameData.SpareParts);

        _dataHandler.Save(_gameData);
    }

    public void LoadGame()
    {
        this._gameData = _dataHandler.Load();

        if (this._gameData == null)
            Debug.Log("File data is null");
        else
            Debug.Log("File data is something; this: " + _gameData);

        if (this._gameData == null)
        {
            Debug.LogWarning("No save file exists; creating new file.");
            NewGame();
        }

        foreach (IData dataObject in dataObjects)
        {            
            dataObject.LoadData(_gameData);
        }

        //Debug.Log("Loaded data: " + _gameData.SpareParts);
    }

    private void OnApplicationQuit()
    {
        //SaveGame();
    }


    private List<IData> FindAllDataObjects()
    {
        IEnumerable<IData> dataObjects = FindObjectsOfType<MonoBehaviour>(true).OfType<IData>();
        return new List<IData>(dataObjects);
    }

}
