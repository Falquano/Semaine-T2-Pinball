using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DingPointGiver : MonoBehaviour
{
    [SerializeField] protected PointsManager pointsManager;
    [SerializeField] protected int defaultPoints = 100;
    [SerializeField] protected int specialPoints = 500;

    [SerializeField] private int maxHP = 3;
    [SerializeField] private int HP;


    private void Start()
    {
        HP = maxHP;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bille"))
            OnTouch();
    }

    private void OnTouch()
    {
        HP--;
        if (HP <= 0)
            SpecialDing();
        else
            NormalDing();
    }

    private void SpecialDing()
    {
        HP = maxHP;
        pointsManager.Points += specialPoints;
        UnStomp();
    }

    private void NormalDing()
    {
        pointsManager.Points += defaultPoints;
        Stomp();
    }

    private void Stomp()
    {
        //Debug.Log(string.Format("HP : {0}, Max HP : {1}, {0}/{1} = {2} !", HP, maxHP, HP / ))
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, (float)HP / (float)maxHP);
        transform.position += new Vector3(0, 0, (.5f / (float)maxHP));
    }

    private void UnStomp()
    {
        for (int i = 0; i < maxHP - 1; i++)
        {
            transform.position -= new Vector3(0, 0, (.5f / (float)maxHP));
        }
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 1);
    }
}
