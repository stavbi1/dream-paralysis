using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

enum State
{
    WALK,
    DIE,
    ATTACK,
    TAKE_DAMAGE
}

public class Mob : MonoBehaviour
{
    public GameObject playerGO;
    public GameObject rendererGO;
    public GameObject sceneHelperGO;
    public AudioClip damageSound;
    public AudioClip preAttackSound;
    public AudioClip attackSound;
    public float HP = 100;

    private Dictionary<State, string> States = new Dictionary<State, string>() {
        { State.WALK , "Walk"},
        { State.DIE , "Die"},
        { State.TAKE_DAMAGE, "TakeDamage" },
        { State.ATTACK, "Attack" }
    };
    private bool isDying = false;
    private float damage = 4;

    private SceneHelper sceneManager;
    private Player player;
    private NavMeshAgent nav;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        sceneManager = sceneHelperGO.GetComponent<SceneHelper>();
        player = playerGO.GetComponent<Player>();
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        nav.SetDestination(playerGO.transform.position);
    }

    void FixedUpdate()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(States[State.WALK]))
        {
            nav.isStopped = false;
        } else
        {
            nav.isStopped = true;
        }
    }

    public void TakeDamage(float damage)
    {
        if (!isDying) {
            if (damageSound) AudioSource.PlayClipAtPoint(damageSound, transform.position);

            HP -= damage;

            if (HP <= 0)
            {
                Die();
            }
            else
            {
                animator.SetTrigger(States[State.TAKE_DAMAGE]);
            }
        }
    }

    public void OnAttackStart()
    {
        if (preAttackSound) AudioSource.PlayClipAtPoint(preAttackSound, transform.position);
        if (attackSound) Invoke(nameof(PlayAttackSound), 0.5f);
    }

    public void OnAttackEnd()
    {
        player.TakeDamage(damage);
    }

    public void UpdateDestination()
    {
        nav.SetDestination(playerGO.transform.position);
    }

    private void PlayAttackSound()
    {
        AudioSource.PlayClipAtPoint(attackSound, transform.position);
    }

    private void Die()
    {
        animator.SetBool(States[State.ATTACK], false);
        isDying = true;
        animator.SetTrigger(States[State.DIE]);
        Destroy(gameObject, 1f);
        sceneManager.OnMobDie();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Attack();
        }
    }

    private void Attack()
    {
        animator.SetBool(States[State.ATTACK], true);
    }
}
