 using UnityEngine;

public class PlayerPaddle : MonoBehaviour
{
    private Vector2 _direction;
    protected Rigidbody2D _rigidbody;
    public float speed = 75.0f;

    
    private void Update(){


        if(Input.GetKey(KeyCode.UpArrow)){
            _direction = Vector2.up;
        } else if (Input.GetKey(KeyCode.DownArrow)){
            _direction = Vector2.down;
        } else {
            _direction = Vector2.zero;
        }
        
    }

    private void FixedUpdate(){
        if(_direction.sqrMagnitude > 0){
            _rigidbody.AddForce(_direction * this.speed);
        }
    }
        private void Awake(){
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void ResetPosition(){
        _rigidbody.position = new Vector2(_rigidbody.position.x, 0.0f);
        _rigidbody.linearVelocity = Vector2.zero;
    }


    public void MoveUp(){
        _direction = Vector2.up;
    }

    public void MoveDown(){
        _direction = Vector2.down;
    }

    public void StopMoving(){
        _direction = Vector2.zero;
    }


}
