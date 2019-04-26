using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveScenarioController : MonoBehaviour {

	public Transform Player;
	public GameObject[] Waves;
	public List<GameObject> WavesInstantiated;

	List<int> IndexListOrdered;
	public int[] IndexShuffled;

	int waveIndexCurr = 0;

	public GameplaySceneController controller;

	// Use this for initialization
	void Start () {
		ShuffleIndex ();
	}

	void ShuffleIndex()
	{
		waveIndexCurr = 0;
		IndexListOrdered = new List<int> ();
		for (int i=0; i<Waves.Length; i++) {
			IndexListOrdered.Add(i);
		}
		IndexShuffled = IndexListOrdered.ToArray();

		for (int i=0; i < IndexShuffled.Length; i++) {
			int index = Random.Range(0, IndexListOrdered.Count);
			IndexShuffled[i] = IndexListOrdered[index];
			IndexListOrdered.RemoveAt(index);
		}

		controller.lifeBonusEnemy++;
	}
	
	// Update is called once per frame
	void Update () {
		if (Player != null && Player.position.x > WavesInstantiated [0].transform.position.x + 70) {
			Destroy(WavesInstantiated[0]);
			WavesInstantiated.RemoveAt(0);

			Vector3 posNewWave = WavesInstantiated[0].transform.position;
			posNewWave.x += 70;
			GameObject wave = Instantiate(Waves[IndexShuffled[waveIndexCurr]], posNewWave, Quaternion.identity) as GameObject;
			WavesInstantiated.Add(wave);

			waveIndexCurr++;
			if(waveIndexCurr >= IndexShuffled.Length)
			{	

				ShuffleIndex();
			}
		}


	}
}
