using UnityEngine;

public class ZombieKill : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        PlayerDeath player = other.GetComponent<PlayerDeath>();
       if (player != null)
        {
            player.Die();
        }
    }
}
