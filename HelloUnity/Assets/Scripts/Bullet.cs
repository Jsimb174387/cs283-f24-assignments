using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 2f;
    public int damage = 5;
    public float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = lifeTime;
        transform.SetParent(null);
    }

    // Update is called once per frame
    void Update()
    {
        // need to move bullet in the x direction
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<BehaviorMinion>().TakeDamage(damage);
            Destroy(gameObject);
        }
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
