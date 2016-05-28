using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class enemySpawner : NetworkBehaviour
{


    public GameObject enemyPrefab;
    public int numberOfEnemies;
    public float radius = 4;
    public override void OnStartServer()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            var spawnPosition = new Vector3(
                Random.Range(-radius, radius),
                0.0f,
                Random.Range(-radius, radius));

            var spawnRotation = Quaternion.Euler(
                0.0f,
                Random.Range(0, 180),
                0.0f);

            var enemy = (GameObject)Instantiate(enemyPrefab, spawnPosition, spawnRotation);
            NetworkServer.Spawn(enemy);
        }
    }
}
