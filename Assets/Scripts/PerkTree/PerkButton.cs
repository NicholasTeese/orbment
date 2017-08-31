using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerkButton : MonoBehaviour
{
    private bool m_bIsPurchased = false;
    private bool m_bChildPathChosen = false;
    private bool m_bCursorIsOver = false;

    private GameObject m_parentPerk = null;

    private List<GameObject> m_childPerks = new List<GameObject>();

    [Header("Perk Button ParticleSystems", order = 0)]
    public ParticleSystem m_firstParticleSystem;
    public ParticleSystem m_secondParticleSystem;

    private Button m_perkButton;

    public Text m_perkDescriptionText;

    private Image m_perkWingsImage;

    [Header("Perk Button Sprites", order = 1)]
    public Sprite m_circleInactive;
    public Sprite m_circleActive;
    public Sprite m_wingsInactive;
    public Sprite m_wingsActive;

    private StartingWeapon m_startingWeapon;

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

            if (child.CompareTag("Wings"))
            {
                m_perkWingsImage = child.GetComponent<Image>();
            }
        }

        m_firstParticleSystem.Stop();
        m_secondParticleSystem.Stop();

        m_perkButton = GetComponent<Button>();

        m_startingWeapon = GameObject.FindGameObjectWithTag("StartingWeapon").GetComponent<StartingWeapon>();
    }

    public void OnCursorEnter(string a_strPerkDescription)
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

        m_perkDescriptionText.text = a_strPerkDescription;
    }

    public void OnCursorExit()
    {
        m_bCursorIsOver = false;
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
        m_bIsPurchased = true;

        PerkTreeManager.m_perkTreeManager.DecrementAvailiablePerks();

        m_firstParticleSystem.Play();
        m_secondParticleSystem.Play();

        m_perkButton.GetComponent<Image>().sprite = m_circleActive;
        m_perkWingsImage.sprite = m_wingsActive;

        CheckFireTree(a_strPerkName);
        CheckIceTree(a_strPerkName);
        CheckLightningTree(a_strPerkName);
    }

    /// <summary>
    /// Is called when the button this script is attached to is clicked.
    /// </summary>
    /// <param name="a_strPerk"></param>
    public void OnClick(string a_strPerk)
    {
        // If there are no available perks, exit the function.
        if (PerkTreeManager.m_perkTreeManager.AvailiablePerks == 0)
        {
            Debug.Log("No availiable perks to spend.");
            return;
        }

        // If this perk's parent perk's is not purchased or it's one of it's child perks have already been chosen, exit the function.
        if (m_parentPerk != null)
        {
            if (!m_parentPerk.GetComponent<PerkButton>().m_bIsPurchased || m_parentPerk.GetComponent<PerkButton>().m_bChildPathChosen)
            {
                Debug.Log("Parent Perk not purchased or Child Perk path already chosen.");
                return;
            }

            // Set this perk's parent perk's child parth to be chosen.
            m_parentPerk.GetComponent<PerkButton>().m_bChildPathChosen = true;
        }

        // Perchase this perk.
        //x gameObject.GetComponent<Image>().color = Color.red;
        PurchasePerk(a_strPerk);
    }
}
