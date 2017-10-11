using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-------------------------------------------------------------------------------------------------------------------------------------------------------------
//
// Authors: Nicholas Teese
// Date Created: 04/08/2017
// Description: Pivots object from center
//
//-------------------------------------------------------------------------------------------------------------------------------------------------------------

public class Pivot : MonoBehaviour
{
        /// <summary>
        /// Maximum pivot angle in degrees (Only positive values)
        /// </summary>
        public float m_fMax;
        /// <summary>
        /// Minimum pivot angle in degrees (Only positive values)
        /// </summary>
        public float m_fMin;
        /// <summary>
        /// Speed at which the object pivots
        /// </summary>
        public float m_fSpeed;
        /// <summary>
        /// Bool for setting the direction of the pivot 
        /// </summary>
        public bool m_bOpen;
        /// <summary>
        /// Bool for beginning pivot
        /// </summary>
        public bool m_bTurn;

        /// <summary>
        /// Quaternion for applying rotation
        /// </summary>
        Quaternion Q_Hinge;
        /// <summary>
        /// Euler for converting rotation
        /// </summary>
        Vector3 V3_euler;

        void Awake()
        {
        m_bOpen = false;
            m_bTurn = false;
            Q_Hinge = transform.rotation;
            V3_euler = Q_Hinge.eulerAngles;
        }

        /// <summary>
        /// Function for operating pivoting object
        /// </summary>
        void GateControl()
        {
            /// <summary>
            /// If Open is true the can pivot else nothing happens
            /// </summary>
            if (m_bOpen)
            {
                /// <summary>
                /// if turn is true the object will pivot to the (left?) else it will pivot to the (right?)
                /// </summary>
                if (m_bTurn)
                {
                    transform.Rotate(0.0f, -m_fSpeed, 0.0f);
                    Q_Hinge = transform.rotation;
                    V3_euler = Q_Hinge.eulerAngles;
                    V3_euler.x = 0.0f;
                    V3_euler.x = V3_euler.y = Mathf.Clamp(V3_euler.y, m_fMin, m_fMax);
                    V3_euler.x = 0.0f;
                    transform.rotation = Quaternion.Euler(V3_euler);

                    if (transform.rotation == Quaternion.Euler(0.0f, m_fMin, 0.0f))
                    {
                        m_bTurn = !m_bTurn;
                    m_bOpen = false;
                    }
                }
                else
                {
                    transform.Rotate(0.0f, m_fSpeed, 0.0f);
                    Q_Hinge = transform.rotation;
                    V3_euler = Q_Hinge.eulerAngles;
                    V3_euler.x = 0.0f;
                    V3_euler.x = V3_euler.y = Mathf.Clamp(V3_euler.y, m_fMin, m_fMax);
                    V3_euler.x = 0.0f;
                    transform.rotation = Quaternion.Euler(V3_euler);

                    if (transform.rotation == Quaternion.Euler(0.0f, m_fMax, 0.0f))
                    {
                        m_bTurn = !m_bTurn;
                    m_bOpen = false;
                    }
                }
            }
        }

        void Update()
        {
            GateControl();
        }
    }