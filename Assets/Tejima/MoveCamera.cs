using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] float camerasHigh = 5.4f;//ÉJÉÅÉâÇÃçÇÇ≥
    Transform myTransform;
    Vector3 pos;
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
            Debug.Log("Hit");
            pos.y += camerasHigh * 2;
            //heightUI.Climb();
        }
        else if (transform.position.y - camerasHigh > player.gameObject.transform.position.y)
        {
            pos.y -= camerasHigh * 2;
            //heightUI.Drop();
        }
        myTransform.position = pos;
    }
}