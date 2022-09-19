using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Transform propeller;
    [SerializeField] private float rotationOffset = 5f;


    private void Update()
    {
        PropellerRotation();
    }
    public void MoveWithMouseInput()
    {
        transform.position += -Vector3.forward * moveSpeed * Time.deltaTime;
    }



    void PropellerRotation()
    {
        propeller.Rotate(Vector3.forward * Time.deltaTime * rotationOffset);
    }
}
