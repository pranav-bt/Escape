using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapView : MonoBehaviour
{
    bool MapActive = false;
    Knight player;
    private void Start()
    {
        EventManager.MapView += ToggleMapView;
        player = FindObjectOfType<Knight>();
        gameObject.SetActive(false);
    }
    private void ToggleMapView()
    {
        if (!MapActive)
        {
            gameObject.transform.position = new Vector3(player.gameObject.transform.position.x, player.gameObject.transform.position.y, -50);
            gameObject.SetActive(true);
            MapActive = true;
        }
        else
        {
            gameObject.SetActive(false);
            MapActive = false;
        }
    }
    public void OnEnable()
    {
        if (player)
        {
            player.stateManager.IdleReturn();
            player.CanMove = false;
        }
    }

    private void OnDisable()
    {
        player.CanMove = true;
    }
}
