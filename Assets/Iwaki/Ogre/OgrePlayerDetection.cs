using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OgrePlayerDetection : MonoBehaviour
{
    [SerializeField] OgreController ogre;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMove>() != null) ogre.ChangeThrowable(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMove>() != null) ogre.ChangeThrowable(false);
    }
}
