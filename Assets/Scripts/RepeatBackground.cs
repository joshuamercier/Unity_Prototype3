using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    // Class variables
    private Vector3 startPos;
    private float repeatWidth;
    private SpawnManager spawnManagerScript;

    // Start is called before the first frame update
    void Start()
    {

        startPos = transform.position;
        if (gameObject.tag.Equals("Background"))
        {
            repeatWidth = GetComponent<BoxCollider>().size.x / 2;
        }
        else
        {
            spawnManagerScript = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
            repeatWidth = spawnManagerScript.spawnPos.x / 2;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < startPos.x - repeatWidth)
        {
            transform.position = startPos;
        }


    }
}
