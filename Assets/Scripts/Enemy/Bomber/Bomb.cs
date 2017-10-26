using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private float m_fRollSpeed = 5.0f;
    private float m_fFuseTimer = 3.0f;
    private float m_fLastColourSwitchTime;

    public float m_fBombDamage;

    private bool m_bHasExploded = false;

    private Vector3 m_v3RollPosition = Vector3.zero;

    private Renderer m_renderer;

    private AudioClip[] m_explosionClips;

    private AudioSource m_audioSource;

    private void Awake()
    {
        m_fLastColourSwitchTime = m_fFuseTimer;

        m_renderer = GetComponent<Renderer>();
        m_renderer.material.color = Color.black;

        m_explosionClips = Resources.LoadAll<AudioClip>("Audio/Beta/Actors/Enemies/Gremlin");

        m_audioSource = AudioManager.m_audioManager.ExplosionAudioSource;

        if (m_fBombDamage == 0.0f)
        {
            //if not set in inspector, default to 200
            m_fBombDamage = 200.0f;
        }
    }

    private void Start()
    {
        m_v3RollPosition = Player.m_player.transform.position;
    }

    private void Update()
    {
        m_fFuseTimer -= Time.deltaTime;
        
        if (m_fLastColourSwitchTime - m_fFuseTimer > 0.5f)
        {
            m_fLastColourSwitchTime = m_fFuseTimer;

            if (m_renderer.material.color == Color.black)
            {
                m_renderer.material.color = Color.red;
            }
            else
            {
                m_renderer.material.color = Color.black;
            }
        }

        if (m_fFuseTimer >= 0.0f)
        {
            transform.position = Vector3.MoveTowards(transform.position, m_v3RollPosition, (m_fRollSpeed * Time.deltaTime));
        }
        else if (m_fFuseTimer <= 0.0f && !m_bHasExploded)
        {
            m_audioSource.PlayOneShot(m_explosionClips[Random.Range(0, m_explosionClips.Length)]);

            GameObject explosion = Instantiate(Resources.Load("Prefabs/Explosions/FireExplosion") as GameObject);
            explosion.transform.position = transform.position + new Vector3(0.0f, 0.5f, 0.0f);
            explosion.transform.SetParent(BombManager.m_bombManager.transform);
            m_bHasExploded = true;
            gameObject.SetActive(false);

            if (Vector3.Distance(transform.position, Player.m_player.transform.position) <= 3.0f && !Player.m_player.m_dashing)
            {
                Player.m_player.m_currHealth -= 200.0f;
            }

            Destroy(explosion, 1.0f);
            Destroy(gameObject, 5.0f);
        }
    }
}
