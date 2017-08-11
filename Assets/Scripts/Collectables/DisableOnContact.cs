using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnContact : MonoBehaviour
{
    public string m_tag;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(m_tag))
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(m_tag))
        {
            this.gameObject.SetActive(false);
        }
    }
}
