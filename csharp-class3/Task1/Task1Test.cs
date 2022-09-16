using NUnit.Framework;
using static NUnit.Framework.Assert;
using static Task1.Task1;

namespace Task1;

using Hand = List<Card>;
using Table = List<Card>;

public class Tests
{
    [Test]
    public void RoundWinnerTest()
    {
        That(RoundWinner(new Card(Suit.Clubs, Rank.Ace), new Card(Suit.Diamonds, Rank.King)),
            Is.EqualTo(Player.First));
        That(RoundWinner(new Card(Suit.Hearts, Rank.Seven), new Card(Suit.Clubs, Rank.Six)),
            Is.EqualTo(Player.First));
        That(RoundWinner(new Card(Suit.Hearts, Rank.King), new Card(Suit.Clubs, Rank.Eight)),
            Is.EqualTo(Player.First));

        That(RoundWinner(new Card(Suit.Clubs, Rank.King), new Card(Suit.Diamonds, Rank.Ace)),
            Is.EqualTo(Player.Second));
        That(RoundWinner(new Card(Suit.Hearts, Rank.Six), new Card(Suit.Clubs, Rank.Seven)),
            Is.EqualTo(Player.Second));
        That(RoundWinner(new Card(Suit.Hearts, Rank.Eight), new Card(Suit.Clubs, Rank.King)),
            Is.EqualTo(Player.Second));

        That(RoundWinner(new Card(Suit.Diamonds, Rank.King), new Card(Suit.Clubs, Rank.King)),
            Is.EqualTo(null));
        That(RoundWinner(new Card(Suit.Spades, Rank.Six), new Card(Suit.Hearts, Rank.Six)),
            Is.EqualTo(null));
    }

    [Test]
    public void FullDeckTest()
    {
        var deck = FullDeck();
        That(deck, Has.Count.EqualTo(DeckSize));
        That(deck, Is.Unique);
    }

    [Test]
    public void RoundTest()
    {
        var hands = new Dictionary<Player, Hand>
        {
            [Player.First] = new()
            {
                new Card(Suit.Clubs, Rank.Ace),
                new Card(Suit.Diamonds, Rank.King),
                new Card(Suit.Hearts, Rank.Seven),
                new Card(Suit.Hearts, Rank.King),
                new Card(Suit.Clubs, Rank.Six),
                new Card(Suit.Hearts, Rank.King),
                new Card(Suit.Hearts, Rank.King)
            },
            [Player.Second] = new()
            {
                new Card(Suit.Clubs, Rank.King),
                new Card(Suit.Diamonds, Rank.Ace),
                new Card(Suit.Hearts, Rank.Six),
                new Card(Suit.Hearts, Rank.King),
                new Card(Suit.Clubs, Rank.Seven),
                new Card(Suit.Hearts, Rank.King),
                new Card(Suit.Hearts, Rank.King)
            }
        };

        That(Round(hands).Item1, Is.EqualTo(Player.First));
        That(Round(hands).Item1, Is.EqualTo(Player.Second));
        That(Round(hands).Item1, Is.EqualTo(Player.First));
        That(Round(hands).Item1, Is.EqualTo(Player.Second));
        That(Round(hands).Item1, Is.EqualTo(null));
    }

    [Test]
    public void Game2CardsTest()
    {
        var six = new Card(Suit.Clubs, Rank.Six);
        var ace = new Card(Suit.Diamonds, Rank.Ace);
        var hands = new Dictionary<Player, List<Card>>
        {
            { Player.First, new List<Card> { six } },
            { Player.Second, new List<Card> { ace } }
        };
        var gameWinner = Game(hands);
        That(gameWinner, Is.EqualTo(Player.Second));
    }
}
