using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Patrol")]
public class PatrolAction : AIAction
{
    public override void Act(StateController controller)
    {
        // 目的地に向かう
        controller.navMeshAgent.destination = controller.wayPoints[controller.nextWayPoint].position;
        controller.navMeshAgent.speed = 2.5f; // 巡回はゆっくり

        // 到着したら次の地点へ
        if (controller.navMeshAgent.remainingDistance <= controller.navMeshAgent.stoppingDistance && !controller.navMeshAgent.pathPending)
        {
            controller.nextWayPoint = (controller.nextWayPoint + 1) % controller.wayPoints.Count;
        }
    }
}