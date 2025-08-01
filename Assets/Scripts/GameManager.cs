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

    public UnityEvent<float> m_onCountdownUpdate;
    public UnityEvent<int, int> m_onScoreUpdate;

    [SerializeField] private float m_maxCountdown = 20.0f;
    [SerializeField] private float m_secondsBeforeFirstWave = 2.0f;

    private int m_score = 0;
    private float m_countdown = 0.0f;
    private bool m_countdownStart = false;


    // Start is called before the first frame update
    void Start()
    {
        // TODO: REMOVE
        StartGame();
    }

    public void StartGame()
    {
        m_score = 0;
        m_countdown = m_maxCountdown;
        m_onScoreUpdate.Invoke(0, 0);
        m_onCountdownUpdate.Invoke(1.0f);
        m_uiManager.Show(UIScreen.HUD);
        StartCoroutine(StartDelayedGame());
    }

    private IEnumerator StartDelayedGame()
    {
        yield return new WaitForSeconds(m_secondsBeforeFirstWave);
        m_spawnManager.StartSpawn();
        m_countdownStart = true;
        yield return true;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_countdownStart) {
            m_countdown -= Time.deltaTime;
            m_onCountdownUpdate.Invoke(m_countdown / m_maxCountdown);

            if (m_countdown <= 0.0f)
            {
                GameOver();
            }
        }
    }


    public void OnLoopEnterBowl(LoopColor color)
    {
        m_score += 1;
        m_onScoreUpdate.Invoke(m_score, 1);
        m_spawnManager.LoopScored();

        if(m_spawnManager.HasWaveFinished()) 
        {
            m_spawnManager.SpawnWave();
        }
    }

    public void OnLoopExitBowl(LoopColor color)
    {
        m_score -= 1;
        m_onScoreUpdate.Invoke(m_score, -1);
        m_spawnManager.LoopDeducted();
    }

    private void GameOver()
    {
        m_countdownStart = false;
        m_spawnManager.EndSpawn();
        m_uiManager.Show(UIScreen.EndMenu);
    }
}
