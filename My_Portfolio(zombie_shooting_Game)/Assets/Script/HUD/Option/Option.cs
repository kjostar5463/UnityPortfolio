using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    [SerializeField] OptionManager optionManager;
    [SerializeField] Button button;
    [SerializeField] GameObject panel;

    private void OnEnable()
    {
        optionManager.SelectOption(button);
        optionManager.OptionActive(panel);
    }

    
}
