using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	[SerializeField] List<GameObject> _pool = new List<GameObject>();
	[SerializeField] BoxCollider _spawnArea;

	float _spawnCountdown = 0f;
	int _nextSpawnIdx = 0;

	// Start is called before the first frame update
	void Start()
	{
		int startingSpawn = _pool.Count / 10;
		for(int i = 0; i < startingSpawn; ++i)
		{
			NextLoop();
		}

		_spawnCountdown = Random.Range(0f, 4f);
	}

	// Update is called once per frame
	void Update()
	{
        _spawnCountdown -= Time.deltaTime;

        if (_spawnCountdown <= 0.0f)
		{
			NextLoop();
			_spawnCountdown = Random.Range(0f, 4f);
		}
	}

	void NextLoop()
	{
		SpawnLoop();
		_nextSpawnIdx = _nextSpawnIdx + 1 < _pool.Count ? _nextSpawnIdx + 1 : 0;
		DespawnLoop();
	}

	void SpawnLoop()
	{
		GameObject toSpawn = _pool[_nextSpawnIdx];
		Vector3 center = _spawnArea.center + transform.position;
		Vector3 extent = _spawnArea.size * 0.5f;

		Vector3 spawnPos = center + new Vector3(Random.Range(-extent.x, extent.x), Random.Range(-extent.y, extent.y), Random.Range(-extent.z, extent.z));
		Debug.Log("Spawned loop at " + spawnPos.ToString());
		toSpawn.transform.position = spawnPos;
		toSpawn.GetComponent<Rigidbody>().isKinematic = false;
		toSpawn.SetActive(true);
	}

	void DespawnLoop()
	{
		GameObject toDespawn = _pool[_nextSpawnIdx];
		toDespawn.transform.position = Vector3.zero;
		toDespawn.GetComponent<Rigidbody>().isKinematic = true;
		toDespawn.SetActive(false);
	}
}
