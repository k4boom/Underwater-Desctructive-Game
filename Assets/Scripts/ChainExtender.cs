using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;

public class ChainExtender : MonoBehaviour
{

	public float speed = 1;
	ObiRopeCursor cursor;
	ObiRope rope;

	void Start()
	{
		cursor = GetComponent<ObiRopeCursor>();
		rope = cursor.GetComponent<ObiRope>();
	}

	void Update()
	{
		if (Input.touchCount > 0)
			cursor.ChangeLength(rope.restLength + speed * Time.deltaTime);

		if (Input.touchCount > 1)
			cursor.ChangeLength(rope.restLength - speed * Time.deltaTime);
	}

	public void DecreaseLength()
    {
		Debug.Log("Decreasing");
		cursor.ChangeLength(rope.restLength - 20f  * Time.deltaTime);
	}

	public void IncreaseLength()
	{

	}

}
