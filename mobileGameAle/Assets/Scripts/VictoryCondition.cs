using UnityEngine;

public class VictoryCondition : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            // Si el jugador colisiona con el objeto de victoria, se muestra la pantalla de victoria
            LevelManager.instance.ShowGameVictory();
        }
    }
}
