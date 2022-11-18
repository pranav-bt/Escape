using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    Vector3 LocalScaleOfHealthBar;
    // Start is called before the first frame update
    void Start()
    {
        LocalScaleOfHealthBar = transform.localScale;
    }

    public void UpdateHealthBar(float Health)
    {
        LocalScaleOfHealthBar.x = Health/100;
        transform.localScale = LocalScaleOfHealthBar;
    }
}
