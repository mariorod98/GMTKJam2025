using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        DontDestroyOnLoad(this.gameObject);
    }
    /*** END SINGLETON BLOCK ***/

    /******************************************************************************/

    public InputManager m_inputManager;
    public SpawnManager m_spawnManager;

    [SerializeField] private int m_score = 0;
    [SerializeField] private float m_maxCountdown = 20.0f;
    private float m_countdown = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        m_countdown = m_maxCountdown;
        m_spawnManager.SpawnWave();
    }

    // Update is called once per frame
    void Update()
    {
        m_countdown -= Time.deltaTime;
    }


    public void OnLoopEnterBowl(LoopColor color)
    {
        m_score += 1;
        m_spawnManager.LoopScored();

        if(m_spawnManager.HasWaveFinished()) 
        {
            m_spawnManager.SpawnWave();
        }
    }

    public void OnLoopExitBowl(LoopColor color)
    {
        m_score -= 1;
        m_spawnManager.LoopDeducted();
    }
}
