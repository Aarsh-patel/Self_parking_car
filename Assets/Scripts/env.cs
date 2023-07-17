using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class env : MonoBehaviour
{
    [SerializeField] Car_agent car;
    [SerializeField] Parking_spot parking_Spot;
    private Transform car1;

    private Transform cars= null;
    private int random = 0;
    private int rand_om;
    public void ResetEnv(){
        if (cars != null){
            cars.gameObject.SetActive(true);
        }
        ResetCars();
        if (random == 0){
            random++;
        }
        else{
            random--;
        }
        rand_om = (random*4) + Random.Range(0,4);
        cars = transform.GetChild(rand_om);
        Vector3 temp = cars.transform.localPosition;
        cars.gameObject.SetActive(false);
        temp.y += 0.06f;
        parking_Spot.transform.localPosition = temp;
        car.dist = Vector3.Distance(temp,transform.localPosition);
    }
    private void ResetCars(){
        Rigidbody te;
        car1 = transform.GetChild(0);
        te = car1.GetComponent<Rigidbody>();
        te.velocity = Vector3.zero;
        car1.transform.localPosition = new Vector3(10.3f,0f,14.69f);
        car1.transform.eulerAngles = new Vector3(0f,90f,0f);
        car1 = transform.GetChild(1);
        te = car1.GetComponent<Rigidbody>();
        te.velocity = Vector3.zero;
        car1.transform.localPosition = new Vector3(10.3f,0f,10.85f);
        car1.transform.eulerAngles = new Vector3(0f,90f,0f);
        car1 = transform.GetChild(2);
        te = car1.GetComponent<Rigidbody>();
        te.velocity = Vector3.zero;
        car1.transform.localPosition = new Vector3(10.3f,0f,6.68f);
        car1.transform.eulerAngles = new Vector3(0f,90f,0f);
        car1 = transform.GetChild(3);
        te = car1.GetComponent<Rigidbody>();
        te.velocity = Vector3.zero;
        car1.transform.localPosition = new Vector3(10.3f,0f,2.4f);
        car1.transform.eulerAngles = new Vector3(0f,90f,0f);
        car1 = transform.GetChild(4);
        te = car1.GetComponent<Rigidbody>();
        te.velocity = Vector3.zero;
        car1.transform.localPosition = new Vector3(-10.87f,0f,14.69f);
        car1.transform.eulerAngles = new Vector3(0f,270f,0f);
        car1 = transform.GetChild(5);
        te = car1.GetComponent<Rigidbody>();
        te.velocity = Vector3.zero;
        car1.transform.localPosition = new Vector3(-10.87f,0f,10.85f);
        car1.transform.eulerAngles = new Vector3(0f,270f,0f);
        car1 = transform.GetChild(6);
        te = car1.GetComponent<Rigidbody>();
        te.velocity = Vector3.zero;
        car1.transform.localPosition = new Vector3(-10.87f,0f,6.68f);
        car1.transform.eulerAngles = new Vector3(0f,270f,0f);
        car1 = transform.GetChild(7);
        te = car1.GetComponent<Rigidbody>();
        te.velocity = Vector3.zero;
        car1.transform.localPosition = new Vector3(-10.87f,0f,2.4f);
        car1.transform.eulerAngles = new Vector3(0f,270f,0f);
    }
}
