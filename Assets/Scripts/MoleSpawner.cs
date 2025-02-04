using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MoleSpawner : MonoBehaviour
{
    public List<GameEntity> m_gameEntities = new List<GameEntity>();

    public float minSpawnTime = 1f; // Temps minimal entre les spawns
    public float maxSpawnTime = 3f; // Temps maximal entre les spawns
    public float moleSpawnChance = 70f; // Pourcentage de chance de spawner une taupe (par exemple, 70%)
    private List<SpawnPoint> spawnPoints = new List<SpawnPoint>(); // Tableau des positions de spawn (trous)

    void Start()
    {
        spawnPoints = GetComponentsInChildren<SpawnPoint>().ToList();
        StartCoroutine(SpawnMoles());
    }

    IEnumerator SpawnMoles()
    {
        while (true)
        {
            // Temps aléatoire entre les spawns
            float spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(spawnTime);

            // Filtrer les spawnPoints disponibles
            List<SpawnPoint> availablePoints = spawnPoints.FindAll(x => x.IsAvailable());

            if (availablePoints.Count > 0)
            {
                // Position aléatoire parmi les spawnPoints disponibles
                int spawnIndex = Random.Range(0, availablePoints.Count);
                SpawnPoint spawnPoint = availablePoints[spawnIndex];

                // Déterminer le type d'objet à spawner (taupe ou bombe)
                GameEntity objectToSpawn = GetWeightedEntity();

                // Instancier l'objet au spawnPoint sélectionné
                GameEntity spawnedObject = Instantiate(objectToSpawn, spawnPoint.transform.position, spawnPoint.transform.rotation);
                spawnedObject.SetSpawner(this, spawnPoint);
            }
        }
    }

    public GameEntity GetWeightedEntity()
    {
        int totalSum = 0;
        for (int i = 0; i < m_gameEntities.Count; i++)
        {
            totalSum += m_gameEntities[i].SpawnWeight;
        }
        int randomWeight = Random.Range(0, totalSum);

        for (int i = 0; i < m_gameEntities.Count; i++)
        {
            randomWeight -= m_gameEntities[i].SpawnWeight;
            if (randomWeight < 0)
            {
                return m_gameEntities[i];
            }
        }

        return null;
    }
}





