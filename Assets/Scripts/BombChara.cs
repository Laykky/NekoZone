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
        if (Input.GetMouseButtonDown(0)) // Vérifie si le bouton gauche de la souris est cliqué
        {
            // Affiche le message "ATTENTION !!" dans la console
            Debug.Log("ATTENTION !!");

            // Informer le spawner pour libérer la position
            if (spawner != null)
            {
                spawner.ReleasePosition(spawnPoint);
            }

            // Détruire l'objet lorsqu'il est cliqué
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        // Assurez-vous de libérer la position si la bombe est détruite par d'autres moyens
        if (spawner != null)
        {
            spawner.ReleasePosition(spawnPoint);
        }
    }

    IEnumerator AutoDestroy()
    {
        // Attendre 5 secondes avant de s'autodétruire
        yield return new WaitForSeconds(5f);
        // Afficher le message dans la console
        Debug.Log("autodétruit");

        // Informer le spawner pour libérer la position
        if (spawner != null)
        {
            spawner.ReleasePosition(spawnPoint);
        }

        // Détruire l'objet
        Destroy(gameObject);
    }
}

