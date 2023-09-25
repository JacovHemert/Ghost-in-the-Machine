using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
    private string _dataDirectoryPath = "";
    private string _dataFileName = "";

    public FileDataHandler (string dataDirectoryPath, string dataFileName)
    {
        this._dataDirectoryPath = dataDirectoryPath;
        this._dataFileName = dataFileName;
    }

    public GameData Load()
    {
        string fullPath = Path.Combine(_dataDirectoryPath + _dataFileName);

        GameData loadedData = null;

        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
                Debug.Log("Loaded from " + fullPath);

            }
            catch (Exception error)
            {
                Debug.LogError("Error while trying to load data from file: " + fullPath + "\n" + error);
            }
        }
        
        return loadedData;
    }

    public void Save(GameData data)
    {
        string fullPath = Path.Combine(_dataDirectoryPath + _dataFileName);

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataToStore = JsonUtility.ToJson(data, true);

            using(FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }

            Debug.Log("Saved to " + fullPath);
        }
        catch (Exception error)
        {
            Debug.LogError("Error while trying to save data to file: " + fullPath + "\n" + error);
            throw;
        }
    }


}
