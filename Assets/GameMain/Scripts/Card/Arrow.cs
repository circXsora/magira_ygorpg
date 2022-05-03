using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace BBYGO
{

    public class Arrow : MonoBehaviour
    {
        public Vector2? OriginAnchoredPosition;
        public Vector2? OriginWorldPosition;

        public Transform BodyOrigin;
        public Transform BodyRoot;
        public RectTransform CtrlAView, CtrlBView;
        private Transform[] bodies;
        [SerializeField]
        private Vector2 ctrlPointARatio = new Vector2(0.2f, 0.2f);

        [SerializeField]
        private Vector2 ctrlPointBRatio = new Vector2(0.2f, 0.2f);

        [SerializeField]
        private int bodyCount = 100;

        private Image image;

        private Camera cam;

        private void Awake()
        {
            image = GetComponent<Image>();
            cam = Camera.main;
        }

        private IEnumerator Start()
        {
            yield return new WaitForEndOfFrame();
            bodies = new Transform[bodyCount];
            for (int i = 0; i < bodyCount; i++)
            {
                bodies[i] = Instantiate(BodyOrigin, BodyRoot);
            }
        }

        private void Arrow_OnGetInput(Vector2 mousePos)
        {
            if (OriginAnchoredPosition == null)
            {
                return;
            }

            var canvas = GameEntry.Card.Canvas;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, mousePos, GameEntry.MainCamera, out Vector2 pos);

            transform.localPosition = pos;
            image.enabled = true;

            var screenPos = canvas.worldCamera.WorldToScreenPoint(OriginWorldPosition.Value);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), screenPos, cam, out Vector2 inCanvasPosition);

            Vector2 startPos = inCanvasPosition;
            Vector2 endPos = pos;


            Vector2 ctrlAPos = startPos + (endPos - startPos) * ctrlPointARatio;
            Vector2 ctrlBPos = startPos + (endPos - startPos) * ctrlPointBRatio;

            CtrlAView.anchoredPosition = ctrlAPos;
            CtrlBView.anchoredPosition = ctrlBPos;

            for (int index = 0; index < bodyCount; index++)
            {
                float t = Mathf.Log((float)index / (bodyCount - 1) + 1f, 2f);

                var position = GetBesizerPosByT(t);

                bodies[index].localScale = Vector3.one + t * (BodyOrigin.transform.localScale - Vector3.one);
                bodies[index].transform.localPosition = position;

                if (index > 0)
                {
                    Vector3 dirVec = -(bodies[index].transform.localPosition - bodies[index - 1].transform.localPosition).normalized;
                    bodies[index].transform.up = dirVec;
                }
                bodies[index].gameObject.SetActive(true);
            }
            transform.localPosition = GetBesizerPosByT(Mathf.Log((float)bodyCount / (bodyCount - 1) + 1f, 2f));
            var arrowDir = -(transform.localPosition - bodies[bodyCount - 1].transform.localPosition).normalized;
            transform.up = arrowDir;

            Vector3 GetBesizerPosByT(float t)
            {
                return startPos * (1 - t) * (1 - t) * (1 - t)
              + 3 * ctrlAPos * t * (1 - t) * (1 - t)
              + 3 * ctrlBPos * t * t * (1 - t)
              + endPos * t * t * t;
            }
        }

        private void Arrow_OnGetInputRelease(Vector2 mousePos)
        {
            for (int i = 0; i < bodyCount; i++)
            {
                bodies[i]?.gameObject.SetActive(false);
            }
            image.enabled = false;
            OriginAnchoredPosition = null;
        }

        private void OnEnable()
        {
            GameEntry.Input.OnGetMouseButton += Arrow_OnGetInput;
            GameEntry.Input.OnGetMouseButtonUp += Arrow_OnGetInputRelease;
        }

        private void OnDisable()
        {
            GameEntry.Input.OnGetMouseButton -= Arrow_OnGetInput;
            GameEntry.Input.OnGetMouseButtonUp -= Arrow_OnGetInputRelease;
        }

    }
}
