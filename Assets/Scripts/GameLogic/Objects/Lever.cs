using UnityEngine;

public class Lever : MonoBehaviour, IInteractable
{
    public GameObject sceneHelperGO;
    public GameObject playerGO;
    public ParticleSystem explosion;
    public AudioClip explosionSound;
    public GameObject gateGO;
    public Animator animator;

    private SceneHelper sceneHelper;
    private Player player;
    private Gate gate;
    private const float explosionDamage = 10;
    private const string LEVER_ANIMATION = "Lever";

    void Start()
    {
        sceneHelper = sceneHelperGO.GetComponent<SceneHelper>();
        player = playerGO.GetComponent<Player>();
        gate = gateGO.GetComponent<Gate>();
    }

    public void Interact()
    {
        animator.SetTrigger(LEVER_ANIMATION);

        if (sceneHelper.GetLeverShots() > 0)
        {
            // open gate
            gate.Open();
        } else
        {
            Invoke(nameof(Explosion), 1);
        }
    }

    private void Explosion()
    {
        sceneHelper.AddLeverShot();
        explosion.Play();
        AudioSource.PlayClipAtPoint(explosionSound, transform.position);
        player.TakeDamage(explosionDamage);
    }
}
