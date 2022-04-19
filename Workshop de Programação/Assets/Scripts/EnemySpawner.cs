using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnTime;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private List<Enemy> enemyList;
    private void Start()
    {
        StartCoroutine(Spawner());
        StartCoroutine(Difficulty());
    }

    private IEnumerator Spawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);

            GameObject enemy = Instantiate(enemyPrefab, new Vector3 (8, (int) Random.Range(-4,4), 0), transform.rotation * Quaternion.Euler (0f, 0f, 90f));
            int index = Random.Range (0, enemyList.Count);
            enemy.GetComponent<EnemyController>().FillValues(enemyList[index]);
        }
    }

    private IEnumerator Difficulty()
    {
        while (spawnTime > 1.5f)
        {
            yield return new WaitForSeconds(spawnTime*5);
            spawnTime *= 0.9f;
        }
    }
}
