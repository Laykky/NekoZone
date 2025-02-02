using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BombChara : MonoBehaviour
{
    private MoleSpawner spawner;
    private Transform spawnPoint;

    public void SetSpawner(MoleSpawner spawner, Transform spawnPoint)
    {
        this.spawner = spawner;
        this.spawnPoint = spawnPoint;
    }

    void Start()
    {
        // Commence la coroutine d'autodestruction
        StartCoroutine(AutoDestroy());
    }

    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0)) // V�rifie si le bouton gauche de la souris est cliqu�
        {
            // Affiche le message "ATTENTION !!" dans la console
            Debug.Log("ATTENTION !!");

            // Informer le spawner pour lib�rer la position
            if (spawner != null)
            {
                spawner.ReleasePosition(spawnPoint);
            }

            // D�truire l'objet lorsqu'il est cliqu�
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        // Assurez-vous de lib�rer la position si la bombe est d�truite par d'autres moyens
        if (spawner != null)
        {
            spawner.ReleasePosition(spawnPoint);
        }
    }

    IEnumerator AutoDestroy()
    {
        // Attendre 5 secondes avant de s'autod�truire
        yield return new WaitForSeconds(5f);
        // Afficher le message dans la console
        Debug.Log("autod�truit");

        // Informer le spawner pour lib�rer la position
        if (spawner != null)
        {
            spawner.ReleasePosition(spawnPoint);
        }

        // D�truire l'objet
        Destroy(gameObject);
    }
}

