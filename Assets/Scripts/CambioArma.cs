using UnityEngine;

public class CambioArma : MonoBehaviour
{
    public GameObject[] armas; 
    public Transform PivoteArma;

    private GameObject armaActual;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void cambiarArma()
    {
        if (armaActual != null)
        {
            Destroy(armaActual);

        }
        if(armas.Length > 0)
        {
            int indice = Random.Range(0, armas.Length);
            armaActual = Instantiate(armas[indice],PivoteArma);

            armaActual.transform.localPosition = Vector3.zero;
            armaActual.transform.localRotation = Quaternion.identity;
        }
    }
}
