using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Transform _player;
    [SerializeField]
    float orthSize = 13.21f;
    [SerializeField]
    float delay;
    Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPosition = new Vector3(Mathf.Lerp(transform.position.x, _player.position.x, delay * Time.deltaTime), Mathf.Lerp(transform.position.y, _player.position.y, delay * Time.deltaTime), transform.position.z);
        float velocity = _player.GetComponent<Rigidbody2D>().velocity.y;
        float camOrthSize = mainCamera.orthographicSize;
        mainCamera.orthographicSize = Mathf.Lerp(camOrthSize, orthSize, delay * Time.deltaTime);
        transform.position = newPosition;
    }
}
