using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parking_spot : MonoBehaviour
{
    [SerializeField] Car_agent car;
    [SerializeField] Road road;
    Renderer renderer1;
    float distance;
    void Start()
    {
        renderer1 = road.GetComponent<Renderer>();
        renderer1.material.color = Color.gray;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(car.transform.localPosition,transform.localPosition);
        if(distance<2){
            renderer1.material.color = Color.green;
        }
    }
}
