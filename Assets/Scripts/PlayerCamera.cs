using UnityEngine;

namespace PlayerCharacter
{

    public class PlayerCamera : MonoBehaviour
    {
        public float targetLength = 3.0f;       // default distance from target
        public float rotationSpeed = 0.0f;      
        public float positionSmoothDamp = 0.0f; 
        public float collisionRadius = 0.25f;   // size of sphere collision
        public LayerMask collisionMask = 0;     // 

        public Transform rig = null;
        public Transform target = null;

        private Camera _camera;
        private Transform _cameraRig = null;
        private Transform _collisionSocket = null;

        private void LateUpdate()
        {
            _camera.transform.localPosition = -Vector3.forward * _camera.nearClipPlane; 
        }

        // Set camera distance from target
        private float GetDesiredTargetLength()
        {
            Ray ray = new Ray(transform.position, -transform.forward);
            RaycastHit hit;

            if (Physics.SphereCast(ray, Mathf.Max(0.001f, collisionRadius), out hit, targetLength, collisionMask))
            {
                return hit.distance;    
            }
            else 
            {
                return targetLength; 
            }
        }

        // Draw gizmos
        private void OnDrawGizmos()
        {
            if (_collisionSocket != null)
            {
                // Line
                Gizmos.color = Color.green;
                Gizmos.DrawLine(transform.position, _collisionSocket.transform.position);
                
            }
        }

        // Draw gizmos shpere by lines
        private void DrawGizmosSphere(Vector3 pos, float radius)
        {
            Quaternion rot = Quaternion.Euler(-90.0f, 0.0f, 0.0f);

            int alphaSteps = 8;     // half of circumference (arc)
            int betaSteps = 16;     // circumference

            float deltaAlpha = Mathf.PI / alphaSteps;        
            float deltaBeta = 2.0f * Mathf.PI / betaSteps;

            for(int a = 0; a  < alphaSteps; a++)
            {
                for(int b = 0; b < betaSteps; b++)
                {
                    float alpha = a * deltaAlpha;
                    float beta = b * deltaBeta;

                }
            }
        }
    }

}
