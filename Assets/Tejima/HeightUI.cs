using UnityEngine;
using UnityEngine.UI;

public class HeightUI : MonoBehaviour
{
    [SerializeField] Slider heightSlider;
    [SerializeField] float addValue = 0.1f;
    [SerializeField] bool isClimb = false;
    HitPointUI hitPointUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isClimb == true)
        {
            heightSlider.value += addValue;
            isClimb = false;
        }
    }

    public void Climb()
    {
        isClimb = true;
    }
}
