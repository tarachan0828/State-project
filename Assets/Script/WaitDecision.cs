using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decision/Wait")]

public class WaitDecision : AIDecision
{
    public float waitTime = 5f;//‰½•b‘Ò‚Â‚©

    public override bool Decide(StateController controller)
    {
        return controller.stateTimeElapsed >= waitTime;
    }
}
