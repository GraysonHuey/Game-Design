using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PipeManager : MonoBehaviour
{
    public float speed = 5f;

    private List<Rigidbody2D> pipeRBs = new List<Rigidbody2D>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (GameObject pipes in GameObject.FindGameObjectsWithTag("Pipe"))
        {
            pipeRBs.AddRange(pipes.GetComponentsInChildren<Rigidbody2D>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Rigidbody2D rb in pipeRBs) {
            rb.linearVelocity = new Vector3(-speed, 0, 0);
        }
    }
}
