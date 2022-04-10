using Appccelerate.StateMachine;
using Appccelerate.StateMachine.Machine;
using Appccelerate.StateMachine.Machine.Reports;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace BBYGO
{

    public class Card : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public enum States
        {
            Idle,
            Hovering,
            Dragging,
            ToHand,
            ToTarget,
        }

        private enum Events
        {
            OnPointerEnter,
            OnBeginDrag,
            OnDrag,
            OnPointerExit,
            OnEndDrag,
            BackToHand,
        }
        private UnityDebugStateMachineReportGenerator<States, Events> report = new UnityDebugStateMachineReportGenerator<States, Events>();
        private PassiveStateMachine<States, Events> machine;

        public States CardState { get; private set; } = States.Idle;

        public Arrow Arrow;
        private Canvas canvas;
        public RectTransform RectTransform { get; private set; }

        #region Set Position
        private Stack<PositionSetInfo> positionSetInfos = new Stack<PositionSetInfo>();
        private MGO.LerpWork lastPositionLerpWork;
        public void SetAnchoredPositionTo(Vector3 anchoredPos, string setName = null, Action onMoveCompelete = null, float time = 0.5f)
        {
            if (lastPositionLerpWork != null && lastPositionLerpWork.WorkStatus == MGO.WorkStatus.Running)
            {
                lastPositionLerpWork.Stop();
            }
            lastPositionLerpWork = MGO.AnchoredPositionLerpWork.Create(RectTransform, anchoredPos, time);
            lastPositionLerpWork.Name = setName;
            lastPositionLerpWork.ContinueWith(_ => onMoveCompelete?.Invoke());
            GameEntry.Work.StartWork(lastPositionLerpWork);
            positionSetInfos.Push(new PositionSetInfo()
            {
                target = anchoredPos,
                name = setName,
                moveCompelete = onMoveCompelete
            });
        }

        public void SetWorldPositionTo(Vector3 worldPosition, string setName = null, Action onMoveCompelete = null, float time = 1f)
        {
            if (lastPositionLerpWork != null && lastPositionLerpWork.WorkStatus == MGO.WorkStatus.Running)
            {
                lastPositionLerpWork.Stop();
            }
            lastPositionLerpWork = MGO.WorldPositionLerpWork.Create(transform, worldPosition, time);
            lastPositionLerpWork.Name = setName;
            lastPositionLerpWork.ContinueWith(_ => onMoveCompelete?.Invoke());
            GameEntry.Work.StartWork(lastPositionLerpWork);
            positionSetInfos.Push(new PositionSetInfo()
            {
                target = worldPosition,
                name = setName,
                moveCompelete = onMoveCompelete
            });
        }


        private struct PositionSetInfo
        {
            public Vector2 target;
            public string name;
            public Action moveCompelete;
        }
        #endregion

        #region Set Scale
        private Stack<ScaleSetInfo> scaleSetInfos = new Stack<ScaleSetInfo>();
        private MGO.LocalScaleLerpWork lastScaleLerpWork;
        public void SetScaleTo(Vector3 scale, string setName = null, Action onScaleCompelete = null, float time = 0.1f)
        {
            if (lastScaleLerpWork != null && lastScaleLerpWork.WorkStatus == MGO.WorkStatus.Running)
            {
                lastScaleLerpWork.Stop();
            }
            Debug.Log(name + " Run Scale Work " + setName ?? "");
            lastScaleLerpWork = MGO.LocalScaleLerpWork.Create(RectTransform, scale, time);
            lastScaleLerpWork.Name = setName;
            lastScaleLerpWork.ContinueWith(_ => onScaleCompelete?.Invoke());
            GameEntry.Work.StartWork(lastScaleLerpWork);
            scaleSetInfos.Push(new ScaleSetInfo()
            {
                target = scale,
                name = setName,
                onScaleCompelete = onScaleCompelete
            });
        }

        private struct ScaleSetInfo
        {
            public Vector3 target;
            public string name;
            public Action onScaleCompelete;
        }
        #endregion

        public static float IMG_WIDTH = 300.0F;
        public static float IMG_HEIGHT = 420.0F;
        public static float IMG_WIDTH_S = 300.0F * 0.7F;
        public static float IMG_HEIGHT_S = 420.0F * 0.7F;

        private void Awake()
        {
            Arrow = FindObjectOfType<Arrow>();
            canvas = GetComponent<Canvas>();
            RectTransform = GetComponent<RectTransform>();
        }

        private void Start()
        {
            var builder = new StateMachineDefinitionBuilder<States, Events>();
            builder.In(States.Idle)
                .ExecuteOnEntry(() =>
                {
                    Debug.Log(name + " To Idle");
                    SetScaleTo(Vector3.one, setName: name + " To Idle");
                    canvas.sortingOrder = 0;
                })
                .On(Events.OnPointerEnter)
                .Goto(States.Hovering);

            builder.In(States.Hovering)
                .ExecuteOnEntry(() =>
                {
                    Debug.Log(name + " To Hovering");
                    SetScaleTo(Vector3.one * 1.5f, setName: name + " To Hovering");
                    canvas.sortingOrder = 1;
                });

            builder.In(States.Hovering)
                .On(Events.OnBeginDrag)
                .Goto(States.Dragging)
                .Execute(() =>
                {
                    Arrow.transform.position = transform.position;
                    Arrow.OriginAnchoredPosition = RectTransform.anchoredPosition;
                    Arrow.OriginWorldPosition = RectTransform.position;
                });

            builder.In(States.Hovering)
                .On(Events.OnPointerExit)
                .Goto(States.Idle);

            builder.In(States.Dragging)
                .On(Events.OnDrag)
                .Execute<PointerEventData>((eventData) =>
                {
                    RectTransformUtility.ScreenPointToWorldPointInRectangle(GameEntry.Card.Canvas.GetComponent<RectTransform>(), eventData.position, GameEntry.MainCamera, out var pos);
                    SetWorldPositionTo(pos, "Dragging Move", time: 0.05f);
                });

            builder.In(States.Dragging)
                .On(Events.OnEndDrag)
                .Goto(States.ToHand)
                .Execute(() =>
                {
                    SetAnchoredPositionTo(Arrow.OriginAnchoredPosition.Value, "Back To Hand", () => { machine.Fire(Events.BackToHand); Debug.Log("Fire Back To Hand"); }, 0.5f);
                });

            builder.In(States.ToHand)
                .On(Events.BackToHand)
                .Goto(States.Idle)
                .Execute(() => { Logger.Debug("DraggingToHand To Idle"); });

            builder
                .WithInitialState(States.Idle);

            machine = builder.Build().CreatePassiveStateMachine();
            machine.Start();
        }

        public void SetAngleTo(float v)
        {
            transform.localRotation = Quaternion.Euler(0, 0, v);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            machine.Fire(Events.OnBeginDrag, eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            machine.Fire(Events.OnDrag, eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            machine.Fire(Events.OnEndDrag, eventData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            machine.Fire(Events.OnPointerExit, eventData);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            machine.Fire(Events.OnPointerEnter, eventData);
        }
    }
}
