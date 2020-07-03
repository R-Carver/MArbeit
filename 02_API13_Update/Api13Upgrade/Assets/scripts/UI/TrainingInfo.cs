using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainingInfo : MonoBehaviour
{   
    public Text Infotext;

    public void UpdateInfoText(string newText)
    {
        Infotext.text = newText;
    }
}
