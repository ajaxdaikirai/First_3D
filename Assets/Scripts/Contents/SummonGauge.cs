using System.Collections;
using UnityEngine;

public class SummonGauge : MonoBehaviour
{
    // 서몬 게이지
    [SerializeField]
    private float _gauge = 0;
    [SerializeField]
    private float _increasePerTick = Define.INCREASE_SUMMON_GAUGE_PER_TICK;
    [SerializeField]
    private float _increaseInterval = Define.INCREASE_GAUGE_INTERVAL;

    private float _maxGauge = Define.MAX_SUMMON_GAUGE;

    public float Gauge { get { return _gauge; } }

    public void StartGaugeIncreasing()
    {
        StartCoroutine(IncreaseGauge());
    }

    private IEnumerator IncreaseGauge()
    {
        while (true)
        {
            yield return new WaitForSeconds(_increaseInterval);
            if (_gauge >= _maxGauge)
                continue;

            _gauge = Mathf.Clamp(_gauge += _increasePerTick, 0, _maxGauge);
        }
    }
}
