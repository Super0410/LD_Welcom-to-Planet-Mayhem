using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	public System.Action<int> OnNextWave;

	#region Public Properties

	public Wave[] waves;

	#endregion

	#region Private Properties

	Wave curWave;
	int curWaveNumber;

	int enemiesRemaining2Spawn;
	int enemiesRemainingAlive;
	float nextSpawnTime;

	#endregion

	#region Unity Methods

	void Start ()
	{
		nextWave ();
	}

	void Update ()
	{
//		if (GameManager.IsGameOver)
//			return;

		if ((enemiesRemaining2Spawn > 0 || curWave.infinite) && Time.time > nextSpawnTime) {
			enemiesRemaining2Spawn--;
			nextSpawnTime = Time.time + curWave.timeBetweenSpawns;

			generateFakeEnemy ();
		}
	}

	#endregion

	#region Private Methods

	void generateFakeEnemy ()
	{
		float generateDelay = 1f;
		float flashSpeed = 4f;
		float flashTimer = 0;

		Vector3 spawnPos = Planet.RandomEnemySpawnPoint;
		Vector3 dir2Plane = (Planet.Instance.transform.position - spawnPos).normalized;
		Quaternion spawnRotation = Quaternion.LookRotation (dir2Plane);
		spawnRotation.eulerAngles = new Vector3 (spawnRotation.eulerAngles.x - 90, spawnRotation.eulerAngles.y, spawnRotation.eulerAngles.z);

		FakeEnemy spawnedFakeEnemy = Instantiate (curWave.enemyPrefab, spawnPos, spawnRotation) as FakeEnemy;

		//TODO
		//spawnedFakeEnemy.OnDeath += onFakeEnemyDeath;
		//spawnedFakeEnemy.SetCharacteristics (curWave.moveSpeed, curWave.hits2KillPlayer, curWave.enemyHealth, curWave.skinColor);
	}

	void onFakeEnemyDeath ()
	{
		enemiesRemainingAlive--;
		if (enemiesRemainingAlive == 0 && !curWave.infinite) {
			nextWave ();
		}
	}

	void nextWave ()
	{
		if (curWaveNumber > 0)
			AudioManager.PlaySound2d ("LevelCompleted");

		curWaveNumber++;
		if (curWaveNumber <= waves.Length) {
			curWave = waves [curWaveNumber - 1];

			enemiesRemaining2Spawn = curWave.enemyCount;
			enemiesRemainingAlive = enemiesRemaining2Spawn;

			if (OnNextWave != null)
				OnNextWave (curWaveNumber);
		}
	}

	#endregion

	[System.Serializable]
	public class Wave
	{
		public bool infinite;
		public int enemyCount;
		public float timeBetweenSpawns;

		public float moveSpeed = 2.2f;
		public int hits2KillPlayer;
		public float enemyHealth;

		public FakeEnemy enemyPrefab;
	}
}
