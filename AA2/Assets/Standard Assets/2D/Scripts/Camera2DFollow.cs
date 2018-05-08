using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class Camera2DFollow : MonoBehaviour
    {
        public float cameraLeftLimit = 14; //canviar quan calgui
        //public float cameraDownLimit = -1;
        //public float cameraUpLimit = 0;
        public float cameraRightLimit = 100;

        public GameObject target;
        public float damping = 1;
        public float lookAheadFactor = 3;
        public float lookAheadReturnSpeed = 0.5f;
        public float lookAheadMoveThreshold = 0.1f;

        private float m_OffsetZ;
        private Vector3 m_LastTargetPosition;
        private Vector3 m_CurrentVelocity;
        private Vector3 m_LookAheadPos;

        // Use this for initialization
        private void Start()
        {
            if (target.gameObject != null)
            {
                m_LastTargetPosition = target.transform.position;
                m_OffsetZ = (transform.position - target.transform.position).z;
                transform.parent = null;
            }
            else
                Debug.Log("traget is not attached!");
        }


        // Update is called once per frame
        private void Update()
        {
            // only update lookahead pos if accelerating or changed direction
            if (target.gameObject != null)
            {
                float xMoveDelta = (target.transform.position - m_LastTargetPosition).x;

                bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

                if (updateLookAheadTarget)
                {
                    m_LookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign(xMoveDelta);
                }
                else
                {
                    m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);
                }

            
                Vector3 aheadTargetPos = target.transform.position + m_LookAheadPos + Vector3.forward * m_OffsetZ;
                Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);


                if (newPos.x < cameraLeftLimit)
                    newPos = new Vector3(cameraLeftLimit, newPos.y, newPos.z);
                //if (newPos.y < cameraDownLimit)
                //    newPos = new Vector3(newPos.x, cameraDownLimit, newPos.z);
                //if (newPos.y > cameraUpLimit)
                //    newPos = new Vector3(newPos.x, cameraUpLimit, newPos.z);
                if (newPos.x > cameraRightLimit)
                    newPos = new Vector3(cameraRightLimit, newPos.y, newPos.z);

                transform.position = new Vector3(newPos.x, transform.position.y, transform.position.z);

                m_LastTargetPosition = target.transform.position;
            }
        }
    }
}
