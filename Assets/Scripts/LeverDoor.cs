using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverDoor : MonoBehaviour
{
    // Start is called before the first frame update
    public bool PlayerInRange = false;
    [SerializeField] private GameObject Lever;
    [SerializeField] private Sprite LeverOpenSprite;
    [SerializeField] private GameObject Door;
    [HideInInspector] public Knight Player;
   
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerInRange && Input.GetKey(KeyCode.F) && Lever.GetComponent<SpriteRenderer>().sprite != LeverOpenSprite)
        {
            Lever.GetComponent<SpriteRenderer>().sprite = LeverOpenSprite; 
            Door.gameObject.transform.localScale = new Vector2(1, 1);
            Destroy(Door.GetComponent<BoxCollider2D>());
            FindObjectOfType<AudioPlayer>().PlayOneShot(Player.SFX_Switch);
        }
    }
}
