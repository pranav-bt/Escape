using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Direction = 0.0f;
    float timer = 5.0f;
    Rigidbody2D MyRigidBody;
    // Start is called before the first frame update
    void Start()
    {
        MyRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MyRigidBody.velocity = new Vector2(2.0f * Direction, 0.0f);
        timer -= Time.deltaTime;
        if(timer <= 0.0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            if (other.gameObject.GetComponent<Zombie>().Health > 0)
            {
                //FindObjectOfType<AudioPlayer>().PlayOneShot(SFX_Damage);
                other.gameObject.GetComponent<Zombie>().ProjectileDamage();              
            }
        }
        if(other.gameObject.tag != "Player")
        {
            Destroy(gameObject);
        }
    }
}
