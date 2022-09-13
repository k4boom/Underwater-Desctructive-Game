using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    public void MoveWithMouseInput()
    {
        transform.position += -Vector3.forward * moveSpeed * Time.deltaTime;
    }
}
