# Player Class Demo

This is a demo I gave during our second meeting on some useful tools for when you're instantiating an ASLObject for each client.

    public class Player : MonoBehaviour
    {
        private static GameObject _playerObject = null;
        private static ASLObject _playerAslObject = null;

        private static readonly float UPDATES_PER_SECOND = 2.0f;

        // Instantiates a player for each client
        void Start()
        {
            ASLHelper.InstantiateASLObject(
                PrimitiveType.Cube,
                new Vector3(0,0,0),
                Quaternion.identity,
                null,
                null,
                OnPlayerCreated);


            StartCoroutine(DelayedInit());
            StartCoroutine(NetworkedUpdate());
        }

        private static void OnPlayerCreated(GameObject obj)
        {
            _playerObject = obj;
            _playerAslObject = obj.GetComponent<ASLObject>();
        }

        // Ensures that the ASLObject is initialized
        // You can also do this in the callback if you prefer, but that has to be static.
        IEnumerator DelayedInit()
        {
            while (_playerObject == null)
            {
                yield return new WaitForSeconds(0.1f);
            }

            _playerAslObject.SendAndSetClaim(() =>
            {
                _playerAslObject.SendAndSetObjectColor(
                    new Color(0.0f, 0.0f, 0.0f, 0.0f),
                    new Color(0.2f, 0.4f, 0.2f));
            });

            _playerObject.SetActive(false);

        }

        // Putting your update in a coroutine allows you to run it at a rate of your choice
        IEnumerator NetworkedUpdate()
        {
            while (true)
            {
                if(_playerObject == null)
                    yield return new WaitForSeconds(0.1f);

                _playerAslObject.SendAndSetClaim(() =>
                {
                    _playerAslObject.SendAndSetWorldPosition(transform.position);
                });

                yield return new WaitForSeconds(1 / UPDATES_PER_SECOND);
            }
        }
    }
