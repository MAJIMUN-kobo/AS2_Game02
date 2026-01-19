using UnityEngine;

public class EnemyItemDestroy : MonoBehaviour
{
    // Item‚ÉG‚ê‚½‚çŒÄ‚Î‚ê‚é
    private void OnTriggerEnter(Collider other)
    {
        // ‘Šè‚ªItem‚È‚ç
        if (other.CompareTag("Item"))
        {
            // Item‚ğÁ‚·
            Destroy(other.gameObject);
        }
    }
}
