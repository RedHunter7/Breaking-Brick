using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public float kecepatan;
    public string axis;
    public float batasKiri;
    public float batasKanan;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float gerak = Input.GetAxis(axis) * kecepatan * Time.deltaTime;
        float nextPos = transform.position.x + gerak;
        
        if (nextPos > batasKanan) {
            gerak = 0;
        }
        if (nextPos < batasKiri) {
            gerak = 0;
        }

        transform.Translate(gerak,0,0);   
    }
}
