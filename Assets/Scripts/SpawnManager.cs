using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

public class SpawnManager : MonoBehaviour
{
	[SerializeField] private List<GameObject> m_pool = new List<GameObject>();
	[SerializeField] private List<Material> m_materials = new List<Material>();
	[SerializeField] private BoxCollider m_spawnArea;
    [SerializeField] GameObject m_loopsRoot = null;
    [SerializeField] private Vector3 m_despawnPos;
	[SerializeField] private int m_loopsPerWave;
	private int m_waves;

	private int m_nextLoop = 0;
	private int m_loopsLeft = 0;

    public UnityEvent<int, int> m_onLoopsLeftUpdate;
    public UnityEvent<int, int> m_onRoundUpdate;

    public void StartSpawn()
    {
        m_loopsLeft = 0;
        m_nextLoop = 0;
        m_waves = 0;

        SpawnWave();
    }

    public void EndSpawn()
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

    public void SpawnWave()
	{
		m_waves += 1;
        m_onRoundUpdate.Invoke(m_waves, 1);
        m_loopsLeft = m_loopsPerWave;
        m_onLoopsLeftUpdate.Invoke(m_loopsPerWave, 0);
        for (int i = 0; i < m_loopsPerWave; ++i)
        {
            float waitTime = Random.Range(0f, 1f);
            GameObject toSpawn = m_pool[m_nextLoop];
            m_nextLoop = m_nextLoop + 1 < m_pool.Count ? m_nextLoop + 1 : 0;
            StartCoroutine(SpawnLoop(toSpawn, waitTime));
        }
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
    }

    private void Start()
    {
        foreach (Transform child in m_loopsRoot.transform)
        {
            m_pool.Add(child.gameObject);
        }
    }

    private IEnumerator SpawnLoop(GameObject loop, float time)
	{
		yield return new WaitForSeconds(time);
		if(loop.activeSelf == true)
		{
			DespawnLoop(loop);
		}

		RespawnLoop(loop);
		loop.GetComponent<Rigidbody>().isKinematic = false;
		
		int loopColorIdx = Random.Range(0, (int)LoopColor.Size);
		loop.GetComponent<Loop>().m_loopColor = (LoopColor)loopColorIdx;
		loop.GetComponentInChildren<Renderer>().material.color = m_materials[loopColorIdx].color;

        loop.SetActive(true);
		yield return true;
	}

    private void DespawnLoop(GameObject loop)
	{
        loop.transform.position = m_despawnPos;
        loop.GetComponent<Rigidbody>().isKinematic = true;
        loop.SetActive(false);
    }
}
