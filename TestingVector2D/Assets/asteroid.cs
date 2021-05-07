using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroid : MonoBehaviour
{
    public GameObject player;
    Vector3 direction;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        direction = player.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * Time.deltaTime;
    }
}
