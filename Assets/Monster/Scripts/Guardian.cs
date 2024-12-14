using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guardian : MonoBehaviour
{
    public GameObject point;
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 70.0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(point.transform.position, Vector3.down, speed * Time.deltaTime);
    }
}
