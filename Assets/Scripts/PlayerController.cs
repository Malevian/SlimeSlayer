using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public Unit player = new Unit(10.0f, 1f, 0, 1.0f);
    public float maxHealth = 10.0f;
    [SerializeField] public float mana = 10.0f;
    public float maxMana = 10.0f;

    private float attackSpd = 1.0f;
    private float fireRate = 2.0f;
    private float fireDelay;

    [SerializeField] int exp = 0;
    [SerializeField] int expMax = 5;
    public TextBehaviour expText;

    [SerializeField] float level = 1;
    public TextBehaviour levelText;

    private Rigidbody2D rb;
    private Vector2 movementDirection;

    public ProjectileBehaviour projectilePrefab;

    public BarBehaviour HealthBar;
    public TextBehaviour HealthText;

    public BarBehaviour ManaBar;
    public TextBehaviour ManaText;
    private float manaRegenDelay;
    private float manaRegenRate = 1.0f;

    private bool isDead = false;

    [SerializeField] public GameManager gameManager;
    [SerializeField] public UpgradePanelManager upgradePanel;

    [SerializeField] List<UpgradeData> upgrades;
    List<UpgradeData> selectedUpgrades;
    [SerializeField]List<UpgradeData> aquiredUpgrades;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        player.Health = maxHealth;
        mana = maxMana;

        HealthBar.setMaxBar(maxHealth);
        HealthText.setStatText(maxHealth, maxHealth);

        ManaBar.setMaxBar(maxMana);
        ManaText.setStatText(maxMana, maxMana);

        expText.setStatText(exp, expMax);

        levelText.setText(level);
    }

    // Update is called once per frame
    void Update()
    {
        movementDirection = (new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"))).normalized;

        faceRotation();
        FireProjectile();
        movementRestrains();

        RegenMana(2); //default 1

        LevelUp();
    }

    private void FixedUpdate()
    {
        rb.velocity = movementDirection * player.Speed * 5;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boost"))
        {
            BoostBehaviour boost = collision.GetComponent<BoostBehaviour>();
            ApplyBoost(boost.boostType, boost.value);
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Enemy"))
        {
            EnemyMovement enemy = collision.GetComponent<EnemyMovement>();
            changeHealthByAmount(-enemy.enemy.Attack);
        }
    }

    public float AttackSpd { get { return attackSpd; } set { attackSpd = value; } }

    public int Experience { get { return exp; } set { exp = value; expText.setStatText(exp, expMax); } }

    public void faceRotation()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
    }

    public void FireProjectile()
    {
        if (mana <= 0) return;

        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.UpArrow)) //Shoot top-left
        {
            ShootProjectile(new Vector3(-1, 1, 0).normalized, new Vector3(0, 0, 135));
        }
        if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow)) //Shoot top-right
        {
            ShootProjectile(new Vector3(1, 1, 0).normalized, new Vector3(0, 0, 45));
        }
        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.DownArrow)) //Shoot down-left
        {
            ShootProjectile(new Vector3(-1, -1, 0).normalized, new Vector3(0, 0, -135));
        }
        if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.DownArrow)) //Shoot down-right
        {
            ShootProjectile(new Vector3(1, -1, 0).normalized, new Vector3(0, 0, -45));
        }
        if (Input.GetKey(KeyCode.LeftArrow)) //Shoot left
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            ShootProjectile(Vector3.left, new Vector3(0, 0, 180));
        }
        if (Input.GetKey(KeyCode.RightArrow)) //Shoot right
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            ShootProjectile(Vector3.right, new Vector3(0, 0, 0));
        }
        if (Input.GetKey(KeyCode.UpArrow)) //Shoot up
        {
            ShootProjectile(Vector3.up, new Vector3(0, 0, 90));
        }
        if (Input.GetKey(KeyCode.DownArrow)) //Shoot down
        {
            ShootProjectile(Vector3.down, new Vector3(0, 0, -90));
        }
    }

    void ShootProjectile(Vector3 direction, Vector3 rotation)
    {
        if(Time.time >= fireDelay + (1 / fireRate))
        {
            Instantiate(projectilePrefab, transform.position + direction, Quaternion.Euler(rotation));
            changeManaByAmount(-1);
            fireDelay = Time.time;
        }
    }

    void movementRestrains()
    {
        float bound_x = 40f;
        float bound_y = 36f;

        if (transform.position.x > bound_x)
        {
            transform.position = new Vector3(bound_x, transform.position.y, 0);
        }
        if (transform.position.y > bound_y)
        {
            transform.position = new Vector3(transform.position.x, bound_y, 0);
        }
        if (transform.position.x < -bound_x)
        {
            transform.position = new Vector3(-bound_x, transform.position.y, 0);
        }
        if (transform.position.y < -bound_y)
        {
            transform.position = new Vector3(transform.position.x, -bound_y, 0);
        }
    }

    void ApplyBoost(BoostType boostType, float value)
    {
        if (player != null)
        {
            switch (boostType)
            {
                case BoostType.Attack:
                    player.Attack += value;
                    StartCoroutine(BuffTimer(BoostType.Attack, value)); break;
                case BoostType.Health:
                    player.Health += value;
                    StartCoroutine(BuffTimer(BoostType.Health, value)); break;
                case BoostType.Defense:
                    player.Defense += value;
                    StartCoroutine(BuffTimer(BoostType.Defense, value)); break;
                case BoostType.AtkSpd:
                    AttackSpd += value;
                    StartCoroutine(BuffTimer(BoostType.AtkSpd, value)); break;
                case BoostType.Speed:
                    player.Speed += value;
                    StartCoroutine(BuffTimer(BoostType.Speed, value)); break;
            }
        }
    }

    IEnumerator BuffTimer(BoostType boostType, float value)
    {
        yield return new WaitForSeconds(3.0f);
        switch(boostType)
        {
            case BoostType.Attack: player.Attack -= value; break;
            case BoostType.Health: player.Health -= value; break;
            case BoostType.Defense: player.Defense -= value; break;
            case BoostType.AtkSpd: attackSpd -= value; break;
            case BoostType.Speed: player.Speed -= value; break;
        }
    }

    void changeManaByAmount(float amount)
    {
        mana = mana + amount >= maxMana ? maxMana : mana + amount;
        ManaBar.setBar(mana);
        ManaText.setStatText(mana, maxMana);
    }

    void changeHealthByAmount(float amount)
    {
        if (isDead) {  return; }

        player.Health = player.Health + amount >= maxHealth ? maxHealth : player.Health + amount;

        if (player.Health < 0) { 
            gameManager.gameOver(); 
            isDead = true;
        }

        HealthBar.setBar(player.Health);
        HealthText.setStatText(player.Health, maxHealth);
    }

    //Mana regeneration
    void RegenMana(float amount)
    {
        if (Time.time >= manaRegenDelay + (1 / manaRegenRate) && mana < maxMana)
        {
            changeManaByAmount(amount);
            manaRegenDelay = Time.time;
        }
    }

    void LevelUp()
    {
        if(selectedUpgrades == null) { selectedUpgrades = new List<UpgradeData>(); }
        selectedUpgrades.Clear();
        selectedUpgrades.AddRange(getUpgrades(4));

        int remainder = 0;
        
        if(exp > expMax)
        {
            remainder = exp - expMax;
        }

        if(exp >= expMax)
        {
            upgradePanel.openPanel(selectedUpgrades);
            level++;
            expMax *= 2;
            exp = remainder;
            levelText.setText(level);
            expText.setStatText(exp, expMax);
        }
    }

    public void upgrade(int upgradeId)
    {
        UpgradeData upgradeData = selectedUpgrades[upgradeId];

        if(aquiredUpgrades == null) aquiredUpgrades = new List<UpgradeData>();

        aquiredUpgrades.Add(upgradeData);
        upgrades.Remove(upgradeData);
    }

    public List<UpgradeData> getUpgrades(int count)
    {
        List<UpgradeData> upgradeList = new List<UpgradeData>();

        if(count > upgrades.Count)
        {
            count = upgrades.Count;
        }

        for(int i = 0; i < count; i++)
        {
            upgradeList.Add(upgrades[UnityEngine.Random.Range(0, upgrades.Count)]);
        }

        return upgradeList;
    }
}