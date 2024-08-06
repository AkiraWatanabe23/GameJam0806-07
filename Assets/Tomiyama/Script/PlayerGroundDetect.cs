using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundDetect : MonoBehaviour
{
    PlayerMove _playerMove;
    void Start()
    {
        _playerMove = FindObjectOfType<PlayerMove>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Ground"))
        {
            _playerMove.CanJump = true;
        }
    }
}
