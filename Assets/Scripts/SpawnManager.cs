using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	[SerializeField] List<GameObject> m_pool = new List<GameObject>();
	[SerializeField] List<Material> m_materials = new List<Material>();
	[SerializeField] BoxCollider m_spawnArea;
	[SerializeField] Vector3 m_despawnPos;

	float _spawnCountdown = 0f;

	// Start is called before the first frame update
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
		_spawnCountdown -= Time.deltaTime;

		if (_spawnCountdown <= 0.0f)
		{
			NextLoop();
			_spawnCountdown = Random.Range(0f, 0.1f);
		}
	}

	void NextLoop()
	{
		SpawnLoop();
	}

	public bool SpawnLoop()
	{
		GameObject toSpawn = m_pool.Find(x => x.activeSelf == false);
		if (toSpawn == null) 
		{
			return false;
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
		return true;
	}

	public void DespawnLoop(GameObject loop)
	{
        loop.transform.position = m_despawnPos;
        loop.GetComponent<Rigidbody>().isKinematic = true;
        loop.SetActive(false);
    }
}
