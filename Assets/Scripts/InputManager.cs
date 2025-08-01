using System.Collections.Generic;
using UnityEngine;


public class InputManager : MonoBehaviour
{
    [SerializeField] private LayerMask m_mouseLayerMask;
    [SerializeField] private float m_pickSphereSize = 0.5f;
    // how fast do we try to go to mouse pos
    [SerializeField] private float m_snapSpeed = 100f;
    // how strong do we want the force after releasing
    [SerializeField] private float m_releaseVelocityFactor = 0.2f;
    // at what height do we want the drag plane
    [SerializeField] private float m_dragPlaneHeight = 1f;

    private Plane m_dragPlane;
    private bool m_isDragging;
    private Vector3 m_initialHitPoint;

    private class DragData
    {
        public Rigidbody m_rb;
        // offset pos between the rb and the pos where the mouse raycast hit
        // used to keep the relative pos of the object
        public Vector3 m_relativePosOffset;
        // target pos move when dragging, keeps the relative offset
        public Vector3 m_targetPos;
        // velocity obtained from moving the rb to targetPos, used to compute
        // fake velocity when dropping
        public Vector3 m_velocityLastUpdate;
    }

    private List<DragData> m_draggedObjects = new List<DragData>();

    private void Update()
    {
        if (!m_isDragging && Input.GetMouseButtonDown(0))
        {
            StartDragging();
        }

        if (m_isDragging)
        {
            UpdateDragTargets();
        }

        if (m_isDragging && Input.GetMouseButtonUp(0))
        {
            EndDragging();
        }
    }

    void FixedUpdate()
    {
        if (m_isDragging)
        {
            Drag(Time.fixedDeltaTime);
        }
    }

    void StartDragging()
    {
        // Perform a raycast to see if you collide with a loop

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.SphereCast(ray, 0.01f, out hit, Mathf.Infinity, m_mouseLayerMask))
        {
            m_initialHitPoint = hit.point;
            LoopColor color = hit.rigidbody.GetComponent<Loop>().m_loopColor;

            Vector3 planePoint = hit.point;
            planePoint.y = m_dragPlaneHeight;
            m_dragPlane = new Plane(-Camera.main.transform.forward, planePoint);

            // If so, take all the loops of equal type in the vicinity
            Collider[] hits = Physics.OverlapSphere(
                m_initialHitPoint, m_pickSphereSize, m_mouseLayerMask);
            foreach (Collider loopCollider in hits)
            {
                if (loopCollider.GetComponent<Loop>().m_loopColor == color)
                {
                    Rigidbody rb = loopCollider.attachedRigidbody;
                    DragData data = new DragData
                    {
                        m_rb = rb,
                        m_relativePosOffset = rb.position - m_initialHitPoint,
                        m_targetPos = rb.position,
                        m_velocityLastUpdate = Vector3.zero
                    };
                    rb.isKinematic = true;
                    m_draggedObjects.Add(data);
                }
            }

            if (m_draggedObjects.Count > 0)
                m_isDragging = true;
        }
    }

    void UpdateDragTargets()
    {
        Camera cam = Camera.main;
        if (cam == null) return;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (m_dragPlane.Raycast(ray, out float enter))
        {
            Vector3 worldPoint = ray.GetPoint(enter);
            foreach (DragData data in m_draggedObjects)
            {
                data.m_targetPos = worldPoint + data.m_relativePosOffset;
            }
        }
    }

    void Drag(float dt)
    {
        foreach (DragData data in m_draggedObjects)
        {
            Vector3 current = data.m_rb.position;
            Vector3 target = data.m_targetPos;
            Vector3 nextPos = Vector3.MoveTowards(current, target, m_snapSpeed * dt);

            data.m_velocityLastUpdate = (nextPos - current) / dt;
            data.m_rb.position = nextPos;
        }
    }

    void EndDragging()
    {
        m_isDragging = false;

        foreach (DragData data in m_draggedObjects)
        {
            data.m_rb.isKinematic = false;
            data.m_rb.velocity = data.m_velocityLastUpdate * m_releaseVelocityFactor;
        }

        m_draggedObjects.Clear();
    }
}
