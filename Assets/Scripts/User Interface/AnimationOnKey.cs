using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationOnKey : MonoBehaviour {
	[SerializeField]
	KeyCode m_key;

	Animator m_animator;

	bool m_activated = false;

	// Use this for initialization
	void Start ()
	{
		m_animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Input.GetKeyDown(m_key) && !m_activated)
		{
			m_activated = true;
			m_animator.SetTrigger("Disappear");
		}
	}
}
