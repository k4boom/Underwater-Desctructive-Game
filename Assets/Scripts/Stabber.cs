using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Stabber : MonoBehaviour
{
    [SerializeField] private string stabTag = "DestroyableBuilding";
    [SerializeField] private string stabTag2 = "Floor";
    private GameObject stabbedObject;
    [SerializeField] private Material insideMat;
    [SerializeField] private ParticleSystem particle;
    public Joint sj;

    private void Update()
    {
        if (stabbedObject) { 

            if (PlayerController.Instance.mode == PlayerController.PlayerMode.DrawBack && sj == null && !stabbedObject.transform.CompareTag("Stab"))
            {
                StartCoroutine("LeaveStabbedObject");
                //ChangeTag();

                stabbedObject.AddComponent<BoxCollider>();
                stabbedObject.transform.tag = "Stab";
                particle.Play();
            }
        }
    }

    IEnumerator LeaveStabbedObject()
    {
        yield return new WaitForSeconds(2f);
        stabbedObject.transform.SetParent(null);
    }

    private void OnCollisionStay(Collision collision)
    {
        if ((collision.transform.CompareTag(stabTag) || collision.transform.CompareTag(stabTag2)) && stabbedObject == null)
        {
            
            stabbedObject = collision.gameObject;

            stabbedObject.transform.SetParent(transform);

            collision.rigidbody.mass = 0.01f;

            //await Task.Delay(1000);
            PlayerController.Instance.mode = PlayerController.PlayerMode.DrawBack;
            PlayerController.Instance.ChangeCanBreak();
            
            sj = gameObject.AddComponent<FixedJoint>();
            //sj.connectedBody = rb;
            sj.breakForce = 5500; // WhenIdle breaks at 150

            stabbedObject.GetComponent<Fracture>().isEnabled = true;

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
