using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public float jumpforce;
    public Collider2D coll;
    public Animator anim;
    public LayerMask ground;
    public int Cherry;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        SwitchAnim();
    }

    void Movement()
    {
        float horizontalmove = Input.GetAxis("Horizontal");
        float facedirection = Input.GetAxisRaw("Horizontal");
        //½ÇÉ«ÒÆ¶¯
        if (horizontalmove !=0)
        {
            rb.velocity = new Vector2(horizontalmove * speed * Time.deltaTime, rb.velocity.y);
            anim.SetFloat("running", Mathf.Abs(facedirection));
        }
        if (facedirection!=0)
        {
            transform.localScale = new Vector3(facedirection,1,1);
        }
        //½ÇÉ«ÌøÔ¾
        if (Input.GetButtonDown("Jump")&&coll.IsTouchingLayers(ground))


        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce * Time.deltaTime);
            anim.SetBool("jumping", true);
        }
    }

    void SwitchAnim()
    { anim.SetBool("idle", false);
        if(anim.GetBool("jumping"))
        {
           
            if (rb.velocity.y<0)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
        }else if (coll.IsTouchingLayers(ground))
        {
            anim.SetBool("falling", false);
            anim.SetBool("idle", true);
        }

                }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Collection")
        {
            Destroy(collision.gameObject);
            Cherry += 1;
        }
    }
}
