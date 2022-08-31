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

    private Dictionary<State, string> States = new Dictionary<State, string>() {
        { State.WALK , "Walk"},
        { State.DIE , "Die"},
        { State.TAKE_DAMAGE, "TakeDamage" },
        { State.ATTACK, "Attack" }
    };
    private bool isDying = false;
    private float HP = 150;
    private float damage = 5;

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

    public void OnAttackEnd()
    {
        player.TakeDamage(damage);
    }

    private void Die()
    {
        animator.SetBool(States[State.ATTACK], false);
        isDying = true;
        animator.SetTrigger(States[State.DIE]);
        Destroy(gameObject, 1f);
        sceneManager.OnFinishedMobs();
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
