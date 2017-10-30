using UnityEngine;

public class Billboard : MonoBehaviour {

	void OnEnable()
	{
		
//		Vector3 cameraDirection = m_Camera.transform.position - transform.position;
//		transform.position += cameraDirection * 0.5f;
	}

	void Update()
	{
		Camera m_Camera = GameController.Instance.MainCamera;
		transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward,
			m_Camera.transform.rotation * Vector3.up);
//		transform.position -= transform.forward * 10f;
	}
}
