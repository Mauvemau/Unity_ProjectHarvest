using UnityEngine;

public interface ITimer {
    public float CurrentTime { get; }
    public bool IsRunning { get; }
    public void SetTimeScale(float scale);
    public void Stop();
    public void Resume();
    public void Reset();
    public void Start();
    public void Update(float deltaTime);
}
