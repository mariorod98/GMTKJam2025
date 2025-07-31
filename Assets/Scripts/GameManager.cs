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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
