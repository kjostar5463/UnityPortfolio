using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private Text ammoText;
    [SerializeField] private Slider HP_bar;
    [SerializeField] private Slider EXP_bar;
    [SerializeField] private Slider breath_bar;
    [SerializeField] private Text levelText;
    [SerializeField] private Text StageText;
    [SerializeField] private Text TimerText;
    [SerializeField] private Player player;


    // Update is called once per frame
    void Update()
    {
        ammoText.text = player.ammo.ToString() + " / " + player.maxAmmo.ToString();

        HP_bar.maxValue = player.maxHP;
        HP_bar.value = player.health;

        EXP_bar.maxValue = player.maxExp;
        EXP_bar.value = player.exp;

        breath_bar.maxValue = player.maxBreath;
        breath_bar.value= player.breath;

        levelText.text = "LV " + player.level.ToString();
        StageText.text = "STAGE " + 1;

        TimerText.text = TimeManager.Instance.stringMin + " : " + TimeManager.Instance.stringSec;
        TimerText.color = TimeManager.Instance.timerColor;
    }
}
