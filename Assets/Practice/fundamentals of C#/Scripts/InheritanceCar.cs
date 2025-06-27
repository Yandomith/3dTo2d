using UnityEngine;

public class InheritanceCar : MonoBehaviour
{
    private void Start()
    {
        Car car = new Car();
        car.StartEngine(); // This will cause an error because StartEngine is not accessible
        car.Drive(); // This will work because Drive is accessible
    }
}

public class Vechicle
{
    public void StartEngine()
    {
        Debug.Log("Engine started");
    }
}

public class Car : Vechicle
{
   public void Drive()
    {
        Debug.Log("Car is driving");
    }
}
