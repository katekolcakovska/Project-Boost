
using UnityEngine;

public class Oscillator : MonoBehaviour
{

    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range (0,1)] float movementFactor;
    [SerializeField] float period = 2f;
    Vector3 startingPosition;
    

    void Start()
    {
        startingPosition = transform.position;
       
    }

    void Update()
    {
        /*if (period == 0f) { return; }*/ // ova ne e predictable so floats zatoa ke koristime:
        if(period <= Mathf.Epsilon) { return; }
        float cycles = Time.time / period; // continually growing over time

        const float tau = Mathf.PI * 2; // constant value of 6.283
        float rawSineWave = Mathf.Sin(cycles * tau); //going from -1 to 1

        movementFactor = (rawSineWave + 1f) / 2f; //recalculated to go from 0 to 1 so its cleaner

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
