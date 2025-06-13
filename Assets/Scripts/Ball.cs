using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour
{

    public float speed = 100.0f;
    private Rigidbody2D _rigidbody;

    private void Awake(){
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    
    private void Start(){
        StartCoroutine(Time());
        ResetPosition();
        AddStartingForce();
    }

    private IEnumerator Time(){
        yield return new WaitForSeconds(5);
    }

    public void ResetPosition(){
        _rigidbody.position = Vector3.zero;
        _rigidbody.linearVelocity = Vector3.zero;
    }

    public void AddStartingForce(){
        float x = Random.value < 0.5f ? -1.0f : 1.0f;
        float y = Random.value < 0.5f ? Random.Range(-1.0f, -0.1f) : Random.Range(0.1f, 1f);

        Vector2 direction = new Vector2(x,y);
        direction = direction.normalized;
        _rigidbody.AddForce(direction * this.speed);
    }

    public void AddForce(Vector2 force){
        _rigidbody.AddForce(force);
    }

    
}
