using UnityEngine;

public class GameCanvas : MonoBehaviour
{
    public GameObject GameMenu_canvas;
    public GameObject GameOver_canvas;
    public GameObject GameVictory_canvas;

    /*private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }*/

    // Ocultamos por defecto los canvas de GameOver y GameVictory para que se ejecuten cuando toque
    private void Start()
    {
        GameOver_canvas.SetActive(false);
        GameVictory_canvas.SetActive(false);
        GameMenu_canvas.SetActive(true);
    }
    
    // Creamos estas dos funciones para poder activar y desactivar el canvas de GameOver y GameVictory
    public void ShowGameOver(bool showGameOver) {
        GameOver_canvas.SetActive(showGameOver);
        //Time.timeScale = 0.0f;
    }

    public void ShowGameVictory(bool showGameVictory) {
        GameVictory_canvas.SetActive(showGameVictory);
        //Time.timeScale = 0.0f;
    }

    public void ShowGameMenu(bool showGameMenu) {
        GameMenu_canvas.SetActive(showGameMenu);
        //Time.timeScale = 0.0f;
    }
}
