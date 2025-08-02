using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum UIScreen { StartMenu, EndMenu, ModifierMenu, HUD}

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
    [SerializeField] GameObject m_modifierMenu;

    [Header("HUD")]
    [SerializeField] Image m_timeProgressBarImage;
    [SerializeField] Color m_progressBarFullColor;
    [SerializeField] Color m_progressBarEmptyColor;

    [Header("Modifier Menu")]
    [SerializeField] Image m_opt1PowerUpPanel;
    [SerializeField] TMP_Text m_opt1PowerUpText;
    [SerializeField] TMP_Text m_opt1ChallengeUpText;

    [SerializeField] Image m_opt2PowerUpPanel;
    [SerializeField] TMP_Text m_opt2PowerUpText;
    [SerializeField] TMP_Text m_opt2ChallengeUpText;

    [SerializeField] Image m_opt3PowerUpPanel;
    [SerializeField] TMP_Text m_opt3PowerUpText;
    [SerializeField] TMP_Text m_opt3ChallengeUpText;

    [SerializeField] List<Color> m_powerLevelColors;



    public void Show(UIScreen screen)
    {
        m_startMenu.SetActive(false);
        m_endMenu.SetActive(false);
        m_HUD.SetActive(false);
        m_modifierMenu.SetActive(false);

        switch (screen) 
        {
            case UIScreen.StartMenu:
                m_startMenu.SetActive(true);
                break;
            case UIScreen.EndMenu:
                m_endMenu.SetActive(true);
                break;
            case UIScreen.ModifierMenu:
                m_modifierMenu.SetActive(true);
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

    public void OnUpdateModifierMenu(ModifierChoice mod1, ModifierChoice mod2, ModifierChoice mod3)
    {
        m_opt1PowerUpText.text = mod1.m_powerUp.GetFormattedText();
        m_opt1ChallengeUpText.text = mod1.m_challengeUp.GetFormattedText();
        m_opt1PowerUpPanel.color = m_powerLevelColors[mod1.m_level];

        m_opt2PowerUpText.text = mod2.m_powerUp.GetFormattedText();
        m_opt2ChallengeUpText.text = mod2.m_challengeUp.GetFormattedText();
        m_opt2PowerUpPanel.color = m_powerLevelColors[mod2.m_level];

        m_opt3PowerUpText.text = mod3.m_powerUp.GetFormattedText();
        m_opt3ChallengeUpText.text = mod3.m_challengeUp.GetFormattedText();
        m_opt3PowerUpPanel.color = m_powerLevelColors[mod3.m_level];

    }
}
