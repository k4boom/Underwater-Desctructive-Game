using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;
public class LogOnCollision : MonoBehaviour
{
    public ObiSolver solver;
    public ObiCollider collider;
	public Joint joint;
	public float breakForce = 0.5f;
	private float firstForceValue = -1f;
	private bool aboutToBeAssigned = false;
	private bool shouldFractured = false;
	private void OnCollisionEnter(Collision collision)
    {
    }

	void Awake()
	{
		joint = GetComponent<Joint>();
	}

	private void Update()
    {
		
		
    }

    
	

	void OnEnable()
	{
		if(solver)	solver.OnCollision += Solver_OnCollision;
	}

	void OnDisable()
	{
		if (solver) solver.OnCollision -= Solver_OnCollision;
	}

	private bool first = true;
	void Solver_OnCollision(object sender, Obi.ObiSolver.ObiCollisionEventArgs e)
	{
		var world = ObiColliderWorld.GetInstance();

		// just iterate over all contacts in the current frame:
		foreach (Oni.Contact contact in e.contacts)
		{
			// if this one is an actual collision:
			if (contact.distance < 0.01)
			{
				ObiColliderBase col = world.colliderHandles[contact.bodyB].owner;
				//world.colliderShapes[contact.bodyB].o
				if (col != null)
				{
					//do it
					//Debug.Log(col.transform.position);
					Debug.DrawRay(col.transform.position, (Vector3)contact.pointA * 15, Color.red);
					//Debug.Log(contact.pointA);
					col.gameObject.GetComponent<Rigidbody>().AddForce((Vector3)contact.pointA * 10f);
					if (PlayerController.Instance.mode == PlayerController.PlayerMode.DrawBack && first && col.gameObject.GetComponent<Joint>())
					{
						StartCoroutine(BreakJoint(col));
					}
					Debug.DrawRay((Vector3)contact.pointA, Vector3.up * 15, Color.red);
					//col.gameObject.GetComponent<Rigidbody>().AddForce()
				}
			}
		}
	}

	IEnumerator BreakJoint(ObiColliderBase col)
    {
		yield return new WaitForSeconds(2f);
		col.gameObject.GetComponent<Joint>().breakForce = 150;
	}



}
