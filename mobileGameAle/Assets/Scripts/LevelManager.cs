using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // [SerializeField] private GameObject GameOver_canvas;
    public GameCanvas gameCanvas;
    public static LevelManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }    
    }

    /* private void Start()
    {
        GameOver_canvas.SetActive(false);
    } */

    public void ShowGameOver() {
        // GameOver_canvas.SetActive(true);
        
        gameCanvas.ShowGameOver(true);
        Time.timeScale = 0.0f;
    }

     public void ShowGameVictory() {
        // GameOver_canvas.SetActive(true);
        gameCanvas.ShowGameVictory(true);
        Time.timeScale = 0.0f;
    }

    // Update is called once per frame
    public void RestartLevel() {
        Time.timeScale = 1.0f;
        gameCanvas.ShowGameOver(false);
        gameCanvas.ShowGameVictory(false);
        gameCanvas.ShowGameMenu(false);
        //SceneManager.LoadScene("Nivel_1");
        GameManager.instance.LoadNivel();
    }

    public void BackToMenu() {
        Time.timeScale = 1.0f;
        gameCanvas.ShowGameOver(false);
        gameCanvas.ShowGameVictory(false);
        gameCanvas.ShowGameMenu(true);
        SceneManager.LoadScene("menu");
    }
}
