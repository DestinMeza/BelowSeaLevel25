using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;

using Random = UnityEngine.Random;

using static BelowSeaLevel_25.Globals.Enums;
using UnityEngine.Rendering;

namespace BelowSeaLevel_25
{
    internal class CameraManager : MonoManager<CameraManager>
    {
        public Vector3 m_LastCameraPosition;
        public Vector3 m_StartingCameraPosition;

        private Coroutine m_ActiveCameraShake;

        public override void Init()
        {
            base.Init();
            m_StartingCameraPosition = Camera.main.transform.position;
        }

        public static void ShakeCamera(float cameraShakeDuration, float cameraShakeMagnitude = 2)
        {
            Instance.PlayCameraShake(cameraShakeDuration, cameraShakeMagnitude);
        }

        private void PlayCameraShake(float cameraShakeDuration, float cameraShakeMagnitude = 2)
        {
            if (m_ActiveCameraShake != null)
            {
                StopCoroutine(m_ActiveCameraShake);
                Camera.main.transform.position = m_StartingCameraPosition;
            }

            m_ActiveCameraShake = StartCoroutine(PlayerCameraShakeEffect(cameraShakeDuration, cameraShakeMagnitude));
        }

        private IEnumerator PlayerCameraShakeEffect(float cameraShakeDuration, float cameraShakeMagnitude = 2)
        {
            m_LastCameraPosition = Camera.main.transform.position;
            float randomNumber = Random.Range(0.0f, 10000.0f);
            float cameraShakeX = Mathf.PerlinNoise1D(randomNumber + Time.time) + 1.0f * cameraShakeMagnitude - 1.0f;
            randomNumber = Random.Range(0.0f, 10000.0f);
            float cameraShakeY = Mathf.PerlinNoise1D(randomNumber + Time.time) + 1.0f * cameraShakeMagnitude - 1.0f;

            Vector3 cameraShakePosition = new Vector3(cameraShakeX, cameraShakeY, m_LastCameraPosition.z);

            for (float t = 0; t <= cameraShakeDuration * 0.33f; t += Time.deltaTime)
            {
                Camera.main.transform.position = Vector3.Lerp(cameraShakePosition, m_LastCameraPosition, t / (cameraShakeDuration * 0.33f));
                yield return new WaitForEndOfFrame();
            }

            cameraShakeX = Mathf.PerlinNoise1D(randomNumber + Time.time) + 1.0f * cameraShakeMagnitude - 1.0f;
            cameraShakeY = Mathf.PerlinNoise1D(randomNumber + Time.time) + 1.0f * cameraShakeMagnitude - 1.0f;

            cameraShakePosition = new Vector3(cameraShakeX, cameraShakeY, m_LastCameraPosition.z);

            for (float t = 0; t <= cameraShakeDuration * 0.33f; t += Time.deltaTime)
            {
                Camera.main.transform.position = Vector3.Lerp(cameraShakePosition, m_LastCameraPosition, t / (cameraShakeDuration * 0.33f));
                yield return new WaitForEndOfFrame();
            }

            cameraShakeX = Mathf.PerlinNoise1D(randomNumber + Time.time) + 1.0f * cameraShakeMagnitude - 1.0f;
            cameraShakeY = Mathf.PerlinNoise1D(randomNumber + Time.time) + 1.0f * cameraShakeMagnitude - 1.0f;

            cameraShakePosition = new Vector3(cameraShakeX, cameraShakeY, m_LastCameraPosition.z);

            for (float t = 0; t <= cameraShakeDuration * 0.33f; t += Time.deltaTime)
            {
                Camera.main.transform.position = Vector3.Lerp(cameraShakePosition, m_LastCameraPosition, t / (cameraShakeDuration * 0.33f));
                yield return new WaitForEndOfFrame();
            }

            Camera.main.transform.position = m_StartingCameraPosition;
            yield return new WaitForEndOfFrame();

            m_ActiveCameraShake = null;
        }
        
        public override void OnDestroy()
        {
            base.OnDestroy();
        }
    }
}