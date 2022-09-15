using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class EndGame : MonoBehaviour
{
    public void Quit() {
        string path = Path.Combine(Application.persistentDataPath, "save.json");
        Debug.Log(path);
        File.Delete(path);
        Application.Quit();
    }
}
