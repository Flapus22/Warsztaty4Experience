using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [field: SerializeField]
    private Transform CameraSlot { get; set; }

    [field: Header("Settings")]
    [field: SerializeField]
    private float MoveSpeed { get; set; }

    [field: SerializeField]
    private Vector2 MinPosition { get; set; }
    [field: SerializeField]
    private Vector2 MaxPosition { get; set; }
    private float XPosition { get; set; }
    private float ZPosition { get; set; }

    protected virtual void Start()
    {
        Initialize();
    }

    protected virtual void Update()
    {
        MoveCamera();
    }

    private void Initialize()
    {
        XPosition = CameraSlot.position.x;
        ZPosition = CameraSlot.position.z;
    }

    private void MoveCamera()
    {
        var temp = MoveSpeed * Time.deltaTime;
        
        XPosition += Input.GetAxis("Horizontal") * temp;
        ZPosition += Input.GetAxis("Vertical") * temp;

        XPosition = Mathf.Clamp(XPosition, MinPosition.x, MaxPosition.x);
        ZPosition = Mathf.Clamp(ZPosition, MinPosition.y, MaxPosition.x);

        CameraSlot.position = new Vector3(XPosition, CameraSlot.position.y, ZPosition);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(new Vector3(MinPosition.x, 5, MinPosition.y), 2);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector3(MaxPosition.x, 5, MaxPosition.y), 2);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(new Vector3(MinPosition.x, 5, MaxPosition.y), 2);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(new Vector3(MaxPosition.x, 5, MinPosition.y), 2);
    }
}
