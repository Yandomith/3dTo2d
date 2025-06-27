using UnityEngine;


    public class Field: MonoBehaviour
    {
        public string brandName;
        private int maxSpped;
        [SerializeField] private int fuelLevel;
        
        private void Start()
        {
            Field car = new Field();
            car.brandName = "Toyota";
            car.maxSpped = 180;
            car.fuelLevel = 100;
            
            Debug.Log("Brand Name: " + car.brandName);
            Debug.Log("Max Speed: " + car.maxSpped);
            Debug.Log("Fuel Level: " + car.fuelLevel);
        }
    }
