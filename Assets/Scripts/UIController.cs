using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;
    public Slider expSlider;
    public TMP_Text text;
    public TMP_Text timerText;
    public LevelUpSelectionButton[] buttons;
    public GameObject LevelUpPanel;
    public Text goldValueText;
    public GameObject PlayerUpgradePanel;
    public PlayerStateUpgradeButton moveSpeedButton, healthButton, pickUpRangeButton, maxWeaponButton;
    public GameObject EndPanel;
    public TMP_Text SurviveTimeText;
    public string menuName;
    public GameObject PausePanel;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        Pause();
    }
    public void UpdateExp(int currentExp,int levelExp,int currentLevel) 
    {
        expSlider.maxValue = levelExp;
        expSlider.value = currentExp;
       if(currentLevel<ExpLevelController.instance.maxLevel)
        text.text = "Level " + currentLevel;
       if(currentLevel== ExpLevelController.instance.maxLevel)
            text.text = "Level " + currentLevel+" (max)";
    }
    public void SkipLevelUp()
    {
        UIController.Instance.LevelUpPanel.SetActive(false);
        Time.timeScale = 1.0f;
    }
    public void UpdateCoins()
    {
        goldValueText.text =" " + CoinController.instance.currentCoins;
    }
    public void Back()
    {
        SFXManager.instance.Pitch(10);
        UIController.Instance.PlayerUpgradePanel.SetActive(false);
    }
    public void Enter()
    {
        SFXManager.instance.Pitch(10);
        PlayerStateController.instance.PlayerStatDisplay();
        UIController.Instance.PlayerUpgradePanel.SetActive(true);
        
    }
    public void BuyMoveSpeed()
    {
        PlayerStateController.instance.BuyMoveSpeed();
    }
    public void BuyHealth()
    {
        PlayerStateController.instance.BuyHealth();
    }
    public void BuyPickUpRange()
    {
        PlayerStateController.instance.BuyPickUpRange();
    }
    public void BuyMaxWeapon()
    {
        PlayerStateController.instance.BuyMaxWeapon();
    }
    public void UpdateTimer(float time)
    {
        float minutes =  Mathf.FloorToInt( time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        timerText.text = minutes.ToString("00") + " : " + seconds.ToString("00");
    }
    public void Restart()
    {
        SFXManager.instance.Pitch(12);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }
    public void BackToMenu()
    {
        SFXManager.instance.Pitch(12);
        SceneManager.LoadScene(menuName);
        Time.timeScale = 1;    
    }
    public void BackToGame()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1;
        SFXManager.instance.Pitch(9);
    }
    public void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PausePanel.activeSelf)
            {
                PausePanel.SetActive(false);
                if(!LevelUpPanel.activeSelf)
                Time.timeScale = 1;
                SFXManager.instance.Pitch(9);
            }
            else
            {
                PausePanel.SetActive(true);
                Time.timeScale = 0;
                SFXManager.instance.Pitch(8);
            }
        }
    }
}
