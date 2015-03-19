using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {
	public int Amout;
	public float spawnWait;
	public float waveWait;
	public GameObject[] Enemy;
	public float CheckRandom;
	public Vector3 SpawnPoint;
	public int waveCount;
	Quaternion rot;
	public int CheckWave;
	public int EnemyNumber;
	public int randomEnemy;
	public GameObject currentSpawnEnemy;
	// Use this for initialization
	void Start () {
		StartCoroutine (SpawnWaves ());
		CheckWave = 1;
		EnemyNumber = 3;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	IEnumerator SpawnWaves ()
	{
		while (!this.gameObject.GetComponent<playerAnimation2> ().Dead) {

			for (int a = 0; a <= Amout; a++) {
				CheckRandom = Random.Range (1,3);
				randomEnemy = Random.Range(0,EnemyNumber);
				Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,Screen.height,0));
				if (CheckRandom == 1) {
					SpawnPoint.x = pos.x;
					rot = Quaternion.Euler(0,-90,0);
				} else if (CheckRandom == 2) {
					SpawnPoint.x = -pos.x;
					rot = Quaternion.Euler(0,90,0);
				}
				for (int i = 0; i < Enemy.Length; i++)
				{
					if(randomEnemy == i)
						currentSpawnEnemy = Enemy[i];
				}
				if(currentSpawnEnemy !=null)
				{
					if (a == Amout && CheckWave > 4)
						Instantiate (Enemy[5], SpawnPoint, rot);
					else
						Instantiate (currentSpawnEnemy, SpawnPoint, rot);
				}
				yield return new WaitForSeconds (spawnWait);
			}

			waveCount++;
			CheckWave++;
			if(waveCount >= 3)
			{
				EnemyNumber++;
				if(Amout <= 15)
					Amout += 1;
				else if(spawnWait > 0.1f)
					spawnWait -= 0.07f;
				waveCount = 0;
			}
			yield return new WaitForSeconds (waveWait);
		}
	}
}
