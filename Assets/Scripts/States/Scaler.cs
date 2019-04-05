using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaler : State<InteractionStates>
{
    public override InteractionStates Id => InteractionStates.Scaler;
        
    [SerializeField]
    private GameObject _rayCaster;

    [SerializeField] private Material _highlightMaterial;

    [SerializeField]
    private Renderer _pointer;

    private Vector3 _forward;

    private float _minSize = 0.1f;
    private float _maxSize = 2f;
    private float _value;
    private float _range;
    private float _amount = 0f;

    private int _layerMask;
    private GameObject _currentTarget;
    private Dictionary<Transform, Material> _originalMaterials = new Dictionary<Transform, Material>();

    private Dictionary<GameObject, Vector3> _localScales = new Dictionary<GameObject, Vector3>();

    private bool isHighlighting = true;


    public override void Enter()
    {
        _layerMask = ~LayerMask.GetMask("Unscalable");
        _forward = _rayCaster.transform.TransformDirection(Vector3.forward);
        isHighlighting = true;
        EnablePointer();
    }

    public override void Leave()
    {
        UnFocusTarget(_currentTarget);
        isHighlighting = false;
        DisablePointer();
    }

    public override void StartApply()
    {
        isHighlighting = false;
        Debug.Log("startApply");
        DisablePointer();
    }

    public override void EndApply()
    {
        isHighlighting = true;
        Debug.Log("endApply");
        EnablePointer();
    }

    void Update()
    {
        Highlight();        
    }

    private void EnablePointer()
    {
        _pointer.enabled = true;
    }

    private void DisablePointer()
    {
        _pointer.enabled = false;
    }

    private void Highlight() {
        if (!isHighlighting) {
            return;
        }
        Debug.DrawRay(_rayCaster.transform.position, _rayCaster.transform.forward * 10, Color.green);

        Vector3 start = _rayCaster.transform.position;
        Vector3 direction = _rayCaster.transform.forward;
        RaycastHit hit;
        if (Physics.Raycast(start, direction, out hit))
        {
            SetTarget(hit.transform.gameObject);
        }
        else
        {
            SetTarget(null);
        }
    }

    private void SetTarget(GameObject newTarget)
    {
        if (newTarget == _currentTarget)
        {
            return;
        }

        UnFocusTarget(_currentTarget);
        _currentTarget = newTarget;
        StoreLocalScale(_currentTarget);
        FocusTarget(_currentTarget);
    }

    private void StoreLocalScale(GameObject target)
    {
        if (target == null || _localScales.ContainsKey(target))
        {
            return;
        }

        _localScales.Add(target, target.transform.localScale);
    }

    private void UnFocusTarget(GameObject target)
    {
        if (target == null)
        {
            return;
        }

        
        foreach (Transform child in target.transform)
        {
            Renderer renderer = child.GetComponent<Renderer>();
            if (renderer == null)
            {
                continue;
            }
            
            renderer.material = _originalMaterials[child];
        }
    }

    private void FocusTarget(GameObject target)
    {
        if (target == null)
        {
            return;
        }


        foreach (Transform child in target.transform)
        {
            Renderer renderer = child.GetComponent<Renderer>();
            if (renderer == null)
            {
                continue;
            }
            _originalMaterials[child] = renderer.material;
            renderer.material = _highlightMaterial;
        }
        
    }

    public override void Apply(float amount)
    {
        if (_currentTarget == null || isHighlighting)
        {
            return;
        }
        Vector3 originalScale = _localScales[_currentTarget];
        float percentage = (_minSize + (_maxSize - _minSize) * amount);
        Vector3 newScale = originalScale * percentage;
        SetScale(newScale);
    }

    void SetScale(Vector3 newScale)
    {
        _currentTarget.transform.localScale = newScale;
    }
}