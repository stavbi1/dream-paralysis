using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public GameObject healthBarGO;
    public GameObject[] guns;
    public AudioClip dieSound;

    private HealthBar healthBar;
    private int currentGunIndex = 0;
    private float hp = 100f;
    private const int PISTOL_INDEX = 0;

    // Start is called before the first frame update
    void Start()
    {
        healthBar = healthBarGO.GetComponent<HealthBar>();
        healthBar.Init(hp, hp);
    }

    public void SwitchGunToPistol()
    {
        guns[currentGunIndex].SetActive(false);
        currentGunIndex = PISTOL_INDEX;
        guns[currentGunIndex].SetActive(true);
    }

    public void SwitchGun()
    {
        guns[currentGunIndex].SetActive(false);
        currentGunIndex = (currentGunIndex + 1) % guns.Length;
        guns[currentGunIndex].SetActive(true);
    }

    public void TakeDamage(float damage)
    {
        hp = hp - damage;

        if (hp <= 0)
        {
            Die();
        } else
        {
            healthBar.SetValue(hp);
        }
    }

    public void AddHP(float addedHP)
    {
        if (hp + addedHP > 100)
        {
            hp = 100;
        } else
        {
            hp += addedHP;
        }
        healthBar.SetValue(hp);
    }

    public void Die()
    {
        AudioSource.PlayClipAtPoint(dieSound, transform.position);
        SceneManager.LoadScene(1);
    }
}
