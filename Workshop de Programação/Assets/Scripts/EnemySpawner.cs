using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float cooldown;
    [SerializeField] private float minCooldown;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private List<EnemySO> enemyList;

    private void Start()
    {
        StartCoroutine(SpawnerCoroutine());
        StartCoroutine(DifficultyCoroutine());
    }

    private IEnumerator SpawnerCoroutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(cooldown);

            Vector3 position = new Vector3(8, (int) Random.Range(-4,5), 0);
            int index = Random.Range(0, enemyList.Count);

            GameObject enemy = Instantiate(enemyPrefab, position, transform.rotation * Quaternion.Euler(0f, 0f, 90f));
            enemy.GetComponent<EnemyController>().FillValues(enemyList[index]);
        }
    }

    private IEnumerator DifficultyCoroutine()
    {
        while (cooldown > minCooldown)
        {
            yield return new WaitForSeconds(cooldown);
            cooldown *= 0.975f;
        }
        cooldown = minCooldown;
    }
}
