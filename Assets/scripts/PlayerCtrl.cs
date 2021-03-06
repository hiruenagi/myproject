using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class PlayerCtrl : MonoBehaviour
{
    // Start is called before the first frame update
    public float MoveForce = 100.0f;
    public float MaxSpeed = 5;
    public Rigidbody2D HeroBody;
    [HideInInspector]
    public bool bFaceRight = true;
    [HideInInspector]
    public bool bJump = false;
    public float JumpForce = 100;

    private Transform mGroundCheck;
    public Animator anim;
    public AudioClip[] JumpClips ;
    public AudioSource audiosource;
    float mVolume=0;
    AudioMixer audiomix;
    void Start()
    {
        HeroBody = GetComponent<Rigidbody2D>();
        mGroundCheck = transform.Find("GroundCheck");
        anim = GetComponent<Animator>();
        audiosource = GetComponent<AudioSource>();
    }
    
    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        if (Mathf.Abs(HeroBody.velocity.x) < MaxSpeed)
        {
            HeroBody.AddForce(Vector2.right * h * MoveForce);
        }

        if (Mathf.Abs(HeroBody.velocity.x) > 5)
        {
            HeroBody.velocity = new Vector2(Mathf.Sign(HeroBody.velocity.x) * MaxSpeed,
                                            HeroBody.velocity.y);
        }

        anim.SetFloat("speed", Mathf.Abs(h));

        if (h > 0 && !bFaceRight)
        {
            flip();
        }
        else if (h < 0 && bFaceRight)
        {
            flip();
        }
        //射线检测是通过按位与的操作进行而不是通过“==”操作进行判断
        if (Physics2D.Linecast(transform.position, mGroundCheck.position,
                                1 << LayerMask.NameToLayer("Ground")))
        {
            if (Input.GetButtonDown("Jump"))
            {
                bJump = true;
            }
        }

        Debug.DrawLine(transform.position, mGroundCheck.position, Color.red);
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            mVolume++;
            audiomix.SetFloat("MasterVolume", mVolume);
        }
        if (Input.GetKeyDown(KeyCode.Underscore))
        {
            mVolume--;
            audiomix.SetFloat("MasterVolume", mVolume);
        }
            if (bJump)
        {
            int i=UnityEngine.Random.Range(0,JumpClips.Length);
            //AudioSource.PlayClipAtPoint(JumpClips[i], transform.position);//播放声音
            audiosource.clip = JumpClips[i];
            HeroBody.AddForce(Vector2.up * JumpForce);
            bJump = false;
            anim.SetTrigger("jump");
        }
    }

    private void flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        bFaceRight = !bFaceRight;
    }
}
