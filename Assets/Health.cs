using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class Health : NetworkBehaviour
{
    public bool destroyOnDeath;
    public const int maxHealth = 100;

    [SyncVar(hook = "OnChangeHealth")]
    public int currentHealth = maxHealth;

    public RectTransform healthBar;
    private Vector3 startPos;
    public void start()
    {

        if (isLocalPlayer)
        {
            startPos = transform.position;
        }
    }
    public void TakeDamage(int amount)
    {
        if (!isServer)
            return;

        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            if (destroyOnDeath)
            {
                Destroy(gameObject);
            }
            else
            {
                currentHealth = maxHealth;
                // called on the Server, but invoked on the Clients
                RpcRespawn();
            }
        }
    }

    void OnChangeHealth(int health)
    {
        healthBar.sizeDelta = new Vector2(health, healthBar.sizeDelta.y);
    }

    [ClientRpc]//rpc are called on the server/host and run on clients
    void RpcRespawn()
    {
        if (isLocalPlayer)//is master
        {
           
            // move back to zero location
            transform.position = startPos;
        }
    }
}