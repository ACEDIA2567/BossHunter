using UnityEngine;

public class WeaponHitBox : MonoBehaviour
{
    [SerializeField] private string targetTag = "Enemy";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag))
        {
            // �Ʒ� �ڵ带 enemy�� damage ���� �ɷ� �ٲٸ� ��.
            TestManager.Instance.HitEnemy();
        }
    }
}
