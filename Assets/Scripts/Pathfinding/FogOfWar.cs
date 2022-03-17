using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


namespace SlowDev.Pathfinding
{
    public class FogOfWar : MonoBehaviour
    {

        [SerializeField] InputAction updateVisionHotkey;
        [SerializeField] Transform testPoint;
        [SerializeField] float testRadius;

        Grid grid;

        private void Start()
        {
            grid = GetComponent<Grid>();
        }

        private void OnEnable()
        {
            updateVisionHotkey.Enable();
            updateVisionHotkey.performed += TestUpdateVision;
        }

        private void OnDisable()
        {
            updateVisionHotkey.Disable();
        }

        private void TestUpdateVision(InputAction.CallbackContext callback)
        {
            UpdateUnitVision(testPoint.position, testRadius);
        }

        public void UpdateUnitVision(Vector3 point, float radius)
        {
            Debug.Log("updating unit vision");
            grid.NodesFromWorldPointRadius(point, radius);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(testPoint.position, testRadius);
        }
    }
}