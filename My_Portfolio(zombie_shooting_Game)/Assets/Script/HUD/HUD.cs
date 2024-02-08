using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private Text ammoText;
    [SerializeField] private Slider hpBar;
    [SerializeField] private Slider expBar;
    [SerializeField] private Slider breathBar;
    [SerializeField] private Text levelText;
    [SerializeField] private Text stageText;
    [SerializeField] private Text timerText;
    [SerializeField] private Player player;


    // Update is called once per frame
    void Update()
    {
        ammoText.text = player.ammo.ToString() + " / " + player.maxAmmo.ToString();

        hpBar.maxValue = player.maxHP;
        hpBar.value = player.health;

        expBar.maxValue = player.maxExp;
        expBar.value = player.exp;

        breathBar.maxValue = player.maxBreath;
        breathBar.value= player.breath;

        levelText.text = "LV " + player.level.ToString();
        stageText.text = "STAGE " + 1;

        timerText.text = TimeManager.Instance.strMin + " : " + TimeManager.Instance.strSec;
        timerText.color = TimeManager.Instance.timerColor;
    }
}
