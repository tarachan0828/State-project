using UnityEngine;
using System;

public class AlertManager : MonoBehaviour
{
    public static AlertManager Instance;
    public event Action<Vector3> OnAlert; // 座標付きのアラートイベント

    private void Awake() { Instance = this; }

    public void ReportPlayerLocation(Vector3 position)
    {
        // アラートを発信
        OnAlert?.Invoke(position);
    }
}