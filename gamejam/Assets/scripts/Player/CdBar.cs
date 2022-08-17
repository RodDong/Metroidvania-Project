using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CdBar : MonoBehaviour
{
    public Slider cdSlider;

    public void SetMaxCD(float cd) {
        cdSlider.maxValue = cd;
        cdSlider.value = cd;
    }

    public void SetCD(float cd) {
        cdSlider.value = cd;
    }
}
