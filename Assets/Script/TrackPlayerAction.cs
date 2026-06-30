using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/TrackPlayer")]
public class TrackPlayerAction : AIAction
{
    public override void Act(StateController controller)
    {
        // プレイヤーの方向を滑らかに向く
        Vector3 dir = controller.target.position - controller.transform.position;
        dir.y = 0; // 上下方向の回転は無視する場合
        Quaternion targetRot = Quaternion.LookRotation(dir);
        controller.transform.rotation = Quaternion.Slerp(controller.transform.rotation, targetRot, Time.deltaTime * 5f);

        // 仲間（ザコ敵）にプレイヤーの位置を伝える（後述のAlertManagerを使用）
        AlertManager.Instance.ReportPlayerLocation(controller.target.position);

        if (AlertManager.Instance != null)
        {
            // これを追加！
            Debug.Log("カメラ：プレイヤーを発見！アラートを発信します！");
            AlertManager.Instance.ReportPlayerLocation(controller.target.position);
        }
    }
}