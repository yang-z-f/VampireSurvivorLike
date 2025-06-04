using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    [SerializeField] private float speed = 0.4f;
    [SerializeField] private float minSize = 0.85f;
    [SerializeField] private float maxSize = 1.15f;
    private float activeSize;
    void Start()
    {
        activeSize = maxSize;
        speed = speed * Random.Range(.8f, 1.2f);
    }
    void Update()
    {
        EnemyTransform();
    }
    private void EnemyTransform()
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.one * activeSize, speed * Time.deltaTime);
        if(transform.localScale.x == activeSize)
        {
            if(transform.localScale.x == maxSize)
            {
                activeSize = minSize;
            }
            else
            {
                activeSize = maxSize;
            }
        }
    }
}
