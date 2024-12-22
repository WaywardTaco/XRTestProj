
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> ufoPrefabs;
    [SerializeField] private float spawnTimer;
    
    void Start(){
        StartCoroutine(TrySpawn(spawnTimer));
    }

    IEnumerator TrySpawn(float waitTime){
        yield return new WaitForSeconds(waitTime);

        int index = Random.Range(0, ufoPrefabs.Count - 1);
        GameObject.Instantiate(ufoPrefabs[index], this.transform);

        StartCoroutine(TrySpawn(spawnTimer));
    }
}
