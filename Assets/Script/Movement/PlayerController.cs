using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float _yAxis, _xAxis, _mouseX,  _movementSpeed = .25f, _heigth = 0,  _jumpTimer =0, _currentJumpCoolDown = 0;
    private bool _isFalling = false, _isJumping = false, _jumpEnded = false;
    private Vector3 _startingPosition;

    private Animator _animator;
    private CharacterController _characterController;

    [HideInInspector] public Animator Animator { get => _animator; set => _animator = value; }
    [HideInInspector] public CharacterController CharacterController { get => _characterController; set => CharacterController = value; }

    [SerializeField] float _sensivity = 0f;
    [Space]
    [SerializeField] float _maxHeigth = 1.2f;
    [SerializeField] float _jumpLength = .6f;
    [SerializeField] float _jumpSpeed = .1f;
    [SerializeField] float _jumpCoolDown = .5f;
    

    // Start is called before the first frame update
    void Start()
    {
        Setup();
    }

    void FixedUpdate()
    {
        UpdateAxis();
        HanddleAnimation();
        HanddleMovement();
        HanddleRotation();
        HandleJump();
    }

    private void Setup()
    {
        _animator = GetComponentInChildren<Animator>();
        _characterController = GetComponent<CharacterController>();
        _startingPosition = transform.localPosition;
    }

    private void HanddleMovement()
    {
        //_characterController.Move(transform.forward * (_yAxis * _movementSpeed));
        //_characterController.Move(transform.right * _yAxis + transform.forward );
        transform.parent.Rotate(Vector3.forward, _xAxis * 1.5f);
    }

    private void HanddleRotation()
    {
    }

    private void UpdateAxis()
    {
        _yAxis = Input.GetAxisRaw("Vertical");
        _xAxis = Input.GetAxisRaw("Horizontal");
        _mouseX = Input.GetAxisRaw("Mouse X");
    }

    private void HanddleAnimation()
    {
        _animator.SetFloat("Sides",_xAxis);
        if(_currentJumpCoolDown == 0 & Input.GetKey(KeyCode.Space) && _animator.GetFloat("Jump") < 1)
        {
            _animator.SetFloat("Jump",1);
            _isJumping = true;
            StartCoroutine("HandleJump");
        }
        if(Input.GetKey(KeyCode.LeftControl) && _animator.GetFloat("Crounch") < 1)
        {
            _animator.SetFloat("Crounch",1);
        }

        if(_xAxis != 0)
        {
            _animator.SetFloat("Front",0);
        }
        else
        {
            _animator.SetFloat("Front",1);
        }
    }

    private IEnumerator HandleJump()
    {
        while(_isJumping)
        {
            _jumpTimer += _jumpSpeed;
            _heigth = Mathf.Sin(_jumpTimer * _jumpLength) * _maxHeigth;
            Debug.Log(_heigth + " " + _isJumping.ToString());

            if((_maxHeigth * _jumpLength) * Mathf.Cos(_jumpTimer * _jumpLength) <=0)
            {
                _isFalling = true;
            }

            if(_isFalling)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - _heigth, transform.localPosition.z);
            }
            else
            {
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + _heigth, transform.localPosition.z);
            }
            
            if(_heigth <= 0)
            {
                _heigth = 0;
                _jumpTimer = 0;
                _isJumping = false;
                _isFalling = false;
                transform.localPosition = _startingPosition;
                _animator.SetFloat("Jump",0);
            }
         yield return new WaitForSeconds(.01f);
        }  
        yield return null;
        StartCoroutine("StartJumpCoolDown");
    }

    private IEnumerator StartJumpCoolDown()
    {
        while(_currentJumpCoolDown <= _jumpCoolDown)
        {
            _currentJumpCoolDown += 0.01f;
            _isJumping = false;
            yield return new WaitForSeconds(.01f);
        }
        _currentJumpCoolDown = 0;
        yield return null;
    }
}
