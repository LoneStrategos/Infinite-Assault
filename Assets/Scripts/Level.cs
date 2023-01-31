using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    public static Level instance;
    string[] levels = { "TitleScreen", "InGame" };
    public GameObject additionalSpawwners;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<Score>();
        StartCoroutine(ActivateSpawners());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ActivateSpawners()
    {
        yield return new WaitForSeconds(30f);
        additionalSpawwners.SetActive(true);
    }
}
