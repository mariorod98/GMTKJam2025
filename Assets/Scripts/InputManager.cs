using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
	[SerializeField] Rigidbody _pickedRb = null;
	[SerializeField] LayerMask _mouseLayerMask;
	[SerializeField] float _dragForceMultiplier = 10f; // approach 2
	[SerializeField] float _dragHeight = 1f; // approach 3
 
	// Start is called before the first frame update
	void Start()
	{

	}

	private void Update()
	{
		if (Input.GetMouseButton(0))
		{
			if (_pickedRb == null)
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
		if (_pickedRb != null && Input.GetMouseButton(0))
		{
			OnMouseDrag();
		}
	}


	private void OnMouseDown()
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		if (Physics.Raycast(ray, out hit, Mathf.Infinity, _mouseLayerMask))
		{
			_pickedRb = hit.rigidbody;

            _pickedRb = hit.rigidbody;
            _pickedRb.useGravity = false;
            _pickedRb.velocity = Vector3.zero;
            _pickedRb.angularVelocity = Vector3.zero;
        }
	}

	private void OnMouseDrag()
	{
		Vector3 mouseWorldPos = GetMouseWorldPosition();
		//Vector3 direction = (mouseWorldPos - _pickedRb.position);
		
		// Approach 1
		//_pickedRb.position += direction;

		// Approach 2
        //_pickedRb.AddForce(direction * _dragForceMultiplier, ForceMode.Force);
        //Debug.Log("force = " + (direction * _dragForceMultiplier).ToString());

		// Approach 3
        Vector3 targetPosition = new Vector3(mouseWorldPos.x, _dragHeight, mouseWorldPos.z);
        Vector3 direction = targetPosition - _pickedRb.position;

        // Snap without building up momentum
        _pickedRb.MovePosition(targetPosition);
    }

	private void OnMouseUp()
	{
		if (_pickedRb)
		{
			_pickedRb.useGravity = true;
			_pickedRb = null;
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
