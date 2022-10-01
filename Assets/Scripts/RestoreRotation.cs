using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RestoreRotation : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform CoinTextSocket;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(CoinTextSocket.position.x + 0.65f, CoinTextSocket.position.y + 1.4f, CoinTextSocket.position.z);
    }

    private void OnEnable()
    {
        StartCoroutine("DisableTimer");
        
    }
    private void OnDisable()
    {
        
    }
    public IEnumerator DisableTimer()
    {
        yield return new WaitForSeconds(4.0f);
        StartCoroutine(FadeTextToZeroAlpha(1f, GetComponentInChildren<TextMeshProUGUI>()));
    }

    public IEnumerator FadeTextToZeroAlpha(float t, TextMeshProUGUI i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
        gameObject.SetActive(false);
    }


}
