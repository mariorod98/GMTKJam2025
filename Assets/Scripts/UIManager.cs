using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text m_scoreText;
    [SerializeField] TMPro.TMP_Text m_loopsLeftText;
    [SerializeField] Image m_timeProgressBarImage;
    [SerializeField] Color m_progressBarFullColor;
    [SerializeField] Color m_progressBarEmptyColor;
    [SerializeField] Color m_scoreTextGain;
    [SerializeField] Color m_scoreTextLose;


    public void UpdateScoreText(int score, int amount)
    {
        m_scoreText.text = score.ToString();
    }

    public void UpdateLoopsLeftText(int score, int amount)
    {
        m_loopsLeftText.text = score.ToString();
    }

    public void UpdateTimeProgressBar(float percentage)
    {
        Debug.Log(percentage.ToString());
        m_timeProgressBarImage.fillAmount = percentage;
        m_timeProgressBarImage.color = Color.Lerp(m_progressBarEmptyColor, m_progressBarFullColor, percentage);
    }
}
