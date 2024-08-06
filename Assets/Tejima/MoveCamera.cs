using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] GameObject[] cameras = new GameObject[10];
    [SerializeField] int cameraNumber = 0;//ÉJÉÅÉâÇÃîzóÒî‘çÜ
    [SerializeField] int camerasHigh = 10;//ÉJÉÅÉâÇÃçÇÇ≥
    HeightUI heightUI;
    PlayerMove player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cameras[cameraNumber].transform.position.y + camerasHigh < player.gameObject.transform.position.y)
        {
            UpCamera();
            heightUI.Climb();
        }
        else if (cameras[cameraNumber].transform.position.y - camerasHigh > player.gameObject.transform.position.y)
        {
            DownCamera();
            heightUI.Drop();
        }
    }

    public void UpCamera()
    {
        if (cameraNumber == 9) {; }

        else
        {
            cameraNumber++;
            cameras[cameraNumber].SetActive(true);
            cameras[cameraNumber - 1].SetActive(false);
        }
    }

    public void DownCamera()
    {
        if (cameraNumber == 0) {; }

        else
        {
            cameraNumber--;
            cameras[cameraNumber].SetActive(true);
            cameras[cameraNumber + 1].SetActive(false);
        }
    }

}