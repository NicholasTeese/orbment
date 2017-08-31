﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerkButton : MonoBehaviour
{
    private bool m_bIsPurchased = false;
    private bool m_bChildPathChosen = false;
    private bool m_bCursorIsOver = false;

    public string m_strPerkDescription = "Default Description";

    private GameObject m_parentPerk = null;
    private List<GameObject> m_childPerks = new List<GameObject>();

    public Text m_perkDescriptionText;

    private StartingWeapon m_startingWeapon;

    public void OnCursorOver()
    {
        m_bCursorIsOver = true;

        if (m_childPerks.Count > 0)
        {
            foreach (GameObject child in m_childPerks)
            {
                if (child.GetComponent<PerkButton>().m_bCursorIsOver)
                {
                    return;
                }
            }
        }
        
        m_perkDescriptionText.text = m_strPerkDescription;
    }

    public void OnCursorExit()
    {
        m_bCursorIsOver = false;
    }

    private void Awake()
    {
        if (transform.parent.CompareTag("PerkButton"))
        {
            m_parentPerk = transform.parent.gameObject;
        }

        foreach (Transform child in transform)
        {
            if (child.CompareTag("PerkButton"))
            {
                m_childPerks.Add(child.gameObject);
            }
        }

        m_startingWeapon = GameObject.FindGameObjectWithTag("StartingWeapon").GetComponent<StartingWeapon>();
    }

    /// <summary>
    /// Checks the fire tree's perks to apply perk to.
    /// </summary>
    /// <param name="a_strPerkName"></param>
    private void CheckFireTree(string a_strPerkName)
    {
        switch (a_strPerkName)
        {
            // Fire bullet (1A).
            case "Fire1A":
                {
                    m_startingWeapon.SetProjectile(Resources.Load("Prefabs/Projectiles/FireBall") as GameObject);
                    break;
                }

            // Increase player movement speed by 50% (2A).
            case "Fire2A":
                {
                    Player.m_Player.m_currSpeed += (Player.m_Player.m_currSpeed * 0.5f);
                    break;
                }

            // Increase player max health by 50% (2B).
            case "Fire2B":
                {
                    Player.m_Player.m_maxHealth += (Player.m_Player.m_maxHealth * 0.5f);
                    break;
                }

            // Give player speed boost based on how many enemies are burning (3A).
            case "Fire3A":
                {
                    //TODO: Implement.
                    break; 
                }

            // Increase player bullet velocity by 30% (3B).
            case "Fire3B":
                {
                    foreach (Transform bullet in m_startingWeapon.transform)
                    {
                        bullet.GetComponent<Bullet>().m_projectileSpeed += (bullet.GetComponent<Bullet>().m_projectileSpeed * 0.30f);
                    }
                    break;
                }

            // Spawn ring of fire when player health is below 25% (3C).
            case "Fire3C":
                {
                    Player.m_Player.m_hasRingOfFire = true;
                    break;
                }

            // Getting a kill streak returns HP to the player (3D).
            case "Fire3D":
                {
                    KillStreakManager.m_killStreakManager.Lifesteal = true;
                    break;
                }

            // Increase player speed boost based on how many enemies are burning (4A).
            case "Fire4A":
                {
                    //TODO: Implement.
                    break;
                }

            // Increase player bullet velocity by 50% (4B).
            case "Fire4B":
                {
                    foreach (Transform bullet in m_startingWeapon.transform)
                    {
                        bullet.GetComponent<Bullet>().m_projectileSpeed += (int)(bullet.GetComponent<Bullet>().m_projectileSpeed * 0.50f);
                    }
                    break;
                }

            // Ring of fire damage increased (4C).
            case "Fire4C":
                {
                    Player.m_Player.AdditionalBurnDPS += 5;
                    break;
                }

            // God mode enabled for 5 seconds when player reaches highest killstreak (4D).
            case "Fire4D":
                {
                    Player.m_Player.GodModeIsAvailable = true;
                    break;
                }

        }
    }

    /// <summary>
    /// Checks ice tree perks to apply perk to.
    /// </summary>
    /// <param name="a_strPerkName"></param>
    private void CheckIceTree(string a_strPerkName)
    {
        switch (a_strPerkName)
        {
            // Ice bullet (1).
            case "IceBullet":
                {
                    m_startingWeapon.SetProjectile(Resources.Load("Prefabs/Projectiles/IceShard") as GameObject);
                    break;
                }
        }
    }

    /// <summary>
    /// Checks lightning tree perk to apply perk to.
    /// </summary>
    /// <param name="a_strPerkName"></param>
    private void CheckLightningTree(string a_strPerkName)
    {
        switch (a_strPerkName)
        {
            // Lightning bullet (1).
            case "LightningBullet":
                {
                    m_startingWeapon.SetProjectile(Resources.Load("Prefabs/Projectiles/LightningBall") as GameObject);
                    break;
                }
        }
    }

    private void PurchasePerk(string a_strPerkName)
    {
        CheckFireTree(a_strPerkName);
        CheckIceTree(a_strPerkName);
        CheckLightningTree(a_strPerkName);
    }

    public void OnClick(string a_strPerk)
    {
        if (PerkTreeManager.m_perkTreeManager.AvailiablePerks == 0)
        {
            Debug.Log("No availiable perks to spend.");
            return;
        }

        if (m_parentPerk != null)
        {
            if (!m_parentPerk.GetComponent<PerkButton>().m_bIsPurchased || m_parentPerk.GetComponent<PerkButton>().m_bChildPathChosen)
            {
                Debug.Log("Parent Perk not purchased or Child Perk path already chosen.");
                return;
            }

            m_parentPerk.GetComponent<PerkButton>().m_bChildPathChosen = true;
        }

        m_bIsPurchased = true;
        gameObject.GetComponent<Image>().color = Color.red;
        PerkTreeManager.m_perkTreeManager.DecrementAvailiablePerks();
        PurchasePerk(a_strPerk);
    }
}
