using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    private PlayerLogic playerLogic;

    public Text pointsText;

    void OnEnable()
    {
        playerLogic = FindObjectOfType<PlayerLogic>();

        if (playerLogic == null)
            throw new MissingReferenceException("Can't find object with PlayerLogic script");

        pointsText.text = "Score: " + playerLogic.GetPoints().ToString();
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
