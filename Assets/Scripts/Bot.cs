using UnityEngine;

public class Bot : MonoBehaviour
{
    public float detectionDistance = 5f;      // Дистанция обнаружения препятствий
    public float avoidSpeed = 3f;             // Скорость смещения влево-вправо
    public float forwardSpeed = 5f;           // Скорость движения вперед
    public float maxAvoidDistance = 5f;       // Максимальное смещение от центра дороги
    //public float centerReturnSpeed = 2f;      // Скорость возвращения на середину дороги
    public LayerMask obstacleLayer;           // Маска для обнаружения препятствий

    private bool avoiding = false;
    private float targetPositionX;
    //private bool returningToCenter = false;
    private string _currentAvodingObstacle;

    void Update()
    {
        MoveForward();

        // Игнорируем слой машины при проверке Raycast и проверяем каждый кадр
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, detectionDistance, obstacleLayer);

        if (hit.collider == null)
        { return; }

        if (hit.collider.tag == "Block" || hit.collider.tag == "ObstacleOil" || hit.collider.tag == "RoadCrack")
        {
            Debug.Log(hit.collider.tag);
            Debug.Log("transform.position.x" + transform.position.x);
            Debug.Log("hit.collider.transform.localposition.x_" + hit.collider.transform.localPosition.x);

            if (!avoiding || _currentAvodingObstacle != hit.collider.tag) // && !returningToCenter)
            {
                // Смещаемся влево или вправо в зависимости от положения препятствия
                if (hit.collider.transform.localPosition.x < transform.position.x)
                {
                    targetPositionX = transform.position.x + maxAvoidDistance;
                }
                else
                {
                    targetPositionX = transform.position.x - maxAvoidDistance;
                }

                avoiding = true;
                _currentAvodingObstacle = hit.collider.tag;
            }
        }

        // Если смещаемся для объезда препятствия
        if (avoiding)
        {
            Vector2 target = new Vector2(targetPositionX, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, target, avoidSpeed * Time.deltaTime);

            // Когда достигли цели объезда
            if (Vector2.Distance(transform.position, target) < 0.1f)
            {
                avoiding = false;
                //returningToCenter = true;  // Готовы вернуться на середину
            }
        }

       /* // Возвращаемся на середину дороги
        if (returningToCenter && !avoiding)
        {
            Vector2 centerPosition = new Vector2(0, transform.position.y);  // Центр дороги (по оси X = 0)
            transform.position = Vector2.MoveTowards(transform.position, centerPosition, centerReturnSpeed * Time.deltaTime);

            // Когда достигли середины дороги
            if (Vector2.Distance(transform.position, centerPosition) < 0.1f)
            {
                returningToCenter = false;  // Закончили возвращение на центр
            }
        }*/
    }

    void MoveForward()
    {
        transform.Translate(Vector2.up * forwardSpeed * Time.deltaTime);
    }
}