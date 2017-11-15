using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    Player m_playerRef;
    Mana m_playerMana;
    private Rigidbody m_rigidBody;
    public bool m_healthCap;
    public bool m_manaCap;

    public enum CollectableType
    {
        YellowOrb,
        GreenOrb,
        BlueOrb,
    }

    public CollectableType m_type;

    public int m_healAmount = 10;
    public int m_manaAmount = 100;


    // Use this for initialization
    void Start()
    {        
        m_healthCap = true;
        m_manaCap = true;
        m_playerRef = GameObject.FindObjectOfType<Player>();
        if(Player.m_player != null)
        {
            m_playerMana = Player.m_player.GetComponent<Mana>();
        }
        m_rigidBody = this.GetComponent<Rigidbody>();

        //Physics.IgnoreCollision(GetComponent<Collider>(), m_playerRef.GetComponent<Collider>());
    }

    // Update is called once per frame
    void Update()
    {
        if ((Player.m_player.m_currHealth + m_healAmount) > Player.m_player.m_maxHealth)
        {
            m_healthCap = true;
        }
        else
        {
            m_healthCap = false;
        }
        if ((m_playerMana.m_currentMana + m_manaAmount) > m_playerMana.m_maxMana)
        {
            m_manaCap = true;
        }
        else
        {
            m_manaCap = false;
        }

        if (m_type == CollectableType.GreenOrb && Player.m_player != null)
        {
            if(m_healthCap)
            {
                Physics.IgnoreCollision(GetComponent<Collider>(), Player.m_player.GetComponent<Collider>(), true);
            }
            else
            {
                Physics.IgnoreCollision(GetComponent<Collider>(), Player.m_player.GetComponent<Collider>(), false);
            }
        }

        if (m_type == CollectableType.BlueOrb && Player.m_player != null)
        {
            if(m_manaCap)
            {
                Physics.IgnoreCollision(GetComponent<Collider>(), Player.m_player.GetComponent<Collider>(), true);
            }
            else
            {
                Physics.IgnoreCollision(GetComponent<Collider>(), Player.m_player.GetComponent<Collider>(), false);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            switch (m_type)
            {
                case CollectableType.YellowOrb:
                    {
                        if(Player.m_player)
                        {
                            //Debug.Log(collision.collider);
                            Player.m_player.m_orbsCollected++;
                            Player.m_player.OrbPickedUp();

                            this.gameObject.SetActive(false);
                        }
                        break;
                    }

                case CollectableType.GreenOrb:
                    {
                        if (Player.m_player)
                        {
                            if (m_healthCap)
                            {
                                // Do not pick up orb, do not pass go, do not collect $200
                            }
                            else
                            {
                                Player.m_player.m_currHealth += m_healAmount;
                                Player.m_player.OrbPickedUp();

                                this.gameObject.SetActive(false);
                            }
                        }
                        break;
                    }

                case CollectableType.BlueOrb:
                    {
                        if(m_manaCap)
                        {
                            // Do not pick up orb, do not pass go, do not collect $200
                        }
                        else
                        {
                            m_playerMana.m_currentMana += m_manaAmount;
                            Player.m_player.OrbPickedUp();

                            this.gameObject.SetActive(false);
                        }
                        break;
                    }

                default:
                    {
                        break;
                    }
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "OrbBoundary")
        {
            m_rigidBody.velocity = -m_rigidBody.velocity;
            //Debug.Log("Exit Bounds");
        }
    }
}