using UnityEngine;

public class WeaponHitBox : MonoBehaviour
{
    [SerializeField] private string targetTag = "Boss";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag))
        {
            // 아래 코드를 enemy에 damage 들어가는 걸로 바꾸면 됨.
            //TestManager.Instance.HitEnemy();
            RadeManager.Instance.DamageToBoss();

        }
    }
}
