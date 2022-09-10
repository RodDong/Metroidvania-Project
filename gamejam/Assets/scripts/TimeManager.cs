using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float timeScale = 1.0f;

    private void Start() {
        Time.timeScale = timeScale;
        Application.targetFrameRate = 60;
    }
    
}
