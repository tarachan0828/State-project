using UnityEngine;

[CreateAssetMenu(menuName = "AI/State")]
public class State : ScriptableObject
{
    public AIAction[] actions;       // この状態で実行する行動
    public Transition[] transitions; // 次のステートへの遷移条件
    public Color sceneGizmoColor = Color.grey; // デバッグ用

    public void UpdateState(StateController controller)
    {
        ExecuteActions(controller);
        CheckTransitions(controller);
    }

    private void ExecuteActions(StateController controller)
    {
        foreach (var action in actions) action.Act(controller);
    }

    private void CheckTransitions(StateController controller)
    {
        foreach (var transition in transitions)
        {
            bool decisionSucceeded = transition.decision.Decide(controller);
            if (decisionSucceeded)
            {
                controller.TransitionToState(transition.trueState);
            }
            else
            {
                controller.TransitionToState(transition.falseState);
            }
        }
    }
}

[System.Serializable]
public class Transition
{
    public AIDecision decision;
    public State trueState;
    public State falseState;
}