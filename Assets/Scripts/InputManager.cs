using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
	[SerializeField] Rigidbody m_pickedRb = null;
	[SerializeField] LayerMask m_mouseLayerMask;
	[SerializeField] float m_dragForceMultiplier = 10f; // approach 2
	[SerializeField] float m_dragHeight = 1f; // approach 3
 
	// Start is called before the first frame update
	void Start()
	{

	}

	private void Update()
	{
		if (Input.GetMouseButton(0))
		{
			if (m_pickedRb == null)
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
		if (m_pickedRb != null && Input.GetMouseButton(0))
		{
			OnMouseDrag();
		}
	}


	private void OnMouseDown()
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		if (Physics.Raycast(ray, out hit, Mathf.Infinity, m_mouseLayerMask))
		{
			m_pickedRb = hit.rigidbody;

			m_pickedRb = hit.rigidbody;
			m_pickedRb.useGravity = false;
			m_pickedRb.velocity = Vector3.zero;
			m_pickedRb.angularVelocity = Vector3.zero;
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
		Vector3 targetPosition = new Vector3(mouseWorldPos.x, m_dragHeight, mouseWorldPos.z);
		Vector3 direction = targetPosition - m_pickedRb.position;

		// Snap without building up momentum
		m_pickedRb.MovePosition(targetPosition);
	}

	private void OnMouseUp()
	{
		if (m_pickedRb)
		{
			m_pickedRb.useGravity = true;
			m_pickedRb = null;
		}
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
