using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    Text gameOverText;
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();

        gameOverText = GetComponentInChildren<Text>();
        StartFlashing();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Respawn();
        }
    }

    public void Respawn()
    {
        player.ResetPlayer();
    }

    IEnumerator GameOverFlickerRoutine()
    {
        while (true)
        {
            gameOverText.text = "Game Over Press Space to Respawn";
            yield return new WaitForSeconds(0.5f);
            gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }

    void StartFlashing()
    {
        StopCoroutine("GameOverFlickerRoutine");
        StartCoroutine("GameOverFlickerRoutine");
    }

    void StopFlashing()
    {
        StopCoroutine("GameOverFlickerRoutine");
    }
}
