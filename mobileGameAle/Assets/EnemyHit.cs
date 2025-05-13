using Unity.VisualScripting;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    public pigCtrl pigScript;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Assuming the player has a script with a method to handle damage
            pigScript.Death();
        }
    }
}
