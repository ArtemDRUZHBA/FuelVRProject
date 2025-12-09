using UnityEngine;
using UnityEngine.UI;

public class RayCastOutline : MonoBehaviour
{
    [SerializeField] private Camera _playerCamera;
    private float _maxRayDistance = 4f;
    private Outline _lastOutlineObject;

    void Update()
    {
        Debug.DrawRay(_playerCamera.transform.position, _playerCamera.transform.forward * _maxRayDistance, Color.green);

        RaycastHit hit;

        if (Physics.Raycast(_playerCamera.transform.position, _playerCamera.transform.forward, out hit, _maxRayDistance))
        {
            if (hit.transform.gameObject.CompareTag("Item"))
            {
                if (_lastOutlineObject != null)
                    _lastOutlineObject.enabled = false;

                _lastOutlineObject = hit.transform.gameObject.GetComponent<Outline>();
                _lastOutlineObject.enabled = true;
            }
            else if (_lastOutlineObject != null)
            {
                _lastOutlineObject.enabled = false;
                _lastOutlineObject = null;
            }
        }
    }
}
