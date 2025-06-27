using UnityEngine;

namespace Practice.fundamentals_of_C_.Scripts
{
    public class DelegateExample : MonoBehaviour
    {
        public delegate void Notify(string message);
        
        private void Start()
        {
            Notify notify;
            notify = ShowMessage; // Assigning the method to the delegate
            notify += ShowWarning; // Adding another method to the delegate
            notify("Hello, this is a delegate example!"); // Invoking the delegate
            
        }
        public void ShowMessage(string message)
        {
            Debug.Log("Message: " + message);
        }
        public void ShowWarning(string message)
        {
            Debug.LogWarning("Warning: " + message);
        }
    }
}