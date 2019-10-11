using UnityEngine;

public class Timer {

    // Inputs
    private float currentTime = 0.0f;
    private float maximumTime = 0.0f;

    public Timer (float maximumTime) {
        this.maximumTime = maximumTime;
        this.currentTime = 0.0f;
    }

    public void Increment() {
        currentTime += Time.deltaTime;
    }

    public void Reset() {
        currentTime = 0.0f;
    }

    public bool HasFinished() {
        if (currentTime >= maximumTime) {
            return true;
        }

        return false;
    }
}
