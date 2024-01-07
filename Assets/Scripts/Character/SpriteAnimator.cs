using System.Collections.Generic;
using UnityEngine;


public class SpriteAnimator
{
    private SpriteRenderer _spriteRenderer;
    private List<Sprite> _frames;
    private float _frameRate;

    private int _currentFrame;
    private float _timer;

    public List<Sprite> Frames => _frames;

    public SpriteAnimator(List<Sprite> frames, SpriteRenderer spriteRenderer, float frameRate = 0.16f)
    {
        _frames = frames;
        _spriteRenderer = spriteRenderer;
        _frameRate = frameRate;
    }

    public void Start()
    {
        _currentFrame = 0;
        _timer = 0;

        _spriteRenderer.sprite = _frames[0];
    }

    public void HandleUpdate()
    {
        _timer += Time.deltaTime;
        if (_timer > _frameRate)
        {
            _currentFrame = (_currentFrame + 1) % _frames.Count;
            _spriteRenderer.sprite = _frames[_currentFrame];
            _timer -= _frameRate;
        }
    }
}