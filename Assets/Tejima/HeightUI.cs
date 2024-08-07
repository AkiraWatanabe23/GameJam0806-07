using UnityEngine;
using UnityEngine.UI;

public class HeightUI : MonoBehaviour
{
    [SerializeField] Slider heightSlider;
    [SerializeField] float addValue = 0.1f;
    [SerializeField] bool isClimb = false;
    [SerializeField] bool isDrop = false;
    [SerializeField] int deepDownCount = 0;//‰Šú’n“_‚æ‚è‰½‰ñ•ª‰º‚ÉƒJƒƒ‰‚ª“®‚¢‚Ä‚¢‚é‚©
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isClimb == true)
        {
            if (deepDownCount > 0)
            {
                deepDownCount--;
                isClimb = false;
            }
            else
            {
                heightSlider.value += addValue;
                isClimb = false;
            }
        }
        if (isDrop == true)
        {
            if (heightSlider.value == 0)
            {
                deepDownCount++;
                isDrop = false;
            }
            else
            {
                heightSlider.value -= addValue;
                isDrop = false;
            }
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
