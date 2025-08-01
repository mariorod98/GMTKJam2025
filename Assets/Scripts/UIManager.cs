using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum UIScreen { StartMenu, EndMenu, UpgradeMenu, HUD}

public class UIManager : MonoBehaviour
{
    public UnityEvent<string> m_onScoreUpdate;
    public UnityEvent<string> m_onRoundUpdate;
    public UnityEvent<string> m_onLoopsLeftUpdate;
    public UnityEvent<string> m_onTotalLoopsUpdate;

    [Header("MenuParents")]
    [SerializeField] GameObject m_startMenu;
    [SerializeField] GameObject m_endMenu;
    [SerializeField] GameObject m_HUD;
    [SerializeField] GameObject m_upgradeMenu;

    [Header("HUD")]
    [SerializeField] Image m_timeProgressBarImage;
    [SerializeField] Color m_progressBarFullColor;
    [SerializeField] Color m_progressBarEmptyColor;
    [SerializeField] Color m_scoreTextGain;
    [SerializeField] Color m_scoreTextLose;

    public void Show(UIScreen screen)
    {
        m_startMenu.SetActive(false);
        m_endMenu.SetActive(false);
        m_HUD.SetActive(false);
        m_upgradeMenu.SetActive(false);

        switch (screen) 
        {
            case UIScreen.StartMenu:
                m_startMenu.SetActive(true);
                break;
            case UIScreen.EndMenu:
                m_endMenu.SetActive(true);
                break;
            case UIScreen.UpgradeMenu:
                m_upgradeMenu.SetActive(true);
                break;
            case UIScreen.HUD:
                m_HUD.SetActive(true);
                break;
        }
    }

    public void UpdateScoreText(int score, int amount)
    {
        m_onScoreUpdate.Invoke(score.ToString());
    }

    public void UpdateRounds(int score, int amount)
    {
        m_onRoundUpdate.Invoke(score.ToString());
    }

    public void UpdateLoopsLeftText(int score, int amount)
    {
        m_onLoopsLeftUpdate.Invoke(score.ToString());
    }

    public void UpdateTotalLoopsText(int score, int amount)
    {
        m_onTotalLoopsUpdate.Invoke(score.ToString());
    }

    public void UpdateTimeProgressBar(float percentage)
    {
        m_timeProgressBarImage.fillAmount = percentage;
        m_timeProgressBarImage.color = Color.Lerp(m_progressBarEmptyColor, m_progressBarFullColor, percentage);
    }
}
