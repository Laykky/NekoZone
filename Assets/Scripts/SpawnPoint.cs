using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private GameEntity m_currentEntity;
    private bool m_isInDelay = false;

    public void SetGameEntity(GameEntity currentEntity)
    {
        m_currentEntity = currentEntity;
    }

    public void ReleasePoint()
    {
        m_currentEntity = null;
        StartCoroutine(C_StartDelay());
    }

    private IEnumerator C_StartDelay()
    {
        m_isInDelay = true;
        yield return new WaitForSeconds(1);
        m_isInDelay = false;
    }

    public bool IsAvailable()
    {
        if (m_currentEntity != null) return false;
        if (m_isInDelay) return false;

        return true;
    }
}
