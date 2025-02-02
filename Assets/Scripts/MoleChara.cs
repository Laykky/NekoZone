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
        if (Input.GetMouseButtonDown(0)) // Détruire l'objet lorsqu'il est cliqué et libération de la position qu'occupait le chat
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
        //  libérer la position si le chatchat est détruit 
        if (spawner != null)
        {
            spawner.ReleasePosition(spawnPoint);
        }
    }

    IEnumerator AutoDestroy()
    {
        // Autodestruct du chatchat si on a pas cliqué dessus, devrait passer la variable en publique ça serait mieux
        yield return new WaitForSeconds(3f);
        // Afficher le message dans la console
        Debug.Log("autodétruit");

        
        if (spawner != null)
        {
            spawner.ReleasePosition(spawnPoint);
        }

        // Détruire l'objet
        Destroy(gameObject);
    }
}


