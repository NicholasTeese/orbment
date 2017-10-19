using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerkButton : MonoBehaviour
{
    private float m_fBranchFillSpeed = 0.01f;
    private float m_fGrowShrinkSpeed = 0.05f;

    private bool m_bIsHighlighted = false;
    public bool IsHighlighted { get { return m_bIsHighlighted; } set { m_bIsHighlighted = value; } }
    private bool m_bIsPurchased = false;
    public bool IsPurchased { get { return m_bIsPurchased; } }
    private bool m_bChildPathChosen = false;
    private bool m_bIsCursorOver = false;

    private string m_strPerkName;

    private AudioClip m_perkSelectedAudioClip;
    private AudioClip m_perkUnavailableAudioClip;

    private Button m_perkIconButton;

    private StartingWeapon m_startingWeapon;

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
    public Text m_perkCanvasDescription;

    [Header("Perk Wings Script")]
    public PerkWings m_perkWings;

    [Header("Perk Upgrade Confirmation")]
    public GameObject m_perkUpgradeConfirmation;

    [Header("Back Button")]
    public GameObject m_backButton;

    private void Awake()
    {
        m_strPerkName = transform.parent.name;

        m_perkSelectedAudioClip = Resources.Load("Audio/Beta/UI/Perk_Tree/Perk_Selected") as AudioClip;
        m_perkUnavailableAudioClip = Resources.Load("Audio/Beta/UI/Perk_Tree/Perk_Unavailable") as AudioClip;

        m_perkIconButton = GetComponent<Button>();

        m_startingWeapon = GameObject.FindGameObjectWithTag("StartingWeapon").GetComponent<StartingWeapon>();
    }

    private void Update()
    {
        if (m_bIsHighlighted)
        {
            Grow();
        }
        else
        {
            Shrink();
        }

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
        if (m_perkUpgradeConfirmation.activeSelf)
        {
            return;
        }

        m_bIsCursorOver = true;
        PerkTreeManager.m_perkTreeManager.m_selectedPerkButton.IsHighlighted = false;
        PerkTreeManager.m_perkTreeManager.m_selectedPerkButton = GetComponent<PerkButton>();
        PerkTreeManager.m_perkTreeManager.m_selectedPerkButton.IsHighlighted = true;

        if (m_childPerks.Count > 0)
        {
            foreach (GameObject child in m_childPerks)
            {
                if (child.GetComponent<PerkButton>().m_bIsCursorOver)
                {
                    return;
                }
            }
        }

        m_perkCanvasDescription.text = a_strPerkDescription;
    }

    /// <summary>
    /// Is called when the cursor exits the button.
    /// </summary>
    public void OnCursorExit()
    {
        m_bIsCursorOver = false;
    }

    /// <summary>
    /// Is called when the button this script is attached to is clicked.
    /// </summary>
    /// <param name="a_strPerk"></param>
    public void OnClick()
    {
        // If there are no available perks, exit the function.
        if (PerkTreeManager.m_perkTreeManager.AvailiablePerks == 0)
        {
            Debug.Log("No available perks to spend.");
            return;
        }

        // If this perk's parent perk's is not purchased or it's one of it's child perks have already been chosen, exit the function.
        if (m_parentPerk != null)
        {
            if (!m_parentPerk.GetComponent<PerkButton>().m_bIsPurchased || m_parentPerk.GetComponent<PerkButton>().m_bChildPathChosen)
            {
                Debug.Log("Parent Perk not purchased or Child Perk path already chosen.");
                PerkTreeManager.m_perkTreeManager.PerkTreeAudioSource.PlayOneShot(m_perkUnavailableAudioClip);
                return;
            }

            // Set this perk's parent perk's child path to be chosen.
            //m_parentPerk.GetComponent<PerkButton>().m_bChildPathChosen = true;
        }

        // Purchase this perk.
        PerkTreeManager.m_perkTreeManager.PerkTreeAudioSource.PlayOneShot(m_perkSelectedAudioClip);
        m_perkUpgradeConfirmation.SetActive(true);
    }

    /// <summary>
    /// Is called when a perk is purchased.
    /// </summary>
    /// <param name="a_strPerkName"></param>
    public void PurchasePerk()
    {
        m_perkUpgradeConfirmation.SetActive(false);

        if (m_parentPerk != null)
        {
            m_parentPerk.GetComponent<PerkButton>().m_bChildPathChosen = true;
        }

        // Set the perk to be purchased.
        m_bIsPurchased = true;
        // Decrement the amount of available perks.
        if (PerkTreeManager.m_perkTreeManager.AvailiablePerks != 0)
        {
            PerkTreeManager.m_perkTreeManager.DecrementAvailiablePerks();
        }

        // If the perk is successfully applied change the perk images to be active.
        CheckFireTree();
        CheckIceTree();
        CheckLightningTree();
    }

    /// <summary>
    /// Checks the fire tree's perks to apply perk to.
    /// </summary>
    /// <param name="a_strPerkName"></param>
    private void CheckFireTree()
    {
        switch (m_strPerkName)
        {
            // Fire bullet (1A).
            case "FirePerk_1A":
                {
                    m_startingWeapon.SetProjectile(Resources.Load("Prefabs/Projectiles/FireBall") as GameObject);
//                    Player.m_Player.m_currentProjectile = GameObject.Find("FireBall");
                    break;
                }

            // Increase player movement speed by 50% (2A).
            case "FirePerk_2A":
                {
                    Player.m_Player.m_currSpeed += (Player.m_Player.m_currSpeed * 0.5f);
                    break;
                }

            // Increase player max health by 20% (2B).
            case "FirePerk_2B":
                {
                    Player.m_Player.m_maxHealth += (Player.m_Player.m_maxHealth * 0.2f);
                    break;
                }

            // Give player speed boost based on how many enemies are burning (3A).
            case "FirePerk_3A":
                {
                    Player.m_Player.BurningSpeedBoost = true;
                    break;
                }

            // Increase player bullet velocity by 30% (3B).
            case "FirePerk_3B":
                {
                    foreach (Transform bullet in m_startingWeapon.transform)
                    {
                        bullet.GetComponent<Bullet>().m_projectileSpeed += (bullet.GetComponent<Bullet>().m_projectileSpeed * 0.30f);
                    }
                    break;
                }

            // Spawn ring of fire when player health is below 25% (3C).
            case "FirePerk_3C":
                {
                    Player.m_Player.m_hasRingOfFire = true;
                    break;
                }

            // Getting a kill streak returns HP to the player (3D).
            case "FirePerk_3D":
                {
                    KillStreakManager.m_killStreakManager.Lifesteal = true;
                    break;
                }

            // Increase player speed boost based on how many enemies are burning (4A).
            case "FirePerk_4A":
                {
                    Player.m_Player.AdditionalBurningSpeedBoost = true;
                    break;
                }

            // Increase player bullet velocity by 50% (4B).
            case "FirePerk_4B":
                {
                    foreach (Transform bullet in m_startingWeapon.transform)
                    {
                        bullet.GetComponent<Bullet>().m_projectileSpeed += (int)(bullet.GetComponent<Bullet>().m_projectileSpeed * 0.50f);
                    }
                    break;
                }

            // Ring of fire damage increased (4C).
            case "FirePerk_4C":
                {
                    Player.m_Player.AdditionalBurnDPS += 5;
                    break;
                }

            // God mode enabled for 5 seconds when player reaches highest kill streak (4D).
            case "FirePerk_4D":
                {
                    Player.m_Player.GodModeIsAvailable = true;
                    break;
                }

            default:
                {
                    Debug.Log("Perk could not be found in fire tree to be applied.");
                    break;
                }

        }
    }

    /// <summary>
    /// Checks ice tree perks to apply perk to.
    /// </summary>
    /// <param name="a_strPerkName"></param>
    private void CheckIceTree()
    {
        switch (m_strPerkName)
        {
            // Ice bullet (1A).
            case "IcePerk_1A":
                {
                    m_startingWeapon.SetProjectile(Resources.Load("Prefabs/Projectiles/IceShard") as GameObject);
                    break;
                }

            // Increase player max mana by 20% (2A).
            case "IcePerk_2A":
                {
                    Player.m_Player.gameObject.GetComponent<Mana>().m_maxMana += (Player.m_Player.gameObject.GetComponent<Mana>().m_maxMana * 0.2f);
                    break;
                }

            // Increase player max health by 20% (2B).
            case "IcePerk_2B":
                {
                    Player.m_Player.m_maxHealth += (Player.m_Player.m_maxHealth * 0.2f);
                    break;
                }

            // Freeze (stun) enemies for 2 seconds (3A.)
            case "IcePerk_3A":
                {
                    //TODO: Implement.
                    Player.m_Player.FreezeUnlocked = true;
                    break;
                }

            // Ice bullets splatter (3B).
            case "IcePerk_3B":
                {
                    Player.m_Player.IceSplatterUnlocked = true;
                    break;
                }

            // Gain 25% armor when attacked by 3 or more enemies (3C).
            case "IcePerk_3C":
                {
                    //TODO: Implement.
                    break;
                }

            //TODO: Think of.
            case "IcePerk_3D":
                {
                    //TODO: Implement.
                    break;
                }

            // Freeze (stun) causes damage over time (4A).
            case "IcePerk_4A":
                {
                    //TODO: Implement.
                    break;
                }

            // Ice splatter drags enemies in (4B).
            case "IcePerk_4B":
                {
                    //TODO: Implement.
                    break;
                }
            
            // Gain 50% armour (4C).
            case "IcePerk_4C":
                {
                    //TODO: Implement.
                    break;
                }

            //TODO: Think of.
            case "IcePerk_4D":
                {
                    //TODO: Implement.
                    break;
                }

            default:
                {
                    Debug.Log("Perk could not be found in ice tree to be applied.");
                    break;
                }
        }
    }
    
    /// <summary>
    /// Checks lightning tree perk to apply perk to.
    /// </summary>
    /// <param name="a_strPerkName"></param>
    private void CheckLightningTree()
    {
        switch (m_strPerkName)
        {
            // Lightning bullet (1A).
            case "LightningPerk_1A":
                {
                    m_startingWeapon.SetProjectile(Resources.Load("Prefabs/Projectiles/LightningBall") as GameObject);
                    break;
                }

            // Increase player max mana by 20% (2A).
            case "LightningPerk_2A":
                {
                    Player.m_Player.gameObject.GetComponent<Mana>().m_maxMana += (Player.m_Player.gameObject.GetComponent<Mana>().m_maxMana * 0.2f);
                    break;
                }

            // Increase player speed by 50% (2B).
            case "LightningPerk_2B":
                {
                    Player.m_Player.m_currSpeed += (Player.m_Player.m_currSpeed * 0.5f);
                    break;
                }

            // Small bolt, chance hit, area damage (3A).
            case "LightningPerk_3A":
                {
                    //TODO: Implement.
                    break;
                }

            // Dash consumes 50% less mana (3B).
            case "LightningPerk_3B":
                {
                    Player.m_Player.m_dashManaCost -= (Player.m_Player.m_dashManaCost * 0.5f);
                    break;
                }

            // Dash does lightning damage (3C).
            case "LightningPerk_3C":
                {
                    //TODO: Implement.
                    break;
                }

            // Recall, go back 3 seconds (3D).
            case "LightningPerk_3D":
                {
                    //TODO: Implement.
                    break;
                }

            // God bolt, chance hit, instant kill (4A).
            case "LightningPerk_4A":
                {
                    //TODO: Implement.
                    break;
                }

            // Dash consumes no mana (4B).
            case "LightningPerk_4B":
                {
                    Player.m_Player.m_dashManaCost = 0;
                    break;
                }

            // Increase dash damage (4C).
            case "LightningPerk_4C":
                {
                    //TODO: Implement.
                    break;
                }

            // Shorter recall cool down (4D).
            case "LightningPerk_4D":
                {
                    //TODO: Implement.
                    break;
                }

            default:
                {
                    Debug.Log("Perk could not be found in lightning tree to be applied.");
                    break;
                }
        }
    }

    private void ActivateChildPerk()
    {
        m_perkIconButton.GetComponent<Image>().sprite = m_iconActive;
        m_perkWings.GetComponent<Image>().sprite = m_wingsActive;
        m_perkWings.Rotate = true;
    }

    private void Grow()
    {
        if (m_perkIconButton.transform.localScale.x < 1.5f)
        {
            m_perkIconButton.transform.localScale += new Vector3(m_fGrowShrinkSpeed, m_fGrowShrinkSpeed, 0.0f);
            m_perkWings.transform.localScale += new Vector3(m_fGrowShrinkSpeed, m_fGrowShrinkSpeed, 0.0f);
        }
    }

    private void Shrink()
    {
        if (m_perkIconButton.transform.localScale.x > 1.0f)
        {
            m_perkIconButton.transform.localScale -= new Vector3(m_fGrowShrinkSpeed, m_fGrowShrinkSpeed, 0.0f);
            m_perkWings.transform.localScale -= new Vector3(m_fGrowShrinkSpeed, m_fGrowShrinkSpeed, 0.0f);
        }
    }
}
