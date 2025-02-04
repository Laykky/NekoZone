using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEntity : MonoBehaviour
{
    [SerializeField] private int m_spawnWeight = 10;
    private MoleSpawner spawner;
    private SpawnPoint spawnPoint;

    public int SpawnWeight { get => m_spawnWeight; set => m_spawnWeight = value; }

    void Start()
    {

        StartCoroutine(AutoDestroy());
    }

    private void OnEnable()
    {
        float duration = Random.Range(0.1f, 0.3f);
        transform.DOScale(Vector3.one, duration).From(Vector3.zero);
        transform.DOPunchScale(Vector3.up * 0.2f, 0.3f).SetDelay(duration * 0.9f);
    }

    public void SetSpawner(MoleSpawner spawner, SpawnPoint spawnPoint)
    {
        this.spawner = spawner;
        this.spawnPoint = spawnPoint;
        spawnPoint.SetGameEntity(this);
    }

    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0)) // D�truire l'objet lorsqu'il est cliqu� et lib�ration de la position qu'occupait le chat
        {
            OnClick();
        }
    }

    protected virtual void OnClick()
    {
        ReleasePosition();

        Destroy(gameObject);
    }

    private void ReleasePosition()
    {
        if (spawnPoint != null)
        {
            spawnPoint.ReleasePoint();
        }
    }

    void OnDestroy()
    {
        ReleasePosition();
    }

    IEnumerator AutoDestroy()
    {
        // Autodestruct du chatchat si on a pas cliqu� dessus, devrait passer la variable en publique �a serait mieux
        yield return new WaitForSeconds(3f);
        // Afficher le message dans la console
        Debug.Log("autod�truit");

        // D�truire l'objet
        Destroy(gameObject);
    }
}
