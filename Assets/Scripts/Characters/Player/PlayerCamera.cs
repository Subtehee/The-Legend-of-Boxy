using UnityEngine;

namespace Characters.Player
{
    public class PlayerCamera : MonoBehaviour
    {
        public float rotationSpeed = 0.0f;      // current mouse input speed
        public float smoothDamp = 10.0f;
        public float collisionRadius = 0.25f;
        public float targetLength = 3.0f;

        public LayerMask collisionMask = 0;     // collision with static mesh

        public Transform rig = null;
        public Transform pivot = null;
        public Transform socket = null;
        public Camera Camera;

        private Vector3 socketVelocity;     // Use for smoothDamp
        private Vector3 rigVelocity;

        // Set CameraRig position
        public void SetTargetPosition(Vector3 targetPosition)
        {
            rig.position = Vector3.SmoothDamp(rig.position, targetPosition, 
                ref rigVelocity, smoothDamp * Time.fixedDeltaTime);
        }

        public void SetCameraRotation(Vector2 cameraRotation)
        {
            // Y Rotation (Yaw Rotation)
            Quaternion rigTargetLocalRotation = Quaternion.Euler(0.0f, cameraRotation.y, 0.0f);

            // X Rotation (Pitch Rotation)
            Quaternion pivotTargetLocalRotation = Quaternion.Euler(cameraRotation.x, 0.0f, 0.0f);

            if(rotationSpeed > 0.0f)
            {
                rig.rotation = Quaternion.Slerp(rig.rotation, rigTargetLocalRotation, rotationSpeed * Time.fixedDeltaTime);
                pivot.rotation = Quaternion.Slerp(pivot.rotation, pivotTargetLocalRotation, rotationSpeed * Time.fixedDeltaTime);
            }
            else
            {
                rig.rotation = rigTargetLocalRotation;
                pivot.rotation = pivotTargetLocalRotation;
            }
        }

        public void UpdateSocketPosition()
        {
            float targetLength = GetTargetLength();

            Vector3 newPosition = -Vector3.forward * targetLength;

            socket.localPosition = Vector3.SmoothDamp(socket.localPosition, newPosition,
                ref socketVelocity, smoothDamp * Time.fixedDeltaTime);
        }

        // Set camera position length from target
        private float GetTargetLength()
        {
            Ray ray = new Ray(transform.position, -transform.forward);
            RaycastHit hit;

            if (Physics.SphereCast(ray, collisionRadius, out hit, targetLength, collisionMask))
                return hit.distance;

            return targetLength;
        }

        // Draw Gizoms
        private void OnDrawGizmos()
        {
            if (socket != null)
            {
                Gizmos.color = Color.green;

                Gizmos.DrawLine(transform.position, socket.position);
                DrawGizmoSphere(transform.position, collisionRadius);
            }
        }

        // Draw gizom sphere
        private void DrawGizmoSphere(Vector3 pos, float radius)
        {
            Quaternion rotation = Quaternion.Euler(-90.0f, 0.0f, 0.0f); 

            int alphaSteps = 8;
            int betaSteps = 16;

            float deltaAlpha = Mathf.PI / alphaSteps;
            float deltaBeta = 2 * Mathf.PI / betaSteps;

            for(int a = 0; a < alphaSteps; a++)
            {
                for(int b = 0; b < betaSteps; b++)
                {
                    float alpha = a * deltaAlpha;
                    float beta = b * deltaBeta;

                    Vector3 p1 = pos + rotation * GetSphericalPoint(alpha, beta, radius);
                    Vector3 p2 = pos + rotation * GetSphericalPoint(alpha + deltaAlpha, beta, radius);
                    Vector3 p3 = pos + rotation * GetSphericalPoint(alpha + deltaAlpha, beta + deltaBeta, radius);

                    // draw gizmo lines
                    Gizmos.DrawLine(p1, p2);
                    Gizmos.DrawLine(p1, p3);
                }
            }
        }

        // Get vertex for draw gizmo line
        private Vector3 GetSphericalPoint(float alpha, float beta, float radius)
        {
            Vector3 point;
            point.x = radius * Mathf.Sin(alpha) * Mathf.Cos(beta);
            point.y = radius * Mathf.Sin(alpha) * Mathf.Sin(beta);
            point.z = radius * Mathf.Cos(alpha);

            return point;
        }
    }

}
