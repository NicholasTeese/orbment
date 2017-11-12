using UnityEngine;
using UnityEngine.UI;

public class ImagePulse : MonoBehaviour
{
    private enum PulseState
    {
        DARKENING,
        LIGHTENING,
    }

    private float m_fPulseSpeed = 0.01f;

    private PulseState m_ePulseState = PulseState.DARKENING;

    public Image m_image;

    private void Awake()
    {
        if (m_image == null)
        {
            m_image = GetComponent<Image>();
        }

        if (m_image.type != Image.Type.Filled)
        {
            Debug.Log(m_image.gameObject.name + " image must be of image type 'Filled'.");
        }
    }

    private void Update()
    {
        m_fPulseSpeed = (1.0f - m_image.fillAmount) / 10.0f;

        if (m_image.fillAmount <= 0.4f)
        {
            Pulse(m_image, m_fPulseSpeed, ref m_ePulseState);
        }
        else
        {
            Reset(m_image);
        }
    }

    private void Pulse(Image a_image, float a_fPulseSpeed, ref PulseState a_ePulseState)
    {
        if (m_image.type != Image.Type.Filled)
        {
            return;
        }

        Color color = m_image.color;

        switch (a_ePulseState)
        {
            case PulseState.DARKENING:
                {
                    if (color.r >= 0.2f || color.g >= 0.2f || color.b >= 0.2f)
                    {
                        color.r -= a_fPulseSpeed;
                        color.g -= a_fPulseSpeed;
                        color.b -= a_fPulseSpeed;
                    }
                    else
                    {
                        a_ePulseState = PulseState.LIGHTENING;
                    }
                    break;
                }
            case PulseState.LIGHTENING:
                {
                    if (color.r <= 0.8f || color.g <= 0.8f || color.b <= 0.8f)
                    {
                        color.r += a_fPulseSpeed;
                        color.g += a_fPulseSpeed;
                        color.b += a_fPulseSpeed;
                    }
                    else
                    {
                        a_ePulseState = PulseState.DARKENING;
                    }
                    break;
                }
            default:
                {
                    Debug.Log(gameObject.name + "'s PulseState value not recognised.");
                    break;
                }
        }

        a_image.color = color;
    }

    private void Reset(Image a_image)
    {
        Color resetColor = Color.white;
        a_image.color = resetColor;
    }
}
