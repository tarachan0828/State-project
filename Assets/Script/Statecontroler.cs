using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic; // 追加

public class StateController : MonoBehaviour
{
    public State alertSearchState;
    public State currentState;
    public State remainInState;
    public List<Transform> wayPoints; // 巡回地点のリスト

    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public Transform target;
    [HideInInspector] public float stateTimeElapsed;
    [HideInInspector] public Vector3 lastKnownPosition; // 最後に見失った場所
    [HideInInspector] public int nextWayPoint; // 次の巡回地点のインデックス


    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        stateTimeElapsed += Time.deltaTime; // 時間をカウント
        currentState.UpdateState(this);
    }

    public void TransitionToState(State nextState)
    {
        if (nextState != remainInState)
        {
            currentState = nextState;
            stateTimeElapsed = 0; // ステートが変わったらタイマーリセット
        }
    }

    // プレイヤーを見失った時に座標をメモするメソッド
    public void UpdateLastKnownPosition()
    {
        lastKnownPosition = target.position;
    }

    // 敵にアタッチされているColliderが何かに触れた時に呼ばれる
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("プレイヤーにダメージ！");
            // ここに「プレイヤーのHPを減らす」などの処理を書く
        }
    }
    void OnEnable()
    {
        // AlertManagerが通報したら、自分の「最後に見失った場所」を更新する
        if (AlertManager.Instance != null)
            AlertManager.Instance.OnAlert += ReceiveAlert;
    }

    // 通報を受けた時の処理
    void ReceiveAlert(Vector3 alertPos)
    {
        lastKnownPosition = alertPos;

        // もし今、追いかけ中（Chase）でなければ、通報場所へ急行させる
        if (currentState.name != "ChaseState")
        {
            Debug.Log(gameObject.name + "：通報地点へ急行する！");

            // 重要：ここでステートを切り替える！
            TransitionToState(alertSearchState);
        }
    }
}