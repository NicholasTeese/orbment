//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class TransparencyController : MonoBehaviour
//{
//    private Camera m_camera = null;

//    private GameObject m_gameObject = null;

//    private Color m_originalColor = Color.white;

//    private void Awake()
//    {
//        m_camera = GetComponent<Camera>();
//    }

//    private void FixedUpdate()
//    {
//        RaycastHit rayCastHit;
//        Ray ray = m_camera.ScreenPointToRay(m_camera.WorldToScreenPoint(Player.m_Player.transform.position));

//        Debug.DrawRay(transform.position, ray.direction * 10, Color.red);

//        if (Physics.Raycast(ray, out rayCastHit))
//        {
//            switch (rayCastHit.transform.tag)
//            {
//                case "Leaves":
//                    {
//                        m_gameObject = rayCastHit.transform.gameObject;
//                        SetTransparent(m_originalColor, m_gameObject, true);
//                        break;
//                    }

//                case "Tree":
//                    {
//                        Debug.Log("CORRECT: " + rayCastHit.transform.name);
//                        break;
//                    }

//                case "Wall":
//                    {
//                        Debug.Log("CORRECT: " + rayCastHit.transform.name);
//                        break;
//                    }

//                default:
//                    {
//                        Debug.Log("INCORRECT: " + rayCastHit.transform.parent.name);
//                        break;
//                    }
//            }
//        }
//        else
//        {
//            SetOpaque(m_originalColor, m_gameObject, false);
//        }
//    }

//    private void SetTransparent(Color a_originalColor, GameObject a_gameObject, bool a_bIncludeChildren = false)
//    {
//        a_originalColor = a_gameObject.GetComponent<Renderer>().material.color;
//        Color transparentColor = a_originalColor;
//        transparentColor.a = 0.3f;

//        if (a_bIncludeChildren)
//        {
//            foreach (Transform child in m_gameObject.transform)
//            {

//            }
//        }
//    }

//    private void SetOpaque(Color a_originalColor, GameObject a_gameObject, bool a_bIncludeChildren = false)
//    {
//        a_originalColor = a_gameObject.GetComponent<Renderer>().material.color;
//    }
//}
