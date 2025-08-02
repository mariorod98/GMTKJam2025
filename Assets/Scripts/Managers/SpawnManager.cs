using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnManager : MonoBehaviour
{
	[SerializeField] private BoxCollider m_spawnArea;
    [SerializeField] GameObject m_loopsRoot = null;
    [SerializeField] private Vector3 m_despawnPos;
    [SerializeField] private Vector3 m_baseLoopSize;
    [SerializeField] private float m_secondsBeforeWave = 1.0f;

	[SerializeField] private List<Material> m_materials = new List<Material>();
	[SerializeField] private int m_loopsPerWave;

    [Header("Events")]
    public UnityEvent<int, int> m_onLoopsLeftUpdate;
    public UnityEvent m_onWaveStart;

	private List<GameObject> m_pool = new List<GameObject>();
	private int m_nextLoop = 0;
	private int m_loopsLeft = 0;

    public void InitSpawn()
    {
        m_loopsLeft = 0;
        m_nextLoop = 0;
    }

    public void StarSpawn()
    {
        ResetSpawn();
        m_loopsLeft = Mathf.Min((int)(m_loopsPerWave * ModifierManager.Instance.m_loopsMultiplierModifier), m_pool.Count);
        m_onLoopsLeftUpdate.Invoke(m_loopsLeft, 0);
        StartCoroutine(SpawnWave(m_loopsLeft));
    }

    public void ResetSpawn()
    {
        foreach(GameObject go in m_pool)
        {
            if(go.activeSelf)
            {
                DespawnLoop(go);
            }
        }

        m_nextLoop = 0;
    }

    public void IncreaseLoopsPerWave()
    {
        m_loopsPerWave += 10;
    }


    public bool HasWaveFinished()
	{
		return m_loopsLeft == 0;
    }

	public void LoopScored()
	{
        m_loopsLeft -= 1;
        m_onLoopsLeftUpdate.Invoke(m_loopsLeft, -1);
    }

	public void LoopDeducted()
	{
        m_loopsLeft += 1;
        m_onLoopsLeftUpdate.Invoke(m_loopsLeft, +1);
    }

    public void RespawnLoop(GameObject loop)
    {
        Vector3 center = m_spawnArea.center + transform.position;
        Vector3 extent = m_spawnArea.size * 0.5f;

        Vector3 spawnPos = center + new Vector3(Random.Range(-extent.x, extent.x), Random.Range(-extent.y, extent.y), Random.Range(-extent.z, extent.z));
        loop.transform.position = spawnPos;
        loop.transform.rotation = Random.rotation;

        loop.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    public void DespawnLoop(GameObject loop)
    {
        loop.transform.position = m_despawnPos;
        loop.GetComponent<Loop>().ResetState();
        loop.SetActive(false);
    }

    private void Start()
    {
        foreach (Transform child in m_loopsRoot.transform)
        {
            m_pool.Add(child.gameObject);
        }
    }

    private IEnumerator SpawnWave(int waveSize)
    {
        yield return new WaitForSeconds(m_secondsBeforeWave);

        for (int i = 0; i < waveSize; ++i)
        {
            float waitTime = Random.Range(0f, 1f);
            GameObject toSpawn = m_pool[m_nextLoop];
            m_nextLoop = m_nextLoop + 1 < m_pool.Count ? m_nextLoop + 1 : 0;
            StartCoroutine(SpawnLoop(toSpawn, waitTime));
        }

        m_onWaveStart.Invoke();
    }

    private IEnumerator SpawnLoop(GameObject loop, float time)
	{
		yield return new WaitForSeconds(time);
		if(loop.activeSelf == true)
		{
			DespawnLoop(loop);
		}

		RespawnLoop(loop);		
		int loopColorIdx = Random.Range(0, ModifierManager.Instance.m_numberOfColorsModifier);
		loop.GetComponent<Loop>().m_loopColor = (LoopColor)loopColorIdx;
		loop.GetComponentInChildren<Renderer>().material.color = m_materials[loopColorIdx].color;

        loop.transform.localScale = m_baseLoopSize * (1.0f + Random.Range(0.0f, ModifierManager.Instance.m_loopSizeVariationModifier));
        loop.GetComponent<SphereCollider>().material.bounciness = ModifierManager.Instance.m_loopBouncinessModifier;
        loop.SetActive(true);
	}
}
