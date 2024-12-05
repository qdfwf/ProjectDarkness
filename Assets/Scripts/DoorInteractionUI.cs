using UnityEngine;
using TMPro;

public class DoorInteractionUI : MonoBehaviour
{
    public TextMeshProUGUI interactionText; // ������ �� UI-�������
    public float activationDistance = 2f;  // ���������, �� ������� ���������� �����
    private Transform player;              // ������ �� ������

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        interactionText.gameObject.SetActive(false); // �������� ����� ��� ������
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        // ���� ����� � ���� ��������������, ���������� �����
        if (distance <= activationDistance)
        {
            interactionText.gameObject.SetActive(true);

            // �����������: ����� �������� �������� ������ �� �������
            
        }
        else
        {
            interactionText.gameObject.SetActive(false); // �������� �����
        }
    }
}
