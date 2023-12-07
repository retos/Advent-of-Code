using static System.Net.Mime.MediaTypeNames;

namespace AdventOfCode2023.Day07
{
    internal class Hand : IComparable<Hand>
    {
        private Dictionary<char, int> dict;
        private HandType type;
        private string cardValues;
        public string Cards { get; internal set; }
        public string CardValues 
        {
            get 
            {
                if(cardValues == null)
                {
                    cardValues = string.Empty;
                    foreach (var c in Cards)
                    {
                        switch (c)
                        {
                            //A, K, Q, J, T, 9, 8, 7, 6, 5, 4, 3, 2
                            case 'A':
                                cardValues += 'M';
                                break;
                            case 'K':
                                cardValues += 'L';
                                break;
                            case 'Q':
                                cardValues += 'K';
                                break;
                            case 'J':
                                cardValues += 'J';
                                break;
                            case 'T':
                                cardValues += 'I';
                                break;
                            case '9':
                                cardValues += 'H';
                                break;
                            case '8':
                                cardValues += 'G';
                                break;
                            case '7':
                                cardValues += 'F';
                                break;
                            case '6':
                                cardValues += 'E';
                                break;
                            case '5':
                                cardValues += 'D';
                                break;
                            case '4':
                                cardValues += 'C';
                                break;
                            case '3':
                                cardValues += 'B';
                                break;
                            case '2':
                                cardValues += 'A';
                                break;

                            default:
                                throw new ArgumentException("what have you done?");
                                break;
                        }
                    }
                }
                return cardValues;
            } 
        }
        public long Bid { get; internal set; }
        public HandType Type 
        {
            get 
            {
                if (dict == null)
                {
                    dict = Cards.Replace(" ", string.Empty)
                                 .GroupBy(c => c)
                                 .ToDictionary(gr => gr.Key, gr => gr.Count());

                    if (dict.Any(k => k.Value >= 5))
                    {
                        type = HandType.FiveOfAKind;
                    }else if (dict.Any(k => k.Value >= 4))
                    {
                        type = HandType.FourOfAKind;
                    }
                    else if (dict.Any(k => k.Value == 3) && dict.Any(k => k.Value == 2))
                    {
                        type = HandType.FullHouse;
                    }else if (dict.Any(k => k.Value == 3))
                    {
                        type = HandType.ThreeOfAKind;
                    }else if (dict.Count(d => d.Value == 2) == 2)
                    {
                        type = HandType.TwoPair;
                    }else if (dict.Any(k => k.Value == 2))
                    {
                        type = HandType.OnePair;
                    }
                    else
                    {
                        type = HandType.HighCard;
                    }

                }
                return type;
            }
        }

        public int CompareTo(Hand? other)
        {
            if (this.Type < other.Type) return -1;
            if (this.Type > other.Type) return 1;

            //if the types are the same, see other cards
            return this.CardValues.CompareTo(other.CardValues);

            return 0;
        }


    }
}