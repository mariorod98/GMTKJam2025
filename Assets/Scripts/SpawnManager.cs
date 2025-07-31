using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Assertions;

public class SpawnManager : MonoBehaviour
{
	[SerializeField] private List<GameObject> m_pool = new List<GameObject>();
	[SerializeField] private List<Material> m_materials = new List<Material>();
	[SerializeField] private BoxCollider m_spawnArea;
	[SerializeField] private Vector3 m_despawnPos;
	[SerializeField] private int m_loopsPerWave;
	[SerializeField] private int m_waves;

	private int m_nextLoop = 0;
	private int m_loopsScored = 0;

    public void SpawnWave()
	{
		m_waves += 1;
        m_loopsScored = 0;
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
		return m_loopsScored == m_loopsPerWave;
    }

	public void LoopScored()
	{
		m_loopsScored += 1;
    }

	public void LoopDeducted()
	{
        m_loopsScored -= 1;
    }

    private IEnumerator SpawnLoop(GameObject toSpawn, float time)
	{
		yield return new WaitForSeconds(time);
		if(toSpawn.activeSelf == true)
		{
			DespawnLoop(toSpawn);
		}

		Vector3 center = m_spawnArea.center + transform.position;
		Vector3 extent = m_spawnArea.size * 0.5f;

		Vector3 spawnPos = center + new Vector3(Random.Range(-extent.x, extent.x), Random.Range(-extent.y, extent.y), Random.Range(-extent.z, extent.z));
		Debug.Log("Spawned loop at " + spawnPos.ToString());
		toSpawn.transform.position = spawnPos;
		toSpawn.transform.rotation = Random.rotation;
		toSpawn.GetComponent<Rigidbody>().isKinematic = false;
		
		int loopColorIdx = Random.Range(0, (int)LoopColor.Size);
		toSpawn.GetComponent<Loop>().m_loopColor = (LoopColor)loopColorIdx;
		toSpawn.GetComponent<Renderer>().material.color = m_materials[loopColorIdx].color;

        toSpawn.SetActive(true);
		yield return true;
	}

    private void DespawnLoop(GameObject loop)
	{
        loop.transform.position = m_despawnPos;
        loop.GetComponent<Rigidbody>().isKinematic = true;
        loop.SetActive(false);
    }
}
