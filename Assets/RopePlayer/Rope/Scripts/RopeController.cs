using UnityEngine;

namespace Rope
{
    public class RopeController : MonoBehaviour
    {
        [SerializeField]
        GameObject fragmentPrefab;

        [SerializeField]
        GameObject ropeHead;

        [SerializeField]
        public GameObject ropeSource;

        [SerializeField]
        int fragmentCount = 80;

        [SerializeField]
        Vector3 interval = new Vector3(0f, 0f, 0.25f);

        [SerializeField]
        Vector3 respawnOffset = new Vector3(0f, 0f, 0.25f);


        GameObject cam;

        GameObject[] fragments;

        [SerializeField]
        public float activeFragmentCount;

        float[] xPositions;
        float[] yPositions;
        float[] zPositions;

        CatmullRomSpline splineX;
        CatmullRomSpline splineY;
        CatmullRomSpline splineZ;

        int splineFactor = 4;

        void Start()
        {
            cam = Camera.main.gameObject;
            activeFragmentCount = 5;//fragmentCount;// / 2;

            fragments = new GameObject[fragmentCount];

            //var position = Vector3.zero;
            var position = ropeHead.transform.position - respawnOffset;
            //var position = ropeSource.transform.position;
           

            for (var i = 0; i < fragmentCount; i++)
            {
                fragments[i] = Instantiate(fragmentPrefab, position, Quaternion.identity);
                fragments[i].transform.SetParent(transform);

                var joint = fragments[i].GetComponent<SpringJoint>();
                if (i > 0)
                {
                    joint.connectedBody = fragments[i - 1].GetComponent<Rigidbody>();
                }

                position += interval;

                if (i == 0)
                {
                    Destroy(fragments[i].GetComponent<SpringJoint>());
                    FixedJoint fd = fragments[i].AddComponent<FixedJoint>();
                    fd.connectedBody = ropeHead.GetComponent<Rigidbody>();
                    cam.GetComponent<PlayerCamera>().lookAt = ropeHead.transform;
                    //ropeHead.AddComponent<Stabbing>();
                }

                /*
                if (i == (fragmentCount - 1 ) )
                {
                    cam.GetComponent<PlayerCamera>().lookAt = fragments[i].transform;
                    fragments[i].AddComponent<Stabbing>();
                }*/
            }

            var lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.positionCount = (fragmentCount - 1) * splineFactor + 1;

            xPositions = new float[fragmentCount];
            yPositions = new float[fragmentCount];
            zPositions = new float[fragmentCount];

            splineX = new CatmullRomSpline(xPositions);
            splineY = new CatmullRomSpline(yPositions);
            splineZ = new CatmullRomSpline(zPositions);
        }

        
        void Update()
        {
            var vy = Input.GetAxisRaw("Vertical") * 20f * Time.deltaTime;
            activeFragmentCount = Mathf.Clamp(activeFragmentCount + vy, 0, fragmentCount);

            for (var i = 0; i < fragmentCount; i++)
            {
                if (i <= fragmentCount - activeFragmentCount)
                {

                    
                    /*Second
                    if (i == 0) fragments[i].GetComponent<SpringJoint>().connectedBody = ropeHead.GetComponent<Rigidbody>();
                    fragments[i].GetComponent<Rigidbody>().position = ropeHead.transform.position;
                    fragments[i].GetComponent<Rigidbody>().isKinematic = true;
                    */

                       /*
                        fragments[i].GetComponent<Rigidbody>().position = ropeSource.transform.position;
                        fragments[i].GetComponent<Rigidbody>().isKinematic = true;
                        */
                }
                else
                {
                    fragments[i].GetComponent<Rigidbody>().position = ropeSource.transform.position;
                    fragments[i].GetComponent<Rigidbody>().isKinematic = true;
                }
                //fragments[i].GetComponent<Rigidbody>().useGravity = false;
            }
        }

        void LateUpdate()
        {
            // Copy rigidbody positions to the line renderer
            var lineRenderer = GetComponent<LineRenderer>();

            // No interpolation
            //for (var i = 0; i < fragmentNum; i++)
            //{
            //    renderer.SetPosition(i, fragments[i].transform.position);
            //}

            for (var i = 0; i < fragmentCount; i++)
            {
                var position = fragments[i].transform.position;
                xPositions[i] = position.x;
                yPositions[i] = position.y;
                zPositions[i] = position.z;
            }

            for (var i = 0; i < (fragmentCount - 1) * splineFactor + 1; i++)
            {
                lineRenderer.SetPosition(i, new Vector3(
                    splineX.GetValue(i / (float) splineFactor),
                    splineY.GetValue(i / (float) splineFactor),
                    splineZ.GetValue(i / (float) splineFactor)));
            }
        }
    }
}
