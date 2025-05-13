using UnityEngine;

public class Hazard : MonoBehaviour
{

    private ParkingController offendingController;

    private void OnTriggerEnter(Collider other)
    {
        
        offendingController = other.GetComponent<ParkingController>();

    }

    private void OnTriggerStay(Collider other)
    {
        offendingController.IsOffCourse = true;
    }

    private void OnTriggerExit(Collider other)
    {
        offendingController.IsOffCourse = false;
        offendingController = null;
    }
}
