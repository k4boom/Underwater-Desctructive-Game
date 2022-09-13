using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Stabbing : MonoBehaviour
{
    [SerializeField] private string stabTag = "Stab";
    [SerializeField] private BuildingManager bm;
    private GameObject stabbedObject;
    async private void OnCollisionEnter(Collision collision)
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
    }
}
