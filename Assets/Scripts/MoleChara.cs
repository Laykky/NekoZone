using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleChara : MonoBehaviour

{
    private MoleSpawner spawner;
    private Transform spawnPoint;

    void Start()
    {
        
        StartCoroutine(AutoDestroy());
    }
    public void SetSpawner(MoleSpawner spawner, Transform spawnPoint)
    {
        this.spawner = spawner;
        this.spawnPoint = spawnPoint;
    }

    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0)) // D�truire l'objet lorsqu'il est cliqu� et lib�ration de la position qu'occupait le chat
        {
           
            if (spawner != null)
            {
                spawner.ReleasePosition(spawnPoint);
            }

           
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        //  lib�rer la position si le chatchat est d�truit 
        if (spawner != null)
        {
            spawner.ReleasePosition(spawnPoint);
        }
    }

    IEnumerator AutoDestroy()
    {
        // Autodestruct du chatchat si on a pas cliqu� dessus, devrait passer la variable en publique �a serait mieux
        yield return new WaitForSeconds(3f);
        // Afficher le message dans la console
        Debug.Log("autod�truit");

        
        if (spawner != null)
        {
            spawner.ReleasePosition(spawnPoint);
        }

        // D�truire l'objet
        Destroy(gameObject);
    }
}


