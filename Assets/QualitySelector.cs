using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QualitySelector : MonoBehaviour
{
    public TextMeshProUGUI qualText;
    int currentQual;
    int numQualLevels;

    // Start is called before the first frame update
    void Start()
    {
        currentQual = QualitySettings.GetQualityLevel();
        numQualLevels = QualitySettings.names.Length;
        qualText.text = "Quality: " + (currentQual + 1) + " out of " + (numQualLevels);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeQuality(bool increase)
    {
        if (increase)
        {
            QualitySettings.IncreaseLevel(true);
        }
        else
        {
            QualitySettings.DecreaseLevel(true);
        }
        currentQual = currentQual = QualitySettings.GetQualityLevel();
        qualText.text = "Quality: " + (currentQual + 1) + " out of " + (numQualLevels);
    }
}
