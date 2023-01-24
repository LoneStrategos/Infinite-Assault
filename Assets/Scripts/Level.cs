using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    public static Level instance;
    string[] levels = { "TitleScreen", "InGame" };

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<Score>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
