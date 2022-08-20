using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject healthBarGO;

    private HealthBar healthBar;
    private float hp = 100f;

    // Start is called before the first frame update
    void Start()
    {
        healthBar = healthBarGO.GetComponent<HealthBar>();
        healthBar.Init(hp, hp);
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
