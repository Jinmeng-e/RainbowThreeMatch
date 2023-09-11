using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sfx : MonoBehaviour
{
    static Sfx sfx;
    public static Sfx Instnace => sfx;
    [SerializeField] GameObject boom;
    [SerializeField] GameObject click;

    public void Awake()
    {
        sfx = this;
    }
    public void Pop(Vector3 uiPos, Color c)
    {
        var go = Instantiate(boom);
        var particle = go.GetComponentInChildren<ParticleSystem>().main;
        particle.startColor = c;

        go.transform.position = uiPos;
    }
}
