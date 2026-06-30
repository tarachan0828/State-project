using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Search")]
public class SearchAction : AIAction
{
    public override void Act(StateController controller)
    {
        // ÅŒã‚ÉŒ©¸‚Á‚½êŠ‚ÖŒü‚©‚¤
        controller.navMeshAgent.destination = controller.lastKnownPosition;
        controller.navMeshAgent.speed = 4f; // ‚¿‚å‚Á‚Æ‹}‚¢‚Ås‚­
    }
}