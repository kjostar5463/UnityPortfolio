using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    [SerializeField] Button [] buttonList;
    [SerializeField] GameObject[] panelList;

    enum OPTION
    {
        GRAPIC,
        SOUND,
        MOUSE,
        LANGUAGE
    }

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OptionSelect()
    {

    }
    void GrapicOption()
    {
        
    }
    void SoundOption()
    {

    }
    void MouseOption()
    {

    }
    void LanguageOption()
    {

    }

    public void SelectOption(Button button)
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].interactable = true;
        }
        button.interactable = false;
    }
    public void OptionActive(GameObject panel)
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            panelList[i].SetActive(false);
        }
        panel.SetActive(true);
    }
}
