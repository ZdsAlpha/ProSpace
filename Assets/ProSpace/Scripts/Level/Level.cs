using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Level : MonoBehaviour {
    public LevelGenerator generator;
    public Transform player;
    public Transform level;
    public Camera camera;
    public FadePanel FadePanel;
    public Transform EscapeMenu;
    public Transform WinPanel;
    public Transform DiedPanel;
    public float maxBarValue = 220f;
    public float lerpRate = 2f;
    public RectTransform healthBar;
    public Image healthBatImage;
    public Color fullHealthColor;
    public Color zeroHealthColor;
    public RectTransform fuelBar;
    public RectTransform boostBar;
    public Text levelLabel;
    public Text livesLabel;
    public Text scoresLabel;
    public float ScoreMultiplier = 1.019f;
    public int MinBlocks = 50;
    public int BlockIncrement = 50;

    private Health playerHealth;
    private PlayerAttackness playerAttackness;
    private PlayerMovement playerMovement;

    private float scores = 0f;
    private float health = 1f;
    private float fuel = 1f;
    private float boost = 1f;
    
	void Start () {
        if (!Game.LoadProfile()) Game.Profile = new UserProfile();
        Physics2D.queriesHitTriggers = true;
        if (Game.Profile == null) Game.LoadProfile();
        playerHealth = player.GetComponent<Health>();
        playerAttackness = player.GetComponent<PlayerAttackness>();
        playerMovement = player.GetComponent<PlayerMovement>();
        if (Game.IsCustom)
        {
            generator.Generate(Game.Seed, Game.Level, Game.Regions);
        }
        else
        {
            System.Random random = new System.Random(Game.Profile.Seed);
            for (int i = 1; i < Game.Level; i++)
            {
                random.Next();
            }
            generator.Generate(random.Next(), Game.Level, (int)Mathf.Floor(MinBlocks + 2f * BlockIncrement * (float)random.NextDouble() * Mathf.Log(Game.Level)));
        }
        levelLabel.text = "Level: " + Game.Level.ToString();
        //Initializing Player stats
        playerHealth.HP *= Game.Profile.Health;
        playerHealth.MaxHP *= Game.Profile.Health;
        playerHealth.HPGeneration *= Game.Profile.HealthGeneration;
        playerAttackness.fuel *= Game.Profile.Fuel;
        playerAttackness.maxFuel *= Game.Profile.Fuel;
        playerAttackness.fuelGeneration *= Game.Profile.FuelGeneration;
        playerMovement.boost *= Game.Profile.Boost;
        playerMovement.maxBoost *= Game.Profile.Boost;
        playerMovement.boostGeneration *= Game.Profile.BoostGeneration;
        playerAttackness.AttackMax *= Game.Profile.AttackDamage;
        playerAttackness.Frequency *= Game.Profile.AttackFrequency;
        playerAttackness.MaxDistance *= Game.Profile.AttackRange;
        playerMovement.JumpSpeed *= Game.Profile.JumpHeight;
        playerMovement.JumpCount = Game.Profile.Jumps;
        playerHealth.Lives = Game.Profile.Lives;
    }
    void Update () {
        if (WinPanel.gameObject.activeSelf || DiedPanel.gameObject.activeSelf)
        {
            if (Input.anyKeyDown)
            {
                Restart();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                EscapeMenu.TrySetActive(!EscapeMenu.gameObject.activeSelf);
                playerMovement.enabled = !EscapeMenu.gameObject.activeSelf;
                playerAttackness.enabled = !EscapeMenu.gameObject.activeSelf;
            }
        }
        healthBar.offsetMax = new Vector3(-maxBarValue * (1 - health), healthBar.offsetMax.y);
        healthBatImage.color = Color.Lerp(zeroHealthColor, fullHealthColor, health);
        fuelBar.offsetMax = new Vector3(-maxBarValue * (1 - fuel), fuelBar.offsetMax.y);
        boostBar.offsetMax = new Vector3(-maxBarValue * (1 - boost), boostBar.offsetMax.y);
        livesLabel.text = "x" + playerHealth.Lives.ToString();
        scoresLabel.text = "Scores: " + (Mathf.Ceil(scores)).ToString();
        RenderSettings.skybox.SetFloat("_Rotation", player.localPosition.x / 10);
    }
    private void FixedUpdate()
    {
        float health = playerHealth.HP / playerHealth.MaxHP;
        this.health = Mathf.Lerp(this.health, health, Time.deltaTime * lerpRate);
        float fuel = playerAttackness.fuel / playerAttackness.maxFuel;
        this.fuel = Mathf.Lerp(this.fuel, fuel, Time.deltaTime * lerpRate);
        float boost = playerMovement.boost / playerMovement.maxBoost;
        this.boost = Mathf.Lerp(this.boost, boost, Time.deltaTime * lerpRate);
        scores = Mathf.Lerp(scores, Game.Profile.Score, Time.deltaTime * lerpRate);
    }
    public void SaveCheckpoint()
    {
        playerHealth.SaveCheckpoint();
    }
    public void Heal(float Amount)
    {
        playerHealth.HP = Mathf.Clamp(playerHealth.HP + Amount, 0f, playerHealth.MaxHP);
    }
    public void Refuel(float Amount)
    {
        playerAttackness.fuel = Mathf.Clamp(playerAttackness.fuel + Amount, 0f, playerAttackness.maxFuel);
    }
    public void Reboost(float Amount)
    {
        playerMovement.boost = Mathf.Clamp(playerMovement.boost + Amount, 0f, playerMovement.maxBoost);
    }
    public void AddScores(float Amount)
    {
        Game.Profile.Score += Amount * Mathf.Pow(ScoreMultiplier, Game.Level - 1);
    }
    public void AddLife()
    {
        playerHealth.Lives += 1;
    }
    public void Lose()
    {
        DiedPanel.TrySetActive(true);
        FadePanel.FadeIn();
    }
    public void Win()
    {
        WinPanel.TrySetActive(true);
        if (Game.Level == Game.Profile.Level) Game.Profile.Level += 1;
        Game.Level += 1;
        Game.SaveProfile();
        player.TrySetActive(false);
        FadePanel.FadeIn();
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Quit()
    {
        SceneManager.LoadScene(0);
    }
}