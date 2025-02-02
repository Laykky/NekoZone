using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoleSpawner : MonoBehaviour
{
    public GameObject molePrefab; // Pr�fab de taupe
    public GameObject bombPrefab; // Pr�fab de bombe
    public float minSpawnTime = 1f; // Temps minimal entre les spawns
    public float maxSpawnTime = 3f; // Temps maximal entre les spawns
    public float moleSpawnChance = 70f; // Pourcentage de chance de spawner une taupe (par exemple, 70%)
    private Transform[] spawnPoints; // Tableau des positions de spawn (trous)
    private List<Transform> occupiedPoints = new List<Transform>(); // Liste des positions occup�es

    void Start()
    {
        // Trouver tous les objets dans la sc�ne avec le tag "Hole"
        GameObject[] holes = GameObject.FindGameObjectsWithTag("Hole");
        spawnPoints = new Transform[holes.Length];

        for (int i = 0; i < holes.Length; i++)
        {
            spawnPoints[i] = holes[i].transform;
        }

        StartCoroutine(SpawnMoles());
    }

    IEnumerator SpawnMoles()
    {
        while (true)
        {
            // Temps al�atoire entre les spawns
            float spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(spawnTime);

            // Filtrer les spawnPoints disponibles
            List<Transform> availablePoints = new List<Transform>(spawnPoints);
            availablePoints.RemoveAll(point => occupiedPoints.Contains(point));

            if (availablePoints.Count > 0)
            {
                // Position al�atoire parmi les spawnPoints disponibles
                int spawnIndex = Random.Range(0, availablePoints.Count);
                Transform spawnPoint = availablePoints[spawnIndex];

                // D�terminer le type d'objet � spawner (taupe ou bombe)
                float randomValue = Random.Range(0f, 100f);
                GameObject objectToSpawn = (randomValue < moleSpawnChance) ? molePrefab : bombPrefab;

                // Instancier l'objet au spawnPoint s�lectionn�
                GameObject spawnedObject = Instantiate(objectToSpawn, spawnPoint.position, spawnPoint.rotation);
                occupiedPoints.Add(spawnPoint);

                // Lib�rer la position apr�s un certain temps ou lorsqu'elle est d�truite
                MoleChara moleChara = spawnedObject.GetComponent<MoleChara>();
                BombChara bombChara = spawnedObject.GetComponent<BombChara>();

                if (moleChara != null)
                {
                    moleChara.SetSpawner(this, spawnPoint);
                }
                else if (bombChara != null)
                {
                    bombChara.SetSpawner(this, spawnPoint);
                }
                else
                {
                    StartCoroutine(ReleasePositionCoroutine(spawnPoint));
                }
            }
        }
    }

    IEnumerator ReleasePositionCoroutine(Transform spawnPoint)
    {
        // D�finir une dur�e pour lib�rer la position (par exemple, 3 secondes)
        yield return new WaitForSeconds(3f);
        occupiedPoints.Remove(spawnPoint);
    }

    public void ReleasePosition(Transform spawnPoint)
    {
        occupiedPoints.Remove(spawnPoint);
    }
}





