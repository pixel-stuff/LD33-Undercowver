/**
 * Invisibility/Obfuscation effect for sprites
 * @author Pierre-Marie Plans
 * @date 07/04/2016
 **/
using UnityEngine;
using System.Collections;

public class Obfuscation : MonoBehaviour {

    public Material _normalObfuscationMaterial;
    public Material _obfuscationMaterial;
    public float _delayAnimation;

    public bool _isObfuscated;

    private bool _isBusy;
    private bool _isAnimationStarted;
    private float _timer;
    private float _startAnimationTime;

    private enum Status { NORMAL, PREOBFUSCATED, OBFUSCATED, POSTOBFUSCATED, STOPOBFUSCATION };

    private Status _status;


    // Use this for initialization
    void Start () {
        _isObfuscated = false;
        _isBusy = false;
        _isAnimationStarted = false;
        _timer = 0.0f;
        _status = Status.NORMAL;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if(sr!= null) {
            if (!_isObfuscated)
                sr.material = _normalObfuscationMaterial;
            else
                sr.material = _obfuscationMaterial;
        }
	}

    public void obfuscate() {
        _status = Status.PREOBFUSCATED;
        _timer = 0.0f;
        _isAnimationStarted = true;
    }

    public void deobfuscate() {
        _status = Status.POSTOBFUSCATED;
        _timer = 0.0f;
        _isAnimationStarted = true;
    }
	
	// Update is called once per frame
	void Update () {
        _timer += Time.deltaTime;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null) {
            Debug.Log(_status);
            if (_isObfuscated && !_isBusy) { // si on est Obfuscated
                obfuscate();
            } else
            if (!_isObfuscated && !_isBusy) { // si on est plus Obfuscated
                deobfuscate();
            }
            switch (_status) {
                case Status.NORMAL:
                    _isBusy = false;
                break;
                case Status.PREOBFUSCATED:
                    _isBusy = true;
                    if (_isAnimationStarted) {
                        sr.material = _obfuscationMaterial;
                        _startAnimationTime = Time.time;
                        sr.material.SetFloat("_StartAnim", _startAnimationTime);
                        sr.material.SetFloat("_UseInternalTime", 0.0f);
                        sr.material.SetFloat("_FadeInKeepOut", 0.0f);
                        _isAnimationStarted = false;
                    }
                    sr.material.SetFloat("_ScriptTime", (Time.time - _startAnimationTime));

                    if (_timer > _delayAnimation) {
                        _status = Status.OBFUSCATED;
                        _timer = 0.0f;
                    }
                break;
                case Status.OBFUSCATED:
                    sr.material.SetFloat("_FadeInKeepOut", 1.0f);
                    _status = Status.NORMAL;
                break;
                case Status.POSTOBFUSCATED:
                    _isBusy = true;
                    if (_isAnimationStarted) {
                        _startAnimationTime = Time.time;
                        sr.material.SetFloat("_StartAnim", _startAnimationTime);
                        sr.material.SetFloat("_UseInternalTime", 0.0f);
                        sr.material.SetFloat("_FadeInKeepOut", 2.0f);
                        _isAnimationStarted = false;
                    }
                    sr.material.SetFloat("_ScriptTime", (Time.time - _startAnimationTime));

                    if (_timer > _delayAnimation) {
                        _status = Status.STOPOBFUSCATION;
                    }
                 break;
                case Status.STOPOBFUSCATION:
                    sr.material = _normalObfuscationMaterial;
                    _status = Status.NORMAL;
                break;
            }
        }
    }
}
