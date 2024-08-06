using UnityEngine;
using UnityEngine.UI;

public class HeightUI : MonoBehaviour
{
    [SerializeField] Slider heightSlider;
    [SerializeField] float addValue = 0.1f;
    [SerializeField] bool isClimb = false;
    [SerializeField] bool isDrop = false;
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
        if (isDrop == true)
        {
            heightSlider.value -= addValue;
            isDrop = false;
        }
    }

    public void Climb()
    {
        isClimb = true;
    }

    public void Drop()
    {
        isDrop = true;
    }
}
