module Candidates

open Cell

let thingToDoToCellsOfRow cellsOfRow =
    ()

let someOtherFunction cellsOfRow =
    List.map thingToDoToCellsOfRow cellsOfRow

let getCandidatesToEliminate =
    

let eliminateCandidatesByDistinctInRow (cellList:Cell list) = 
    let localCellList :Cell list = []
    for i in 1 .. 9 do
        let result :Cell list =
            cellList
            |> Cell.getCellsOfRow i
            |> getCandidatesToEliminate
            |> eliminateCandidatesFromCells
    // incomplete
    ()

//let EliminateCandidatesByDistinctInColumn = 
//let EliminateCandidatesByDistinctInBlock = 

//let EliminateCandidatesByCandidateLines
                
let removeCandidates cellList =
    cellList
    |> eliminateCandidatesByDistinctInRow
    //|> EliminateCandidatesForGivenAndConfirmedCells  // move to top position once written
    //EliminateCandidatesByDistinctInColumn
    //EliminateCandidatesByDistinctInBlock

    // Eliminate by candidate lines
    //EliminateCandidatesByCandidateLines