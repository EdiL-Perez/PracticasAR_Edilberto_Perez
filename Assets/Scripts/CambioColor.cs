using UnityEngine;

public class CambioColor : MonoBehaviour
{
    public GameObject model;
    //public Color colores;
    public int indiceMaterial = 2;

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

            
            if (indiceMaterial < materiales.Length)
            {
                
                Color colorAzar = Random.ColorHSV(0f, 1f, 0.8f, 1f, 0.8f, 1f);
                materiales[indiceMaterial].color = colorAzar;

                
                rend.materials = materiales;
            }

        }
    }
}
