using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PipeManager : MonoBehaviour
{
    public float speed = 5f;
    public float spawnInterval = 2f;
    public GameObject pipePrefab;

    private List<GameObject> pipes = new List<GameObject>();
    private float spawnTimer = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pipes = GameObject.FindGameObjectsWithTag("Pipe").ToList();
    }

    void Update()
    {
        for (int i = pipes.Count - 1; i >= 0; i--)
        {
            GameObject pipe = pipes[i];

            if (pipe == null)
            {
                pipes.RemoveAt(i);
                continue;
            }

            pipe.transform.Translate(Vector3.left * speed * Time.deltaTime);

            if (pipe.transform.position.x < -15f)
            {
                pipes.RemoveAt(i);
                Destroy(pipe);
            }
        }

        spawnPipe();
    }

    void spawnPipe()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            spawnTimer = 0f;

            float randomY = Random.Range(-4.5f, -0.5f);
            Vector3 spawnPosition = new Vector3(12f, randomY, 0f);

            GameObject newPipe = Instantiate(pipePrefab, spawnPosition, Quaternion.identity);
            newPipe.layer = LayerMask.NameToLayer("Pipes");
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Pipes"), LayerMask.NameToLayer("Ground"), true);
            pipes.Add(newPipe);
        }
    }
}
