using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Target
    {
        get; set;
    }

    public float MoveSpeed = 10;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, 
                new Vector3( Target.position.x, Target.position.y, transform.position.z), MoveSpeed * Time.deltaTime);
        }
    }
}
