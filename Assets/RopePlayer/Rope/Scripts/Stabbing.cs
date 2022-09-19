using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Stabbing : MonoBehaviour
{
    [SerializeField] private string stabTag = "Stab";
    [SerializeField] private string stabTag2 = "Stab";
    [SerializeField] private BuildingManager bm;
    private GameObject stabbedObject;
    async private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.CompareTag(stabTag) && stabbedObject == null)
        {
            stabbedObject = collision.gameObject;

            collision.transform.SetParent(transform);
            collision.rigidbody.mass = 0.01f;
            Destroy(PlayerController.Instance.player.GetComponent<PlayerMotor>());
            PlayerController.Instance.cam.GetComponent<PlayerCamera>().camMode = PlayerCamera.CameraMode.draw;
            await Task.Delay(1000);

            //bm.releaseWalls = true;
            //PlayerController.Instance.player.GetComponent<PlayerMotor>().limit = 0;
            PlayerController.Instance.mode = PlayerController.PlayerMode.DrawBack;
            /*
            var sp = collision.gameObject.AddComponent<FixedJoint>();
            sp.connectedBody = GetComponent<Rigidbody>();
            sp.enableCollision = true;
            */
            //SOLUTION 1
            /*
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            GetComponent<Rigidbody>().isKinematic = true;
            transform.SetParent(collision.transform);
            */
        }


        if (collision.transform.CompareTag(stabTag2) && stabbedObject == null)
        {
            stabbedObject = collision.gameObject;
            collision.transform.SetParent(transform);
            collision.rigidbody.mass = 0.01f;
            Destroy(PlayerController.Instance.player.GetComponent<PlayerMovementWithPhysics>());
            PlayerController.Instance.cam.GetComponent<PlayerCamera>().camMode = PlayerCamera.CameraMode.draw;



            await Task.Delay(1000);

            //bm.releaseWalls = true;
            //PlayerController.Instance.player.GetComponent<PlayerMotor>().limit = 0;
            PlayerController.Instance.mode = PlayerController.PlayerMode.DrawBack;
            //GetComponent<Rigidbody>().useGravity = true;
        }

    }


    async private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.CompareTag(stabTag) && stabbedObject == null)
        {
            stabbedObject = collision.gameObject;

            collision.transform.SetParent(transform);
            collision.attachedRigidbody.mass = 0.01f;
            Destroy(PlayerController.Instance.player.GetComponent<PlayerMotor>());
            PlayerController.Instance.cam.GetComponent<PlayerCamera>().camMode = PlayerCamera.CameraMode.draw;
            await Task.Delay(1000);

            //bm.releaseWalls = true;
            //PlayerController.Instance.player.GetComponent<PlayerMotor>().limit = 0;
            PlayerController.Instance.mode = PlayerController.PlayerMode.DrawBack;
            /*
            var sp = collision.gameObject.AddComponent<FixedJoint>();
            sp.connectedBody = GetComponent<Rigidbody>();
            sp.enableCollision = true;
            */
            //SOLUTION 1
            /*
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            GetComponent<Rigidbody>().isKinematic = true;
            transform.SetParent(collision.transform);
            */
        }


        if (collision.transform.CompareTag(stabTag2) && stabbedObject == null)
        {
            stabbedObject = collision.gameObject;
            collision.transform.SetParent(transform);
            collision.GetComponent<Rigidbody>().mass = 0.01f;
            Destroy(PlayerController.Instance.player.GetComponent<PlayerMotor>());
            PlayerController.Instance.cam.GetComponent<PlayerCamera>().camMode = PlayerCamera.CameraMode.draw;



            await Task.Delay(1000);

            //bm.releaseWalls = true;
            //PlayerController.Instance.player.GetComponent<PlayerMotor>().limit = 0;
            PlayerController.Instance.mode = PlayerController.PlayerMode.DrawBack;
        }

    }

    public void ChangeTag(string tag = "Stab")
    {
        transform.tag = tag;
        foreach (Transform child in transform)
        {
            child.tag = tag;
            foreach (Transform echild in child)
            {
                echild.tag = tag;

            }
        }
    }
}
