using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    Text startGameText;

    // Start is called before the first frame update
    void Start()
    {
        startGameText = GetComponentInChildren<Text>();
        StartFlashing();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("InGame");
    }

    IEnumerator StartGameFlickerRoutine()
    {
        while (true)
        {
            startGameText.text = "Press Space to Start Game!";
            yield return new WaitForSeconds(0.35f);
            startGameText.text = "";
            yield return new WaitForSeconds(0.35f);
        }
    }

    void StartFlashing()
    {
        StopCoroutine ("StartGameFlickerRoutine");
        StartCoroutine("StartGameFlickerRoutine");
    }

    void StopFlashing()
    {
        StopCoroutine("StartGameFlickerRoutine");
    }
}
