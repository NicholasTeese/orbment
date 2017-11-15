using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveScatter : MonoBehaviour
{
    public BreakOnImpactWith m_script;
    public float m_explosiveForce = 100;
    public float m_explosiveRadius = 10;
    public float m_explosiveUpMod = 1;
    public Transform m_explsionPosition;
    private Rigidbody m_rb;

    private bool canFade;
    [SerializeField]
    private float fadePerSecond = 1.5f;

    void Update()
    {
        var material = GetComponent<Renderer>().material;
        var color = material.color;

        if (canFade)
        {
            //x this.GetComponent<MeshRenderer>().material.color = Color.Lerp(this.GetComponent<MeshRenderer>().material.color, alphaColor, timeToFade * Time.deltaTime);
            material.color = new Color(color.r, color.g, color.b, color.a - (fadePerSecond * Time.deltaTime));
            GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        }
    }

    void OnEnable()
    {
        m_rb = this.GetComponent<Rigidbody>();
        if (m_script != null && m_rb != null && m_explsionPosition != null)
        {
            transform.rotation = Random.rotation;
            m_rb.AddExplosionForce(m_explosiveForce, m_explsionPosition.position, m_explosiveRadius, m_explosiveUpMod);
            Physics.IgnoreCollision(GetComponent<Collider>(), Player.m_player.GetComponent<Collider>(), true);
            StartCoroutine(FadeDelay());
        }
    }

    IEnumerator FadeDelay()
    {
        yield return new WaitForSeconds(2.0f);
        canFade = true;
        yield return new WaitForSeconds(0.5f);
        //Destroy(transform.root.gameObject);
        this.gameObject.SetActive(false);
    }
}