using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class demoTargetBlink : MonoBehaviour
{
    [SerializeField] private MeshFilter target_mesh;
    [SerializeField] private Mesh[] target_mesh_states;
    [SerializeField] private float blinkspeed = 0.5f;
    [SerializeField] private float max_cycle = 4;
    private int bid = 0;

    void Start()
    {
        StartCoroutine(BlinkTarget());
    }

    IEnumerator BlinkTarget()
    {
        while (true)
        {
            yield return new WaitForSeconds(blinkspeed);

            target_mesh.mesh = target_mesh_states[bid];

            if (++bid >= max_cycle) bid = 0;
        }
    }
}
