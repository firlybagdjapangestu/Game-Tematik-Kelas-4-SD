using UnityEngine;

public class NpcController : MonoBehaviour
{
    [SerializeField] private int npcIndex;
    [SerializeField] private bool npcGender;
    [SerializeField] private string choiceSfx;

    private Transform player; // Referensi ke transform pemain
    private Transform initialPositon; // Referensi ke transform parent awal
    private Animator animator;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Menemukan transform pemain saat permainan dimulai
        initialPositon = this.transform; // Menyimpan transform parent awal
        animator = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EventManager.BroadcastOnKidsCanTalk(true);
            animator.Play("Waving");
            GameManager.Instance.PlaySfx(choiceSfx);
            GameManager.Instance.SaveLevel(npcIndex);
            GameManager.Instance.LoadGame();

            RotateTowardsPlayer();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EventManager.BroadcastOnKidsCanTalk(false);
            RotateParentToInitial();
        }
    }

    private void RotateParentToInitial()
    {
        if (initialPositon != null)
        {
            this.transform.position = initialPositon.position;
            this.transform.rotation = initialPositon.rotation;
        }
    }

    private void RotateTowardsPlayer()
    {
        Transform gameobjectTransform = this.transform; // Mengakses transform parent dari collider

        if (gameobjectTransform != null)
        {
            Vector3 directionToPlayer = player.position - gameobjectTransform.position;
            directionToPlayer.y = 0; // Mengatur perubahan rotasi hanya pada sumbu horizontal
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            gameobjectTransform.rotation = targetRotation;
        }
        else
        {
            Debug.LogWarning("Parent transform not found!");
        }
    }
}
