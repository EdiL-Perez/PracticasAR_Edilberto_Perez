using UnityEngine;

public class ControladorEquipo : MonoBehaviour
{

    public Transform manoSlot; // Arrastra aquí el objeto "ManoSlot"
    private GameObject objetoActual;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void EquiparObjeto(GameObject prefab)
    {
        
        Desequipar();

        objetoActual = Instantiate(prefab, manoSlot);

       
        objetoActual.transform.localPosition = Vector3.zero;
        objetoActual.transform.localRotation = Quaternion.identity;
    }

    public void Desequipar()
    {
        if (objetoActual != null)
        {
            Destroy(objetoActual);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
