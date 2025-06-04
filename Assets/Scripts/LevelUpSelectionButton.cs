using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpSelectionButton : MonoBehaviour
{
    public Text upgradeText, nameLevelText;
    private Weapon assignedWeapon;
    public Image img;
    public void UpdateButtonDisplay(Weapon weapon)
    {
        assignedWeapon = weapon;
        if (PlayerController.instance.assignedWeapon.Contains(weapon)) 
        { 
        upgradeText.text = weapon.weaponStats[weapon.weaponLevel].updateText;
        img.sprite = weapon.icon;
        nameLevelText.text = weapon.name + " µÈ¼¶ " + weapon.weaponLevel;
        }
        else
        {
            upgradeText.text = "Î´½âËø";
            img.sprite = weapon.icon;
            nameLevelText.text = weapon.name;
        }
    }
    public void SelectUpgrade()
    {
        if (assignedWeapon != null)
        {
            if (PlayerController.instance.assignedWeapon.Contains(assignedWeapon))
            {
                assignedWeapon.LevelUp();
            }
            else
            {
                PlayerController.instance.AddWeapon(assignedWeapon);
            }
            SFXManager.instance.Pitch(10);
            UIController.Instance.LevelUpPanel.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
