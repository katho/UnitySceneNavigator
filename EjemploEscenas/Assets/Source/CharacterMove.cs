using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CharacterMove : MonoBehaviour
{
    Vector3 translateVector;
    public float movementSpeed = 18;
    public Rigidbody rigidBody;

    private bool blackScreen = false;

    public Texture2D customGuiTexture;

    public GUIStyle customStyle;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Mover el cubo usando el teclado
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalAxis,0,verticalAxis) * movementSpeed * Time.deltaTime;

        rigidBody.MovePosition(transform.position + movement);
    }

    //Detectar la colisión
    private void OnTriggerEnter(Collider other) {
        //Detectar con qué objeto colisiona
        if(other.gameObject.name == "door1")
        {
            Debug.Log("door1");
            //Ocultar la pantalla
            StartCoroutine("BlackenScreen");
            //ir al otro lado - aquí de ida
            if(other.gameObject.transform.position.x < transform.position.x)
            {
                transform.position = new Vector3(-0.43f,1.6f,-5.84f);
                rigidBody.MovePosition(transform.position);
            }
            else{//ir al otro lado - aquí de vuelta
                transform.position = new Vector3(2.43f,1.6f,-5.84f);
                rigidBody.MovePosition(transform.position);
            }
            
                
        }
        //Cambiar la escena
        if(other.gameObject.name == "door2")
        {
             Debug.Log("door2");
            //Obtener el nombre de la escena
             Scene currentScene = SceneManager.GetActiveScene();
             //Moverese entre escenas
             if(currentScene.name == "Scene2")
             {
                 SceneManager.LoadScene("Scene1");
             }
             else if(currentScene.name == "Scene1")
             {
                 SceneManager.LoadScene("Scene2");
             }
             

        }
        
    }

    //Poner la pantalla en negro
    private void OnGUI() {
        customStyle = new GUIStyle();
        if(blackScreen)
        {
           GUI.Box(new Rect(-100, 0, Screen.width, Screen.height), customGuiTexture); 
        }
        
    }

    //Corutina para poner en negro la pantalla
    IEnumerator BlackenScreen()
    {
        for(int x = 0; x < 3; x++)
        {
            blackScreen = true;
            
        }

        yield return new WaitForSeconds(2.0f);

        blackScreen = false;

    }

}
