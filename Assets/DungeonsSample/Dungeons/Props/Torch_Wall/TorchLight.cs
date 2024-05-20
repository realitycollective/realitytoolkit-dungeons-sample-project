using UnityEngine;

namespace DungeonsSample.Dungeons
{
    public class TorchLight : MonoBehaviour
    {
        [SerializeField]
        private Light pointLight = null;

        [SerializeField]
        private float minimumIntensity = .5f;

        [SerializeField]
        private float maximumIntensity = 1f;

        [SerializeField]
        private float flickerSpeed = .1f;

        private float currentIntensity;
        private float targetIntensity;
        private float flickerTimer;

        private void Awake()
        {
            currentIntensity = pointLight.intensity;
            UpdateTargetIntensity();
        }

        private void Update()
        {
            flickerTimer += Time.deltaTime * flickerSpeed;
            pointLight.intensity = Mathf.Lerp(currentIntensity, targetIntensity, flickerTimer);

            if (flickerTimer >= 1.0f)
            {
                currentIntensity = targetIntensity;
                UpdateTargetIntensity();
                flickerTimer = 0f;
            }
        }

        private void UpdateTargetIntensity() => targetIntensity = Random.Range(minimumIntensity, maximumIntensity);
    }
}
