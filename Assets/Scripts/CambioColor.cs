using UnityEngine;

public class CambioColor : MonoBehaviour
{
    public GameObject model;
    public Color colores;
    public Material material;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CambioColor1()
    {
        model.GetComponent<Renderer>().material.color = colores;
        material.color = colores;

    }
}
