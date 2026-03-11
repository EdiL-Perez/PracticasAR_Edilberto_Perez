using UnityEngine;

public class CambioColor : MonoBehaviour
{
    public GameObject model;
    //public Color colores;
    public int indiceMaterial = 2;
    public int indiceMaterial2 = 3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //model.GetComponent<Renderer>().material.color = Color.black;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CambioColor1()
    {
        Renderer rend = model.GetComponent<Renderer>();
        if (rend != null)
        {
            Material[] materiales = rend.materials;

            
            if (indiceMaterial < materiales.Length && indiceMaterial2 < materiales.Length)
            {
                
                
                materiales[indiceMaterial].color = Random.ColorHSV(0f, 1f, 0.8f, 1f, 0.8f, 1f);
                materiales[indiceMaterial2].color = Random.ColorHSV(0f, 0.8f, 0.8f, 1f, 1f, 0.8f);


                rend.materials = materiales;
            }

        }
    }
}
