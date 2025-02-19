using UnityEngine;

namespace MondayCatWorld
{
    public class CameraController : MonoBehaviour
    {
        private Transform target = null;
        private float smoothSpeed = 5f;
        [SerializeField] public Vector3 offset;

        public void SetTarget(Transform targetTr)
        {
            target = targetTr;
        }

        private void LateUpdate()
        {
            if (!target) return;

            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            smoothedPosition.x = Mathf.Clamp(smoothedPosition.x, 35, 67);
            smoothedPosition.y = Mathf.Clamp(smoothedPosition.y, -16f, 17);
            transform.position = smoothedPosition;
        }
    }
}