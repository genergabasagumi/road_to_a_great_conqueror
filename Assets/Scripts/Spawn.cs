using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {
	public int Amout;
	public float spawnWait;
	public float waveWait;
	public GameObject Enemy;
	public float CheckRandom;
	public Vector3 SpawnPoint;
	public int waveCount;
	Quaternion rot;
	// Use this for initialization
	void Start () {
		StartCoroutine (SpawnWaves ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	IEnumerator SpawnWaves ()
	{
		while (true) {
			for (int a = 0; a < Amout; a++) {
				CheckRandom = Random.Range (1,3);
				Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,0,0));
				if (CheckRandom == 1) {
					SpawnPoint.x = pos.x;
					rot = Quaternion.Euler(0,0,0);
				} else if (CheckRandom == 2) {
					SpawnPoint.x = -pos.x;
					rot = Quaternion.Euler(0,180,0);
				}
				Instantiate (Enemy, SpawnPoint, rot);
				yield return new WaitForSeconds (spawnWait);
			}

			waveCount++;
			if(waveCount >= 3)
			{
				if(Amout < 60)
					Amout += 5;
				else if(spawnWait > 0.1)
					spawnWait -= 0.07f;
				waveCount = 0;
			}
			yield return new WaitForSeconds (waveWait);
		}
	}
}
