using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler {
    private string savePath = "";
    private string saveName = "";

    public FileDataHandler(string savePath, string saveName) {
        this.savePath = savePath;
        this.saveName = saveName;
    }

    public GameData Load() {
        string fullPath = Path.Combine(savePath, saveName);
        GameData loadedData = null;

        if (File.Exists(fullPath)) {
            string loadData = "";
            using (FileStream stream = new FileStream(fullPath, FileMode.Open)) {
                using (StreamReader reader = new StreamReader(stream)) {
                    loadData = reader.ReadToEnd();
                }
            }
            loadedData = JsonUtility.FromJson<GameData>(loadData);
        }
        return loadedData;
    }

    public void Save(GameData data) {
        string fullPath = Path.Combine(savePath, saveName);

        Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
        string saveData = JsonUtility.ToJson(data, true);

        using (FileStream stream = new FileStream(fullPath, FileMode.Create)) {
            using (StreamWriter writer = new StreamWriter(stream)) {
                writer.Write(saveData);
            }
        }
    }
}
