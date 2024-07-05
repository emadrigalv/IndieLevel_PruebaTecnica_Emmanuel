using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarManager : MonoBehaviour
{
    public AnimationCurve pillarAscendCurve;

    public void Awake()
    {
        ModularPillar.distanceCurve = pillarAscendCurve;
    }
}
