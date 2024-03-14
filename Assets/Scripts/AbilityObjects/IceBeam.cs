using UnityEngine;

public class IceBeam : MonoBehaviour 
{
    [SerializeField] private GameObject iceCubePrefab;
	[SerializeField] private Transform startPoint;
	[SerializeField] private Transform endPoint;
	[SerializeField] private LineRenderer beamLine;
	private void Start () 
    {
		beamLine = GetComponentInChildren<LineRenderer>();
    }
    void Update () 
    {
		beamLine.SetPosition(0, startPoint.position);
		beamLine.SetPosition(1, endPoint.position);
	}
    private void OnTriggerEnter(Collider other)
    {
        Controller controller = other.GetComponent<Controller>();
        if (controller != null)
        {
            GameObject impact = Instantiate(iceCubePrefab, other.transform.position, Quaternion.identity);
            impact.GetComponent<IceCube>().Freeze(controller);
        }
    }
}
