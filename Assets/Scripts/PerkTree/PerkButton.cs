using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerkButton : MonoBehaviour
{
    private float m_fBranchFillSpeed = 0.01f;
    private float m_fGrowShrinkSpeed = 0.05f;

    private bool m_bIsHighlighted = false;
    private bool m_bIsPurchased = false;
    private bool m_bIsActivated = false;
    private bool m_bChildPathChosen = false;
    private bool m_bIsCursorOver = false;
    private bool m_bBranchFilled = false;

    private string m_strPerkName;

    private Button m_perkIconButton;

    private StartingWeapon m_startingWeapon;

    public bool IsHighlighted { get { return m_bIsHighlighted; } set { m_bIsHighlighted = value; } }
    public bool IsPurchased { get { return m_bIsPurchased; } }

    [Header("Particles")]
    public GameObject[] m_onPurchaseParticles;
    public GameObject[] m_afterPurchaseParticles;

    [Header("Parent Perks")]
    public GameObject m_parentPerk = null;
    [Header("Child Perks")]
    public List<GameObject> m_childPerks = new List<GameObject>();

    [Header("Active Perk Button Sprites")]
    public Sprite m_iconActive;
    public Sprite m_wingsActive;

    [Header("Perk Images")]
    public Image m_branchImage;

    [Header("Perk Description")]
    public string m_strPerkCanvasDescription;

    [Header("Perk Wings Script")]
    public PerkWings m_perkWings;

    [Header("Perk Upgrade Confirmation")]
    public GameObject m_perkUpgradeConfirmation;

    [Header("Back Button")]
    public GameObject m_backButton;

    private void Awake()
    {
        m_strPerkName = transform.parent.name;

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

        if (!m_bBranchFilled)
        {
            if (m_parentPerk != null && m_bIsPurchased)
            {
                if (m_branchImage.fillAmount < 1.0f)
                {
                    m_branchImage.fillAmount += m_fBranchFillSpeed;
                    return;
                }

                m_bBranchFilled = true;
                ActivateChildPerk();
                return;
            }
        }

        if (m_bIsPurchased)
        {
            ActivateChildPerk();
        }
    }

    private void OnDisable()
    {
        if (m_bIsPurchased)
        {
            for (int iCount = 0; iCount < m_onPurchaseParticles.Length; ++iCount)
            {
                Destroy(m_onPurchaseParticles[iCount]);
            }

            m_bIsActivated = true;
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

        PerkTreeManager.m_perkTreeManager.m_perkTreeDecriptionText.text = a_strPerkDescription;

        //UpdateBranchSizes();
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
        if (m_perkUpgradeConfirmation.activeSelf)
        {
            return;
        }

        // If there are no available perks, exit the function.
        if (PerkTreeManager.m_perkTreeManager.AvailiablePerks == 0)
        {
            Debug.Log("No available perks to spend.");
            return;
        }

        if (m_bIsPurchased)
        {
            Debug.Log("Perk already purchased.");
            return;
        }

        // If this perk's parent perk's is not purchased or it's one of it's child perks have already been chosen, exit the function.
        if (m_parentPerk != null)
        {
            if (!m_parentPerk.GetComponent<PerkButton>().m_bIsPurchased)
            {
                Debug.Log("Parent Perk not purchased.");
                AudioManager.m_audioManager.PlayOneShotPerkUnavailable();
                return;
            }

            // Set this perk's parent perk's child path to be chosen.
            //m_parentPerk.GetComponent<PerkButton>().m_bChildPathChosen = true;
        }

        // Purchase this perk.
        AudioManager.m_audioManager.PlayOneShotPerkSelected();
        PerkTreeManager.m_perkTreeManager.m_perkTreeDecriptionText.gameObject.SetActive(false);
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
        //if (PerkTreeManager.m_perkTreeManager.AvailiablePerks != 0)
        {
            PerkTreeManager.m_perkTreeManager.DecrementAvailiablePerks();
        }

        // If the perk is successfully applied change the perk images to be active.
        CheckFireTree();
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

            //x Increase player movement speed by 50% (2A).
            // Fire bullets have a chance 15% chance to set enemies on fire (2A).
            case "FirePerk_2A":
                {
                    //Player.m_player.m_currSpeed += (Player.m_player.m_currSpeed * 0.5f);
                    Player.m_player.CanSetEnemiesOnFire = true;
                    Player.m_player.ChanceToSetEnemiesOnFire = 15;
                    break;
                }

            // Increase player max health by 20% (2B).
            case "FirePerk_2B":
                {
                    Player.m_player.m_maxHealth += (Player.m_player.m_maxHealth * 0.2f);
                    break;
                }

            // Give player speed boost based on how many enemies are burning (3A).
            case "FirePerk_3A":
                {
                    Player.m_player.BurningSpeedBoost = true;
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
                    Player.m_player.m_hasRingOfFire = true;
                    break;
                }

            // Getting a kill streak returns HP to the player (3D).
            case "FirePerk_3D":
                {
                    KillStreakManager.m_killStreakManager.Lifesteal = true;
                    break;
                }

            //x Increase player speed boost based on how many enemies are burning (4A).
            // Fire bullets have a 25% chance of setting enemies on fire.
            case "FirePerk_4A":
                {
                    //x Player.m_player.AdditionalBurningSpeedBoost = true;
                    Player.m_player.ChanceToSetEnemiesOnFire = 25;
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
                    Player.m_player.AdditionalBurnDPS += 5;
                    break;
                }

            // God mode enabled for 5 seconds when player reaches highest kill streak (4D).
            case "FirePerk_4D":
                {
                    Player.m_player.GodModeAvailable = true;
                    break;
                }

            default:
                {
                    Debug.Log("Perk could not be found in fire tree to be applied.");
                    break;
                }
        }
    }

    private void ActivateChildPerk()
    {
        m_perkIconButton.GetComponent<Image>().sprite = m_iconActive;
        m_perkWings.GetComponent<Image>().sprite = m_wingsActive;
        m_perkWings.Rotate = true;

        if (!m_bIsActivated)
        {
            for (int iCount = 0; iCount < m_onPurchaseParticles.Length; ++iCount)
            {
                m_onPurchaseParticles[iCount].SetActive(true);
                m_onPurchaseParticles[iCount].GetComponent<ParticleSystem>().Simulate(Time.unscaledDeltaTime, true, false);
            }
        }

        for (int iCount = 0; iCount < m_afterPurchaseParticles.Length; ++iCount)
        {
            m_afterPurchaseParticles[iCount].SetActive(true);
            m_afterPurchaseParticles[iCount].GetComponent<ParticleSystem>().Simulate(Time.unscaledDeltaTime, true, false);
        }
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
