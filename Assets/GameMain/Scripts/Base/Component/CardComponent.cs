//------------------------------------------------------------------------------
//  <copyright file="CardComponent.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2022/4/9 18:07:44
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using MGO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BBYGO
{
    public class CardComponent : GameComponent
    {
        public CanvasScaler CanvasScaler;
        public Canvas Canvas;

        public Transform CardOrigin;

        [SerializeField]
        private Transform cardParent;
        public Transform CardParent { get => cardParent; }

        /// <summary>
        /// 起始手牌数量
        /// </summary>
        [SerializeField] private int startHandCardCount;

        /// <summary>
        /// 总卡组数量
        /// </summary>
        [SerializeField] private int totalCardCount;

        public float CardXSpace = 10, CardYSpace = 10;
        public float CardEular = 10;
        private List<Card> handCards;


        private void Start()
        {
            CardSettings.initialize(false);
        }

        public void CreateCards(int count)
        {
            handCards = new List<Card>();
            for (int i = 0; i < count; i++)
            {
                var cardTransform = Instantiate(CardOrigin, CardParent);
                cardTransform.name = "Card" + i;
                handCards.Add(cardTransform.GetComponent<Card>());
                //handCards[i].TargetLocalPosition = CalTargetPos(i, startHandCardCount);
                //cards[i].transform.rotation = CalTargetQua(i, startHandCardCount);
                handCards[i].gameObject.SetActive(true);
            }
            RefreshHandlLayout();
        }

        public void RefreshHandlLayout()
        {
            float angleRange = 50.0F - (10 - handCards.Count) * 5.0F;
            float incrementAngle = angleRange / handCards.Count;
            float sinkStart = 80.0F * CardSettings.scale;
            float sinkRange = 300.0F * CardSettings.scale;
            float incrementSink = sinkRange / handCards.Count / 2.0F;
            int middle = handCards.Count / 2;
            var handCardXTargets = new float?[handCards.Count];
            var handCardYTargets = new float?[handCards.Count];
            for (int i = 0; i < handCards.Count; i++)
            {
                handCards[i].SetAngleTo(angleRange / 2.0F - incrementAngle * i - incrementAngle / 2.0F);

                int t = i - middle;
                if (t >= 0)
                {
                    if (handCards.Count % 2 == 0)
                    {
                        t++;
                        t = -t;
                    }
                    else
                    {
                        t = -t;
                    }
                }
                if (handCards.Count % 2 == 0)
                {
                    t++;
                }
                t = (int)(t * 1.7F);
                handCardYTargets[i] = (sinkStart + incrementSink * t);
            }
            foreach (var theCard in handCards)
            {
                theCard.SetScaleTo(new Vector3(0.75F, 0.75F, 0.75F));
            }
            switch (handCards.Count)
            {
                case 0:
                    return;
                case 1:
                    handCardXTargets[0] = CardSettings.WIDTH / 2.0F;
                    break;
                case 2:
                    handCardXTargets[0] = (CardSettings.WIDTH / 2.0F - Card.IMG_WIDTH_S * 0.47F);
                    handCardXTargets[1] = (CardSettings.WIDTH / 2.0F + Card.IMG_WIDTH_S * 0.53F);
                    break;
                case 3:
                    handCardXTargets[0] = (CardSettings.WIDTH / 2.0F - Card.IMG_WIDTH_S * 0.9F);
                    handCardXTargets[1] = (CardSettings.WIDTH / 2.0F);
                    handCardXTargets[2] = (CardSettings.WIDTH / 2.0F + Card.IMG_WIDTH_S * 0.9F);
                    handCardYTargets[0] += 20.0F * CardSettings.scale;
                    handCardYTargets[2] += 20.0F * CardSettings.scale;
                    break;
                case 4:
                    handCardXTargets[0] = (CardSettings.WIDTH / 2.0F - Card.IMG_WIDTH_S * 1.36F);
                    handCardXTargets[1] = (CardSettings.WIDTH / 2.0F - Card.IMG_WIDTH_S * 0.46F);
                    handCardXTargets[2] = (CardSettings.WIDTH / 2.0F + Card.IMG_WIDTH_S * 0.46F);
                    handCardXTargets[3] = (CardSettings.WIDTH / 2.0F + Card.IMG_WIDTH_S * 1.36F);
                    handCardYTargets[1] -= 10.0F * CardSettings.scale;
                    handCardYTargets[2] -= 10.0F * CardSettings.scale;
                    break;
                case 5:
                    handCardXTargets[0] = (CardSettings.WIDTH / 2.0F - Card.IMG_WIDTH_S * 1.7F);
                    handCardXTargets[1] = (CardSettings.WIDTH / 2.0F - Card.IMG_WIDTH_S * 0.9F);
                    handCardXTargets[2] = (CardSettings.WIDTH / 2.0F);
                    handCardXTargets[3] = (CardSettings.WIDTH / 2.0F + Card.IMG_WIDTH_S * 0.9F);
                    handCardXTargets[4] = (CardSettings.WIDTH / 2.0F + Card.IMG_WIDTH_S * 1.7F);
                    handCardYTargets[0] += 25.0F * CardSettings.scale;
                    handCardYTargets[2] -= 10.0F * CardSettings.scale;
                    handCardYTargets[4] += 25.0F * CardSettings.scale;
                    break;
                case 6:
                    handCardXTargets[0] = (CardSettings.WIDTH / 2.0F - Card.IMG_WIDTH_S * 2.1F);
                    handCardXTargets[1] = (CardSettings.WIDTH / 2.0F - Card.IMG_WIDTH_S * 1.3F);
                    handCardXTargets[2] = (CardSettings.WIDTH / 2.0F - Card.IMG_WIDTH_S * 0.43F);
                    handCardXTargets[3] = (CardSettings.WIDTH / 2.0F + Card.IMG_WIDTH_S * 0.43F);
                    handCardXTargets[4] = (CardSettings.WIDTH / 2.0F + Card.IMG_WIDTH_S * 1.3F);
                    handCardXTargets[5] = (CardSettings.WIDTH / 2.0F + Card.IMG_WIDTH_S * 2.1F);
                    handCardYTargets[0] += 10.0F * CardSettings.scale;
                    handCardYTargets[5] += 10.0F * CardSettings.scale;
                    break;
                case 7:
                    handCardXTargets[0] = (CardSettings.WIDTH / 2.0F - Card.IMG_WIDTH_S * 2.4F);
                    handCardXTargets[1] = (CardSettings.WIDTH / 2.0F - Card.IMG_WIDTH_S * 1.7F);
                    handCardXTargets[2] = (CardSettings.WIDTH / 2.0F - Card.IMG_WIDTH_S * 0.9F);
                    handCardXTargets[3] = (CardSettings.WIDTH / 2.0F);
                    handCardXTargets[4] = (CardSettings.WIDTH / 2.0F + Card.IMG_WIDTH_S * 0.9F);
                    handCardXTargets[5] = (CardSettings.WIDTH / 2.0F + Card.IMG_WIDTH_S * 1.7F);
                    handCardXTargets[6] = (CardSettings.WIDTH / 2.0F + Card.IMG_WIDTH_S * 2.4F);
                    handCardYTargets[0] += 25.0F * CardSettings.scale;
                    handCardYTargets[1] += 18.0F * CardSettings.scale;
                    handCardYTargets[3] -= 6.0F * CardSettings.scale;
                    handCardYTargets[5] += 18.0F * CardSettings.scale;
                    handCardYTargets[6] += 25.0F * CardSettings.scale;
                    break;
                case 8:
                    handCardXTargets[0] = (CardSettings.WIDTH / 2.0F - Card.IMG_WIDTH_S * 2.5F);
                    handCardXTargets[1] = (CardSettings.WIDTH / 2.0F - Card.IMG_WIDTH_S * 1.82F);
                    handCardXTargets[2] = (CardSettings.WIDTH / 2.0F - Card.IMG_WIDTH_S * 1.1F);
                    handCardXTargets[3] = (CardSettings.WIDTH / 2.0F - Card.IMG_WIDTH_S * 0.38F);
                    handCardXTargets[4] = (CardSettings.WIDTH / 2.0F + Card.IMG_WIDTH_S * 0.38F);
                    handCardXTargets[5] = (CardSettings.WIDTH / 2.0F + Card.IMG_WIDTH_S * 1.1F);
                    handCardXTargets[6] = (CardSettings.WIDTH / 2.0F + Card.IMG_WIDTH_S * 1.77F);
                    handCardXTargets[7] = (CardSettings.WIDTH / 2.0F + Card.IMG_WIDTH_S * 2.5F);
                    handCardYTargets[1] += 10.0F * CardSettings.scale;
                    handCardYTargets[6] += 10.0F * CardSettings.scale;
                    foreach (var theCard in handCards)
                    {
                        theCard.SetScaleTo(new Vector3(0.7125F, 0.7125F, 0.7125F));
                    }
                    break;
                case 9:
                    handCardXTargets[0] = (CardSettings.WIDTH / 2.0F - Card.IMG_WIDTH_S * 2.8F);
                    handCardXTargets[1] = (CardSettings.WIDTH / 2.0F - Card.IMG_WIDTH_S * 2.2F);
                    handCardXTargets[2] = (CardSettings.WIDTH / 2.0F - Card.IMG_WIDTH_S * 1.53F);
                    handCardXTargets[3] = (CardSettings.WIDTH / 2.0F - Card.IMG_WIDTH_S * 0.8F);
                    handCardXTargets[4] = (CardSettings.WIDTH / 2.0F + Card.IMG_WIDTH_S * 0.0F);
                    handCardXTargets[5] = (CardSettings.WIDTH / 2.0F + Card.IMG_WIDTH_S * 0.8F);
                    handCardXTargets[6] = (CardSettings.WIDTH / 2.0F + Card.IMG_WIDTH_S * 1.53F);
                    handCardXTargets[7] = (CardSettings.WIDTH / 2.0F + Card.IMG_WIDTH_S * 2.2F);
                    handCardXTargets[8] = (CardSettings.WIDTH / 2.0F + Card.IMG_WIDTH_S * 2.8F);
                    handCardYTargets[1] += 22.0F * CardSettings.scale;
                    handCardYTargets[2] += 18.0F * CardSettings.scale;
                    handCardYTargets[3] += 12.0F * CardSettings.scale;
                    handCardYTargets[5] += 12.0F * CardSettings.scale;
                    handCardYTargets[6] += 18.0F * CardSettings.scale;
                    handCardYTargets[7] += 22.0F * CardSettings.scale;
                    foreach (var theCard in handCards)
                    {
                        theCard.SetScaleTo(new Vector3(0.67499995F, 0.67499995F, 0.67499995F));
                    }
                    break;
                case 10:
                    handCardXTargets[0] = (CardSettings.WIDTH / 2.0F - Card.IMG_WIDTH_S * 2.9F);
                    handCardXTargets[1] = (CardSettings.WIDTH / 2.0F - Card.IMG_WIDTH_S * 2.4F);
                    handCardXTargets[2] = (CardSettings.WIDTH / 2.0F - Card.IMG_WIDTH_S * 1.8F);
                    handCardXTargets[3] = (CardSettings.WIDTH / 2.0F - Card.IMG_WIDTH_S * 1.1F);
                    handCardXTargets[4] = (CardSettings.WIDTH / 2.0F - Card.IMG_WIDTH_S * 0.4F);
                    handCardXTargets[5] = (CardSettings.WIDTH / 2.0F + Card.IMG_WIDTH_S * 0.4F);
                    handCardXTargets[6] = (CardSettings.WIDTH / 2.0F + Card.IMG_WIDTH_S * 1.1F);
                    handCardXTargets[7] = (CardSettings.WIDTH / 2.0F + Card.IMG_WIDTH_S * 1.8F);
                    handCardXTargets[8] = (CardSettings.WIDTH / 2.0F + Card.IMG_WIDTH_S * 2.4F);
                    handCardXTargets[9] = (CardSettings.WIDTH / 2.0F + Card.IMG_WIDTH_S * 2.9F);
                    handCardYTargets[1] += 20.0F * CardSettings.scale;
                    handCardYTargets[2] += 17.0F * CardSettings.scale;
                    handCardYTargets[3] += 12.0F * CardSettings.scale;
                    handCardYTargets[4] += 5.0F * CardSettings.scale;
                    handCardYTargets[5] += 5.0F * CardSettings.scale;
                    handCardYTargets[6] += 12.0F * CardSettings.scale;
                    handCardYTargets[7] += 17.0F * CardSettings.scale;
                    handCardYTargets[8] += 20.0F * CardSettings.scale;
                    foreach (var theCard in handCards)
                    {
                        theCard.SetScaleTo(new Vector3(0.63750005F, 0.63750005F, 0.63750005F));
                    }
                    break;
                default:
                    //logger.info("WTF MATE, why so many cards");
                    break;
            }
            for (int i = 0; i < handCards.Count; i++)
            {
                handCards[i].SetAnchoredPositionTo(new Vector2(handCardXTargets[i] ?? handCards[i].RectTransform.anchoredPosition.x, handCardYTargets[i] ?? handCards[i].RectTransform.anchoredPosition.y));
            }
            //Card card = AbstractDungeon.player.hoveredCard;
            //if (card != null)
            //{
            //    card.setAngle(0.0F);
            //    card.target_x = ((card.current_x + card.target_x) / 2.0F);
            //    card.target_y = card.current_y;
            //}
            //for (CardQueueItem q : AbstractDungeon.actionManager.cardQueue)
            //{
            //    if (q.card != null)
            //    {
            //        q.card.setAngle(0.0F);
            //        q.card.target_x = q.card.current_x;
            //        q.card.target_y = q.card.current_y;
            //    }
            //}
        }
    }
}