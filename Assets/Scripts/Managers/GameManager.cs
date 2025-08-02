using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum LoopColor { Red, Orange, Yellow, Green, Blue, Purple, Size }

public class GameManager : MonoBehaviour
{
    /******************************************************************************/

    /*** START SINGLETON BLOCK ***/
    private static GameManager m_instance = null;
    public static GameManager Instance
    {
        get { return m_instance; }
    }

    private void Awake()
    {
        m_instance = this;
    }
    /*** END SINGLETON BLOCK ***/

    /******************************************************************************/

    public InputManager m_inputManager;
    public SpawnManager m_spawnManager;
    public UIManager m_uiManager;

    public UnityEvent<float, float, float> m_onCountdownUpdate;
    public UnityEvent<int, int> m_onScoreUpdate;
    public UnityEvent<int, int> m_onTotalLoopsUpdate;
    public UnityEvent<int, int> m_onRoundUpdate;

    public UnityEvent<ModifierChoice, ModifierChoice, ModifierChoice> m_onModifierChoiceStarted;

    [SerializeField] private float m_baseMaxCountdown = 20.0f;
    [SerializeField] private float m_baseMaxBonusCountdown = 5.0f;
    [SerializeField] private float m_waitTimeForCountdown = 2.0f;

    private int m_rounds;
    private int m_score = 0;
    private int m_totalLoops = 0;
    private float m_countdown = 0.0f;
    private bool m_isPlaying = false;
    private float m_bonusCountdown = 0.0f;


    private List<ModifierChoice> m_modifierChoices = new List<ModifierChoice>();

    private void Start()
    {
        // StartGame();
    }

    public bool IsPlaying()
    {
        return m_isPlaying;
    }

    public void StartGame()
    {
        m_rounds = 0;
        m_score = 0;
        m_totalLoops = 0;
        m_onScoreUpdate.Invoke(0, 0);
        m_spawnManager.InitSpawn();
        StartRound();
    }

    public void OnWaveStart()
    {
        StartCoroutine(StartCountdown());
    }
    private IEnumerator StartCountdown()
    {
        yield return new WaitForSeconds(m_waitTimeForCountdown);
        m_isPlaying = true;
    }

    public void OnLoopEnterBowl(LoopColor color)
    {
        if (!m_isPlaying) return;

        AddScore(1);
        m_totalLoops += 1;
        m_onScoreUpdate.Invoke(m_score, 1);
        m_onTotalLoopsUpdate.Invoke(m_totalLoops, 1);

        m_spawnManager.LoopScored();

        if(m_spawnManager.HasWaveFinished()) 
        {
            EndRound();
        }
    }

    public void OnLoopExitBowl(LoopColor color)
    {
        if (!m_isPlaying) return;

        AddScore(-1);
        m_totalLoops -= 1;

        m_onScoreUpdate.Invoke(m_score, -1);
        m_onTotalLoopsUpdate.Invoke(m_totalLoops, -1);
        m_spawnManager.LoopDeducted();
    }

    public void OnModifierSelected(int selected)
    {
        m_modifierChoices[selected].ApplyChoice();
        m_modifierChoices.Clear();
        StartRound();
    }

    public float GetMaxCountdown()
    {
        return m_baseMaxCountdown + ModifierManager.Instance.m_countdownModifier;
    }

    public float GetMaxBonusCountdown()
    {
        return m_baseMaxBonusCountdown + ModifierManager.Instance.m_bonusCountdownModifier;
    }

    private void Update()
    {
        if (m_isPlaying)
        {
            m_countdown -= Time.deltaTime;
            m_bonusCountdown -= Time.deltaTime;
            m_onCountdownUpdate.Invoke(m_countdown / GetMaxCountdown(), m_countdown, m_bonusCountdown);

            if (m_countdown <= 0.0f)
            {
                GameOver();
            }
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            EndRound();
        }
    }

    private void AddScore(int value)
    {
        m_score += value * (IsInBonusTime() ? ModifierManager.Instance.m_bonusScoreModifier : 1);
    }

    private bool IsInBonusTime()
    {
        return m_bonusCountdown > 0.0f;
    }

    private void StartRound()
    {
        m_rounds += 1;
        m_countdown = GetMaxCountdown();
        m_bonusCountdown = GetMaxBonusCountdown();
        m_onCountdownUpdate.Invoke(1.0f, m_countdown, m_bonusCountdown);
        m_onRoundUpdate.Invoke(m_rounds, 1);
        m_uiManager.Show(UIScreen.HUD);
        m_spawnManager.StarSpawn();
    }

    private void EndRound()
    {
        m_isPlaying = false;
        PickModifier();
    }

    private void PickModifier()
    {
        m_modifierChoices.Add(ModifierManager.Instance.GenerateModifierChoice());
        m_modifierChoices.Add(ModifierManager.Instance.GenerateModifierChoice());
        m_modifierChoices.Add(ModifierManager.Instance.GenerateModifierChoice());
        m_onModifierChoiceStarted.Invoke(m_modifierChoices[0], m_modifierChoices[1], m_modifierChoices[2]);
        m_uiManager.Show(UIScreen.ModifierMenu);
    }

    private void GameOver()
    {
        m_isPlaying = false;
        m_uiManager.Show(UIScreen.EndMenu);
    }
}
