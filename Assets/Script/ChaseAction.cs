using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Chase")]
public class ChaseAction : AIAction
{
    public override void Act(StateController controller)
    {
        // ナビメッシュエージェントの目的地をプレイヤーの座標にする
        controller.navMeshAgent.destination = controller.target.position;
        // 追いかけるときは少し速くする設定（任意）
        controller.navMeshAgent.speed = 5f;
    }
}