using UnityEngine;

public class AIFieldOfViewEditor : MonoBehaviour
{
    public LookDecision lookDecision; // 判定に使っているSOを入れる

    private void OnDrawGizmos()
    {
        if (lookDecision == null) return;

        // 視認距離のワイヤー
        Gizmos.color = Color.white;

        // 古典的な描画方法（どのバージョンでも動作）
        Gizmos.color = Color.yellow;
        Vector3 leftDir = Quaternion.AngleAxis(-lookDecision.lookAngle / 2, Vector3.up) * transform.forward;
        Vector3 rightDir = Quaternion.AngleAxis(lookDecision.lookAngle / 2, Vector3.up) * transform.forward;

        Gizmos.DrawLine(transform.position + Vector3.up * lookDecision.offsetHeight,
                        transform.position + Vector3.up * lookDecision.offsetHeight + leftDir * lookDecision.lookRange);
        Gizmos.DrawLine(transform.position + Vector3.up * lookDecision.offsetHeight,
                        transform.position + Vector3.up * lookDecision.offsetHeight + rightDir * lookDecision.lookRange);
    }
}