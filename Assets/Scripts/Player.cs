using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;

    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private GameObject _tripleShotPrefab;

    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -1;

    [SerializeField]
    private int _lives = 3;

    [SerializeField]
    private bool _isTripleShotActive = true;

    private SpawnManager _spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        //take the current position = new position(0, 0, 0)
        transform.position = new Vector3(0,0,0);

        _isTripleShotActive = true;

        // find the Spawn_Manager object and get SpawnManager component
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire){
            //Debug.Log("Space Key Pressed");
            // spawn the laser prefab
            _canFire = Time.time + _fireRate;
            if (_isTripleShotActive)
                Instantiate(_tripleShotPrefab, transform.position + new Vector3(1.90594f,1.847405f,-2.609898f), Quaternion.identity);
            else
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }
    }

    void CalculateMovement() 
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        //transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
        //transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * _speed * Time.deltaTime);

        /*if (transform.position.y >= 0){
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if  (transform.position.y <= -3.8f){
            transform.position = new Vector3(transform.position.x, -3.8f, 0);
        }*/
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        if (transform.position.x > 11.3f){
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if  (transform.position.x < -11.3f){
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    public void Damage(){
          _lives -= 1;

        if (_lives < 1){

            if (_spawnManager != null) {
                _spawnManager.StopSpawning();
            }

            Destroy(this.gameObject);
        }
    }
}
