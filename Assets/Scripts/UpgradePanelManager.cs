using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePanelManager : MonoBehaviour
{
    [SerializeField] GameObject panel;

    [SerializeField] List<UpgradeButton> upgradeButtons;

    private void Start()
    {
        HideButtons();
    }

    public void openPanel(List<UpgradeData> upgradeDatas)
    {
        clean();
        panel.SetActive(true);
        Time.timeScale = 0;

        for(int i = 0; i < upgradeDatas.Count; i++)
        {
            upgradeButtons[i].gameObject.SetActive(true);
            upgradeButtons[i].Set(upgradeDatas[i]);
        }
    }

    public void closePanel()
    {
        HideButtons();

        panel.SetActive(false);
        Time.timeScale = 1;
    }

    private void HideButtons()
    {
        for (int i = 0; i < upgradeButtons.Count; i++)
        {
            upgradeButtons[i].gameObject.SetActive(false);
        }
    }

    public void upgrade(int pressButtonById)
    {
        GameObject.FindWithTag("Player").GetComponent<PlayerController>().upgrade(pressButtonById);
        closePanel();
    }

    public void clean()
    {
        for(int i = 0; i < upgradeButtons.Count;i++)
        {
            upgradeButtons[i].clean();
        }
    }
}
