module Puzzle

open Cell

//type Puzzle = { }

let templateCellList = [ for i in 1 .. 81 -> Cell.create i ]
let reassignableCellList = templateCellList
    
let loadValuesIntoCellList cellList seedValues =
    [ for i in cellList do
        i.Value = seedValues[i.Id-1] ]

let removeCandidates =
    // Eliminate candidates for Given and Confirmed cells
    Cell.EliminateCandidatesForGivenAndConfirmedCells

    // Eliminate by distinct in neighborhood
    Row.EliminateCandidatesByDistinctInRow
    Column.EliminateCandidatesByDistinctInColumn  // could put all of these into "Candidates.fs" file
    Block.EliminateCandidatesByDistinctInBlock

    // Eliminate by candidate lines
    Block.EliminateCandidatesByCandidateLines

let solve =

    let markCellsAsGivenOrUnconfirmed cellList =
        [ for i in cellList do
            match i.Value with
            | 0 -> { i with ValueStatus=Unconfirmed }
            | _ -> { i with ValueStatus=Given } ]

    // create solver data structure
    let CurrentCell = 1
    let PuzzleIsSolvable = true

    // mark cells as Given or Unconfirmed
    let cellList = markCellsAsGivenOrUnconfirmed reassignableCellList

    // perform initial removal of candidates
    let cellList = removeCandidates cellList

    // start (recursive) algo

    ()  // unfinished