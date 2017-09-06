using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerkButton : MonoBehaviour
{
    private float m_fBranchFillSpeed = 0.01f;

    private bool m_bIsPurchased = false;
    public bool IsPurchased { get { return m_bIsPurchased; } }
    private bool m_bChildPathChosen = false;
    private bool m_bCursorIsOver = false;
    private bool m_bLeftBranchActive = false;
    private bool m_bRightBranchActive = false;

    private Button m_perkIconButton;

    [Header("Parent Perks")]
    public GameObject m_parentPerk = null;
    [Header("Child Perks")]
    public List<GameObject> m_childPerks = new List<GameObject>();

    [Header("Active Perk Button Sprites")]
    public Sprite m_iconActive;
    public Sprite m_wingsActive;

    [Header("Perk Images")]
    public Image m_branchImage;

    [Header("Perk Description Text")]
    public Text m_perkDescriptionText;

    [Header("Perk Wings Script")]
    public PerkButtonWings m_perkButtonWings;

    private StartingWeapon m_startingWeapon;

    private void Awake()
    {
        m_perkIconButton = GetComponent<Button>();

        m_startingWeapon = GameObject.FindGameObjectWithTag("StartingWeapon").GetComponent<StartingWeapon>();
    }

    private void Update()
    {
        if (m_parentPerk != null && m_bIsPurchased)
        {
            if (m_branchImage.fillAmount < 1.0f)
            {
                m_branchImage.fillAmount += m_fBranchFillSpeed;
                return;
            }

            ActivateChildPerk();
            return;
        }

        if (m_bIsPurchased)
        {
            ActivateChildPerk();
        }
    }

    /// <summary>
    /// Is called when the cursor enters the button. Sends the perk's description to be displayed.
    /// </summary>
    /// <param name="a_strPerkDescription"></param>
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

    /// <summary>
    /// Is called when the cursor exits the button.
    /// </summary>
    public void OnCursorExit()
    {
        m_bCursorIsOver = false;
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
        PurchasePerk(a_strPerk);
    }

    /// <summary>
    /// Is called when a perk is purchased.
    /// </summary>
    /// <param name="a_strPerkName"></param>
    private void PurchasePerk(string a_strPerkName)
    {
        // Set the perk to be purchased.
        m_bIsPurchased = true;
        // Decrement the amount of available perks.
        PerkTreeManager.m_perkTreeManager.DecrementAvailiablePerks();

        // If the perk is successfully applied change the perk images to be active.
        CheckFireTree(a_strPerkName);

        //TODO: CheckIceTree(a_strPerkName);
        //TODO: CheckLightningTree(a_strPerkName);
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

            default:
                {
                    Debug.Log("Perk could not be found to be applied.");
                    break;
                }

        }
    }

    //TODO: /// <summary>
    //TODO: /// Checks ice tree perks to apply perk to.
    //TODO: /// </summary>
    //TODO: /// <param name="a_strPerkName"></param>
    //TODO: private void CheckIceTree(string a_strPerkName)
    //TODO: {
    //TODO:     switch (a_strPerkName)
    //TODO:     {
    //TODO:         // Ice bullet (1).
    //TODO:         case "IceBullet":
    //TODO:             {
    //TODO:                 m_startingWeapon.SetProjectile(Resources.Load("Prefabs/Projectiles/IceShard") as GameObject);
    //TODO:                 break;
    //TODO:             }
    //TODO:     }
    //TODO: }
    //TODO: 
    //TODO: /// <summary>
    //TODO: /// Checks lightning tree perk to apply perk to.
    //TODO: /// </summary>
    //TODO: /// <param name="a_strPerkName"></param>
    //TODO: private void CheckLightningTree(string a_strPerkName)
    //TODO: {
    //TODO:     switch (a_strPerkName)
    //TODO:     {
    //TODO:         // Lightning bullet (1).
    //TODO:         case "LightningBullet":
    //TODO:             {
    //TODO:                 m_startingWeapon.SetProjectile(Resources.Load("Prefabs/Projectiles/LightningBall") as GameObject);
    //TODO:                 break;
    //TODO:             }
    //TODO:     }
    //TODO: }

    private void ActivateChildPerk()
    {
        m_perkIconButton.GetComponent<Image>().sprite = m_iconActive;
        m_perkButtonWings.GetComponent<Image>().sprite = m_wingsActive;
        m_perkButtonWings.Rotate = true;
    }
}
