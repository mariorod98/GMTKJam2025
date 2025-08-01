using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
	[SerializeField] List<Rigidbody> m_pickedRbs = new List<Rigidbody>();
	[SerializeField] LayerMask m_mouseLayerMask;
	[SerializeField] float m_dragForceMultiplier = 10f; // approach 2
	[SerializeField] float m_dragHeight = 1f; // approach 3
	[SerializeField] float m_pickSphereSize = 0.5f; 
 
	// Start is called before the first frame update
	void Start()
	{

	}

	private void Update()
	{
		if (Input.GetMouseButton(0))
		{
			if (m_pickedRbs.Count == 0)
			{
				OnMouseDown();
			}
		}
		else if(Input.GetMouseButtonUp(0)) 
		{
			OnMouseUp();
		}
	}

	private void FixedUpdate()
	{
		if (m_pickedRbs.Count != 0 && Input.GetMouseButton(0))
		{
			OnMouseDrag();
		}
	}


	private void OnMouseDown()
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		if (Physics.SphereCast(ray, 0.05f, out hit, Mathf.Infinity, m_mouseLayerMask))
		{
			Vector3 sphereCenter = hit.point;
			LoopColor color = hit.rigidbody.GetComponent<Loop>().m_loopColor;

			Collider[] loops = Physics.OverlapSphere(sphereCenter, m_pickSphereSize, m_mouseLayerMask);

			foreach(Collider loopCollider in loops)
			{
                if(loopCollider.GetComponent<Loop>().m_loopColor == color)
				{
					Rigidbody rb = loopCollider.attachedRigidbody;
                    m_pickedRbs.Add(rb);
                    rb = hit.rigidbody;
                    rb.useGravity = false;
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                }
            }
		}
	}

	private void OnMouseDrag()
	{
		Vector3 mouseWorldPos = GetMouseWorldPosition();
		//Vector3 direction = (mouseWorldPos - m_pickedRb.position);
		
		// Approach 1
		//m_pickedRb.position += direction;

		// Approach 2
		//m_pickedRb.AddForce(direction * m_dragForceMultiplier, ForceMode.Force);
		//Debug.Log("force = " + (direction * m_dragForceMultiplier).ToString());

		// Approach 3
		//Vector3 targetPosition = new Vector3(mouseWorldPos.x, m_dragHeight, mouseWorldPos.z);
		//Vector3 direction = targetPosition - m_pickedRb.position;
		
		Vector3 targetPosition = new Vector3(mouseWorldPos.x, m_dragHeight, mouseWorldPos.z);
		foreach(Rigidbody rb in m_pickedRbs)
		{
			Vector3 direction = targetPosition - rb.position;

            // Snap without building up momentum
            rb.MovePosition(targetPosition);
		}

	}

	private void OnMouseUp()
	{
        foreach (Rigidbody rb in m_pickedRbs)
        {
            rb.useGravity = true;
		}

        m_pickedRbs.Clear();
    }

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(GetMouseWorldPosition(), 0.5f);
	}

	private Vector3 GetMouseWorldPosition()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		Plane dragPlane = new Plane(Vector3.up, Vector3.up * 3.5f); // Adjust plane as needed
		if (dragPlane.Raycast(ray, out float distance))
		{
			return ray.GetPoint(distance);
		}

		return Vector3.zero;
	}
}
