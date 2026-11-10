using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerDeath : MonoBehaviour
{
    bool isDead;

    public void Die()
    {
        if (isDead) return;
        isDead = true;

        GetComponent<PlayerMotor>().enabled = false;
        GetComponent<PlayerInteractor>().enabled = false;

        Respawn();
    }

    public void Respawn()
    {
        isDead = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
