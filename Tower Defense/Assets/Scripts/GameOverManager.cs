using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour {

    public Button playAgainButton;

    public Image gameOverModal;

    public Text scoreContainer;

    // Use this for initialization
    void Start () {
        Debug.Log("Hide game over modal");

        // Hide game over modal on start
        gameOverModal.gameObject.SetActive(false);

        // Listen to play again
        Button btn = playAgainButton.GetComponent<Button>();
        btn.onClick.AddListener(PlayAgain);
    }
	
	// Update is called once per frame
	void Update () {
	}

    void PlayAgain ()
    {
        Debug.Log("play again");

        // Hide game over modal
        gameOverModal.gameObject.SetActive(false);
    }

    public void ShowGameOver(int score)
    {
        Debug.Log("Show game over modal");

        gameOverModal.gameObject.SetActive(true);
        scoreContainer.text = score.ToString();
    }
}
