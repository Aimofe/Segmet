using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.
public class resaltable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public string titulo;
    public string des;
    public GameObject popup;
    Vector3 maxScale;
    Vector3 minScale;
    GameObject referencia;

    // Start is called before the first frame update
    void Start()
    {
        maxScale = new Vector3(1f , 1f, 1f);
        minScale = new Vector3(0, 0, 0);

        referencia = GameObject.FindGameObjectWithTag("Referencia");
        //popup.SetActive(false);
        popup.transform.GetChild(0).GetComponent<Text>().text = titulo;
        popup.transform.GetChild(1).GetComponent<Text>().text = des;
        //popup.transform.position = new Vector2(1390, 789);
        popup.GetComponent<RectTransform>().position = referencia.transform.position;
        popup.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(closeDes());
        }
    }
    //CONTROL POP-UP
    private void OnMouseEnter()
    {
       
            if (!Input.GetMouseButton(0))
            {
                
            }
        
   
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        popup.SetActive(true);
        StartCoroutine(openDes());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(closeDes());
    }
    
    IEnumerator openDes()
    {
        yield return new WaitForSeconds(0.0001f);
        popup.transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
        if(popup.transform.localScale.x <= maxScale.x)
        {
            StartCoroutine(openDes());
        }
        
    }
    IEnumerator closeDes()
    {
        yield return new WaitForSeconds(0.001f);
        popup.transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
        if(popup.transform.localScale.x >= minScale.x)
        {
            StartCoroutine(closeDes());
        }
        else
        {
            popup.SetActive(false);
        }
        
    }

}
