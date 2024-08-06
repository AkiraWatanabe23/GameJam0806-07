using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] int camerasHigh = 10;//ÉJÉÅÉâÇÃçÇÇ≥
    Transform myTransform;
    Vector2 pos;
    HeightUI heightUI;
    PlayerMove player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerMove>();
        myTransform = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        pos = myTransform.position;
        if (transform.position.y + camerasHigh < player.gameObject.transform.position.y)
        {
            UpCamera();
            heightUI.Climb();
        }
        else if (transform.position.y - camerasHigh > player.gameObject.transform.position.y)
        {
            DownCamera();
            heightUI.Drop();
        }
    }

    public void UpCamera()
    {
        pos.y += camerasHigh;
    }

    public void DownCamera()
    {
        pos.y -= camerasHigh;
    }

}