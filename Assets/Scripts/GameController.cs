using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	public GameObject[] spawns;
	public int[] spawnRange;
	public float startWait;
	public Text score;
	private int score_count;

    // Start is called before the first frame update
    void Start()
    {
    	score_count = 0;
		StartCoroutine(SpawnWaves ());
    }

	void FixedUpdate(){
	}

	IEnumerator SpawnWaves () {
		yield return new WaitForSeconds (startWait);
		while(true){
			int choice = Random.Range(0,4);
			Vector3 spawnPosition;
			if (choice==0) {
				spawnPosition = new Vector3(Random.Range(0, Screen.width),0,1);
			} else if (choice==1) {
				spawnPosition = new Vector3(Random.Range(0, Screen.width),Screen.height,1);
			} else if (choice==2) {
				spawnPosition = new Vector3(0,Random.Range(0, Screen.height),1);
			} else {
				spawnPosition = new Vector3(Screen.width,Random.Range(0, Screen.height),1);
			}
			Quaternion spawnRotation = Quaternion.identity;
			int spawnIndex = Random.Range (0, spawns.Length);
			GameObject clone = Instantiate (spawns[spawnIndex], spawnPosition, spawnRotation);
			clone.tag = "Clone";
			int spawnWait = Random.Range(spawnRange[0], spawnRange[1]);
			yield return new WaitForSeconds(spawnWait);
		}
	}

	public void update_score(){
		score_count ++;
		score.text="Score: " + score_count;
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
