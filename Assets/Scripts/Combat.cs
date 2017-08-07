using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Combat : NetworkBehaviour
{
    public const int maxHealth = 100;
    public bool destroyOnDeath;
    public List<GameObject> positions;

    [SyncVar]
    public int health = maxHealth;

    public void TakeDamage(int amount)
    {
        if (!isServer)
            return;

        health -= amount;
        if (health <= 0)
        {
            if (destroyOnDeath)
            {
                Destroy(gameObject);
            }
            else
            {
                health = maxHealth;

                // called on the server, will be invoked on the clients
                RpcRespawn();
            }
        }
    }

    [ClientRpc]
    void RpcRespawn()
    {
        if (isLocalPlayer)
        {
            int idx = Random.Range(0, positions.Count);
            // move back to default location
            transform.position = positions[idx].transform.localPosition;
        }
    }
}