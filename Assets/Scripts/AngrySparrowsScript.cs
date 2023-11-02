using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngrySparrowsScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += new Vector3(0, 0.1f, 0);
        this.transform.position += new Vector3(0, 0, 0.1f);
        this.transform.position += new Vector3(0, -0.1f, 0);
    }
}
