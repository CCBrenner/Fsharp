module Cards

type Suit =
    | Spades
    | Clubs
    | Hearts
    | Diamonds

type Rank =
    | Ace
    | Value of int
    | King
    | Queen
    | Jack

module Rank =
    let GetAllRanks() =
        [   yield Ace
            for i in 2 .. 10 do yield Value i
            yield Jack
            yield Queen
            yield King    ]

/// This is a record type that combines a Suit and a Rank.
/// It's common to use both Records and Discriminated Unions when representing data.
type Card = { Suit: Suit; Rank: Rank }

/// This computes a list representing all the cards in the deck.
let createDeck () =
    let suits = [ Hearts; Diamonds; Clubs; Spades ]
    [ for suit in suits do
        for rank in Rank.GetAllRanks() do
            yield { Suit=suit; Rank=rank } ]

/// This example converts a 'Card' object to a string.
let showPlayingCard (c: Card) =
    let rankString =
        match c.Rank with
        | Ace -> "Ace"
        | King -> "King"
        | Queen -> "Queen"
        | Jack -> "Jack"
        | Value n -> string n
    let suitString =
        match c.Suit with
        | Clubs -> "Clubs"
        | Diamonds -> "Diamonds"
        | Spades -> "Spades"
        | Hearts -> "Hearts"
    rankString  + " of " + suitString

/// This example prints all the cards in a playing card deck.
let printAllCards () =
    let deck = createDeck ()
    let action = showPlayingCard >> printfn "%s"
    for card in deck do action card
