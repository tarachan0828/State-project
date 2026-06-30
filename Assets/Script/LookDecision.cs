using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/Look")]
public class LookDecision : AIDecision
{
    public float lookRange = 20f;      // 見える距離
    public float lookAngle = 90f;      // 視野角（左右合計）
    public float offsetHeight = 1.5f; // 敵の目の高さ

    public override bool Decide(StateController controller)
    {
        return Look(controller);
    }

    private bool Look(StateController controller)
    {
        // プレイヤーとの距離をチェック
        float distanceToTarget = Vector3.Distance(controller.transform.position, controller.target.position);
        if (distanceToTarget > lookRange) return false;

        // プレイヤーへの方向ベクトル
        Vector3 dirToTarget = (controller.target.position - controller.transform.position).normalized;

        // 視野角の中にプレイヤーがいるか？
        if (Vector3.Angle(controller.transform.forward, dirToTarget) < lookAngle / 2)
        {
            RaycastHit hit;
            // 目の高さからレイを飛ばす
            Vector3 startPos = controller.transform.position + Vector3.up * offsetHeight;
            Vector3 targetPos = controller.target.position + Vector3.up * offsetHeight;

            if (Physics.Raycast(startPos, (targetPos - startPos).normalized, out hit, lookRange))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    // 見つけたら位置を更新（Searchステート用）
                    controller.lastKnownPosition = controller.target.position;
                    return true;
                }
            }
        }
        return false;
    }
}