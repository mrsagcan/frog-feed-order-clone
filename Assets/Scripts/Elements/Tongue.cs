using System.Collections.Generic;
using UnityEngine;

public class Tongue : MonoBehaviour
{
    [HideInInspector] public bool isReleasing = false;
    [HideInInspector] public bool isRetracting = false;

    [SerializeField] private float releaseSpeed;
    [SerializeField] private float _maxLength;

    private LineRenderer _lineRenderer;
    private BoxCollider _boxCollider;
    private Vector3 _direction;
    private Vector3 _newPosition;
    private bool _shouldCollect;
    private Color _frogColor;
    private int _hitCount;

    private List<Vector3> _hitPoints;
    private List<ICollectable> _collectablesFound;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _boxCollider = GetComponent<BoxCollider>();
        _boxCollider.enabled = false;
        _hitPoints = new List<Vector3>();
        _collectablesFound = new List<ICollectable>();
    }

    private void Update()
    {
        //If the tongue is releasing advance with that.
        if(isReleasing)
        {
            Advance();
        }
        //If the tongue is retracting.
        if(isRetracting)
        {
            Retract();
        }
        //If the tongue should not collect anything continue on updating the position.
        if(!_shouldCollect)
        {
            UpdatePosition();
        }
    }

    private void CollectAll()
    {
        //Collect all the found elements.
        for(int i = 0; i < _collectablesFound.Count; i++)
        {
            List<Vector3> route = new List<Vector3> { transform.parent.position };
            route.AddRange(_hitPoints.GetRange(0, i + 1));
            _collectablesFound[i].Collect(route);
        }
    }

    //Checks if all the items are collected.
    private bool HasCollected()
    {
        if (_collectablesFound.Count == 0) return false;

        foreach(var c in _collectablesFound)
        {
            if(!c.HasCollected)
            {
                return false;
            }
        }
        return true;

    }

    private void OnTriggerEnter(Collider other)
    {
        //If it is the same object or the other is not active then return.
        if (transform.parent == other.transform || !other.gameObject.activeInHierarchy)
        {
            return;
        }
        //If we hit a berry or an arrow, update the line renderer.
        if (HandleBerryHit(other) || HandleArrowHit(other))
        {
            UpdateLineRenderer();
        }
        //Means we reached an obstacle (edge or different colored element).
        else
        {
            Actions.OnTongueReachedObstacle();
            //If it is an edge we should collect.
            if (other.CompareTag("Edge"))
            {
                _shouldCollect = true;
            }
            //If it isn't, then its an element we shouldn't collect. Clear all the found objects.
            else
            {
                _collectablesFound.Clear();
            }
            isReleasing = false;
            isRetracting = true;
        }
        //Collect them all.
        if (_shouldCollect) CollectAll();
    }

    private bool HandleArrowHit(Collider other)
    {
        Arrow hitArrow = other.GetComponentInChildren<Arrow>();

        //If what we hit is an arrow, it is active and its color matches ours'
        if (hitArrow != null && hitArrow.gameObject.activeInHierarchy && hitArrow.color == _frogColor)
        {
            _collectablesFound.Add(hitArrow);
            _hitPoints.Add(hitArrow.transform.position + Vector3.up * 0.2f);
            _hitCount++;
            SetDirection(hitArrow.pose);
            return true;
        }
        return false;
    }

    private bool HandleBerryHit(Collider other)
    {
        Berry hitBerry = other.GetComponentInChildren<Berry>();

        //If what we hit is a berry, it is active and its color matches ours'
        if(hitBerry != null && hitBerry.gameObject.activeInHierarchy && hitBerry.color == _frogColor) 
        {
            _collectablesFound.Add(hitBerry);
            hitBerry.Found();
            _hitPoints.Add(hitBerry.transform.position + Vector3.up * 0.2f);
            _hitCount++;
            Actions.OnBerryFound();
            return true;
        }
        return false;
    }

    //Update the line at each encounter.
    private void UpdateLineRenderer()
    {
        _lineRenderer.positionCount++;
        _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, _hitPoints[_hitCount - 1]);
    }

    public void Release()
    {
        Initialize();
    }

    public void SetDirection(Direction pose)
    {
        switch(pose)
        {
            case Direction.Left:
                _direction = Vector3.left;
                break;
            case Direction.Right:
                _direction = Vector3.right;
                break;
            case Direction.Up:
                _direction = Vector3.forward;
                break;
            case Direction.Down:
                _direction = Vector3.back;
                break;
        }
    }

    public void SetColor(Color color)
    {
        _frogColor = color;
    }

    private void Initialize()
    {
        _boxCollider.enabled = true;
        _shouldCollect = false;
        transform.position = transform.parent.position + Vector3.up * 0.2f;
        _lineRenderer.enabled = true;
        _lineRenderer.positionCount = 2;
        _lineRenderer.SetPositions(new Vector3[] { transform.position, transform.position });
        _newPosition = transform.position + _direction * releaseSpeed * Time.deltaTime;
        isReleasing = true;
    }

    //Advance with rendering the line with the new position.
    private void Advance()
    {
        _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, _newPosition);
    }
    private void Retract()
    {
        _boxCollider.enabled = false;
        //If it shouldn't collect but retracting, that means we hit an obstacle. Disable renderer and stop retracting.
        if(!_shouldCollect )
        {
            _lineRenderer.enabled = false;
            isRetracting = false;
            _hitPoints.Clear();
        }
        //If it did collect everything during retraction, deactivate it. 
        if(HasCollected())
        {
            _collectablesFound.Clear();
            _hitPoints.Clear();
            _lineRenderer.enabled = false;
            isRetracting = false;
            transform.parent.gameObject.SetActive(false);
            Actions.OnTongueCollect();
        }
    }

    //Update the position at each frame with the given speed.
    private void UpdatePosition()
    {
        transform.position = _lineRenderer.GetPosition(_lineRenderer.positionCount - 1);
        _newPosition = transform.position + _direction * releaseSpeed * Time.deltaTime;
    }
}
