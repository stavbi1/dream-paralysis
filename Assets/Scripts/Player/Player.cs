using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject healthBarGO;
    public GameObject[] guns;

    private HealthBar healthBar;
    private int currentGunIndex = 0;
    private float hp = 100f;

    // Start is called before the first frame update
    void Start()
    {
        healthBar = healthBarGO.GetComponent<HealthBar>();
        healthBar.Init(hp, hp);
    }

    public void SwitchGun()
    {
        guns[currentGunIndex].SetActive(false);
        currentGunIndex = (currentGunIndex + 1) % guns.Length;
        guns[currentGunIndex].SetActive(true);
    }

    public void TakeDamage(float damage)
    {
        Debug.Log(hp);
        hp = hp - damage;

        if (hp <= 0)
        {
            Die();
        } else
        {
            healthBar.SetValue(hp);
        }
    }

    public void Die()
    {
        Time.timeScale = 0;
    }
}
