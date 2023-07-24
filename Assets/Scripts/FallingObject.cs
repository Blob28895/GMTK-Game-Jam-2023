using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    [Tooltip("Amount of time in seconds that the object will take to despawn after hitting the ground.")]
    [SerializeField] private float _groundTime;

    

    private Animator _animator;
    private bool _isGrounded;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
            _animator.SetTrigger("hitGround");
            Destroy(gameObject, _groundTime);
        }
	}


	private void OnTriggerEnter2D(Collider2D collision)
	{
        if(!collision.CompareTag("Mouse") || _isGrounded)
        {
            return;
        }

        
        GameObject mouse;
        if(collision.name.ToLower().Contains("collider"))
        {
            mouse = collision.transform.parent.gameObject;
        }
        else
        {
            mouse = collision.gameObject;
        }
        mouse.GetComponent<MouseController>().Die();
	}
}