// Колода

using Deck = System.Collections.Generic.List<Card>;
// Набор карт у игрока
using Hand = System.Collections.Generic.List<Card>;
// Набор карт, выложенных на стол
using Table = System.Collections.Generic.List<Card>;

// Масть
internal enum Suit
{
    Clubs,
    Diamonds,
    Hearts,
    Spades
}

// Значение
internal enum Rank
{
    Six = 6,
    Seven,
    Eight,
    Nine,
    Ten,
    Jack,
    Queen,
    King,
    Ace
}

internal enum LogLevel
{
    General = 0,
    Fine = 1,
    Finest = 2
}

// Карта
internal record Card(Suit Suit, Rank Rank)
{
    public override string ToString()
    {
        var suit = Suit switch
        {
            Suit.Clubs => "♣",
            Suit.Diamonds => "♦",
            Suit.Hearts => "♥",
            Suit.Spades => "♠",
            _ => throw new ArgumentOutOfRangeException()
        };

        var rank = Rank switch
        {
            Rank.Ace => "A",
            Rank.King => "K",
            Rank.Queen => "Q",
            Rank.Jack => "J",
            _ => ((int)Rank).ToString()
        };

        return $"{rank} {suit}";
    }
}

// Тип для обозначения игрока (первый, второй)
internal enum Player
{
    First,
    Second
}

namespace Task1
{
    public class Task1
    {
        /*
         * Реализуйте игру "Пьяница" (в простейшем варианте, на колоде в 36 карт)
         * https://ru.wikipedia.org/wiki/%D0%9F%D1%8C%D1%8F%D0%BD%D0%B8%D1%86%D0%B0_(%D0%BA%D0%B0%D1%80%D1%82%D0%BE%D1%87%D0%BD%D0%B0%D1%8F_%D0%B8%D0%B3%D1%80%D0%B0)
         * Рука — это набор карт игрока. Карты выкладываются на стол из начала "рук" и сравниваются
         * только по значениям (масть игнорируется). При равных значениях сравниваются следующие карты.
         * Набор карт со стола перекладывается в конец руки победителя. Шестерка туза не бьёт.
         *
         * Реализация должна сопровождаться тестами.
         */

        // Размер колоды
        internal const int DeckSize = 36;

        // Возвращается null, если значения карт совпадают
        internal static Player? RoundWinner(Card card1, Card card2)
        {
            if (card1.Rank > card2.Rank)
                return Player.First;
            if (card2.Rank > card1.Rank)
                return Player.Second;
            return null;
        }

        // Возвращает полную колоду (36 карт) в фиксированном порядке
        internal static Deck FullDeck()
        {
            var deck = new Deck();
            foreach (var rank in Enum.GetValues<Rank>())
            foreach (var suit in Enum.GetValues<Suit>())
                deck.Add(new Card(suit, rank));
            return deck;
        }

        internal static Deck Shuffle(Deck deck)
        {
            var random = new Random();
            for (var i = 0; i < deck.Count; i++)
            {
                var j = random.Next(i, deck.Count);
                (deck[i], deck[j]) = (deck[j], deck[i]);
            }

            return deck;
        }

        // Раздача карт: случайное перемешивание (shuffle) и деление колоды пополам
        internal static Dictionary<Player, Hand> Deal(Deck deck)
        {
            deck = Shuffle(deck);
            const int half = DeckSize / 2;
            var firstHand = deck.GetRange(0, half);
            var secondHand = deck.GetRange(half, half);
            return new Dictionary<Player, Hand>
            {
                [Player.First] = firstHand,
                [Player.Second] = secondHand
            };
        }

        private static void Log(LogLevel level, string message)
        {
            var padding = new string(' ', (int)level) + "- ";
            message = message.Replace(Player.First.ToString(), "Игрок 1");
            message = message.Replace(Player.Second.ToString(), "Игрок 2");
            Console.WriteLine(padding + message);
        }

        private static Card FetchOne(Hand hand, Table table)
        {
            var card = hand.First();
            hand.Remove(card);
            table.Add(card);
            Log(LogLevel.Finest, $"Взята карта {card}");
            return card;
        }

        // Один раунд игры (в том числе спор при равных картах).
        // Возвращается победитель раунда и набор карт, выложенных на стол.
        internal static Tuple<Player?, Table> Round(Dictionary<Player, Hand> hands)
        {
            var table = new Table();
            Player? winner;

            do
            {
                var firstEmpty = hands[Player.First].Count == 0;
                var secondEmpty = hands[Player.Second].Count == 0;

                if (firstEmpty && secondEmpty)
                {
                    Log(LogLevel.General, "У обоих игроков нет карт, ничья");
                    return new Tuple<Player?, Deck>(null, table);
                }

                if (firstEmpty)
                {
                    Log(LogLevel.General, $"В руке закончились карты, {Player.Second} победил");
                    return new Tuple<Player?, Deck>(Player.Second, table);
                }

                if (secondEmpty)
                {
                    Log(LogLevel.General, $"В руке закончились карты, {Player.First} победил");
                    return new Tuple<Player?, Deck>(Player.First, table);
                }

                Log(LogLevel.Fine, "Начало раунда");
                Log(LogLevel.Finest, $"{Player.First} берет карту");
                var firstCard = FetchOne(hands[Player.First], table);
                Log(LogLevel.Finest, $"{Player.Second} берет карту");
                var secondCard = FetchOne(hands[Player.Second], table);
                Log(LogLevel.Finest, "Сравниваются карты");

                winner = RoundWinner(firstCard, secondCard);
            } while (winner == null);

            return new Tuple<Player?, Deck>(winner.Value, table);
        }

        // Полный цикл игры (возвращается победивший игрок)
        // в процессе игры печатаются ходы
        internal static Player? Game(Dictionary<Player, Hand> hands)
        {
            var round = 0;

            while (true)
            {
                round++;
                var (winner, table) = Round(hands);
                if (winner != null)
                {
                    Log(LogLevel.General, $"Раунд {round} окончен, победил {winner}");
                    hands[winner.Value].AddRange(Shuffle(table));
                } else
                    Log(LogLevel.General, $"Раунд {round} окончен, ничья");

                var firstEmpty = hands[Player.First].Count == 0;
                var secondEmpty = hands[Player.Second].Count == 0;

                if (firstEmpty && secondEmpty)
                    return null;

                if (firstEmpty)
                    return Player.Second;

                if (secondEmpty)
                    return Player.First;
            }
        }

        public static void Main(string[] args)
        {
            var deck = FullDeck();
            var hands = Deal(deck);
            var winner = Game(hands);

            if (winner == null)
                Log(LogLevel.General, "Оба игрока не могут сделать ход, ничья");
            else
                Log(LogLevel.General, $"Победил {winner}!");
        }
    }
}
