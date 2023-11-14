using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeleScript : MonoBehaviour
{
    LevelLoader levelLoader;
    // Start is called before the first frame update
    void Start()
    {
        levelLoader = FindObjectOfType<LevelLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            levelLoader.LoadNextLevel();

            Debug.Log("We Out Here");
        }
        Debug.Log("We Extra Out Here");
    }


}
