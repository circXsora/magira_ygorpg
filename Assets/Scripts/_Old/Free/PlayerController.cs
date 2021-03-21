using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Magia.SingletonInScene<PlayerController>
{

    public float Speed = 2f;

    Vector2 velocity;
    Rigidbody2D body;
    Animator animator;
    SpriteRenderer sprite_renderer;
    public MonsterController.MonsterData[] Datas;
    public void YamiYugiKita()
    {
        sprite_renderer.enabled = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite_renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        velocity.x = Input.GetAxis("Horizontal");
        velocity.y = Input.GetAxis("Vertical");

        if (velocity.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (velocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        animator.SetBool("Run", velocity.sqrMagnitude > 0);
    }
    private void FixedUpdate()
    {
        body.velocity = velocity * Speed;
    }
}
