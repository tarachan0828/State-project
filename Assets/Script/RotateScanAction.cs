using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/RotateScan")]
public class RotateScanAction : AIAction
{
    public float scanAngle = 45f; // 左右に振る角度
    public float scanSpeed = 1f;  // 振るスピード

    public override void Act(StateController controller)
    {
        // 時間経過で-45度〜45度の間を揺れ動かす
        float angle = Mathf.Sin(Time.time * scanSpeed) * scanAngle;
        controller.transform.localRotation = Quaternion.Euler(0, angle, 0);
    }
}