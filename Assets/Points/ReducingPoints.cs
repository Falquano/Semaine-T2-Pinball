using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReducingPoints : PointGiver
{
    [SerializeField] private int maxHP = 3;
    private int HP;
    public float ModPerPassage => 1f / (float)(maxHP + 1);
    private float scale = 1f;
    [SerializeField] private float pointMod = 1f;
    [SerializeField] private float pointMultiplierIncrease = .5f;

    private void Start()
    {
        OnPoints.AddListener(GotPoint);
        HP = maxHP;
    }

    public override int Points => (int) ((float) points * pointMod);

    public void GotPoint()
    {
        scale -= ModPerPassage;
        transform.localScale = Vector3.one * scale;
        pointMod += pointMultiplierIncrease;
        HP--;

        if (HP <= 0)
            Destroy(gameObject);
    }
}
