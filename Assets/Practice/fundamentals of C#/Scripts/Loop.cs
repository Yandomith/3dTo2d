using UnityEngine;

public class Loop : MonoBehaviour
{
    int count = 0;

    private void Start()
    {
        for (int i = 1; i <= 10 ; i++)
       {

           Debug.Log("using for Current Count: "+ i );
           
       }
       
       while (count <= 20)
       {
           
           Debug.Log("using while Current Count: " + count);
           count += 2;
       }
    }
}
