module Candidates

open Cell

let private getCandidatesToEliminate rowOfCells =
    rowOfCells
    |> List.filter (fun x -> x.Value<>ProjectVals.defaultCellValue)
    |> List.map (fun x -> x.Value)
    |> List.distinct

let private eliminateCandidate candidate cell =
    cell.Values[candidate] <- ProjectVals.defaultCellValue
    cell

let private eliminateCandidatesFromCells cellsOfGrouping candidatesToEliminate =
    [ for candidate in candidatesToEliminate do
        for cell in cellsOfGrouping do
          if cell.ValueStatus=ValueStatus.Unconfirmed
            then yield eliminateCandidate candidate cell
          else yield cell ]

let private eliminateCellsPerGrouping cellsOfGrouping =
    cellsOfGrouping
    |> getCandidatesToEliminate
    |> eliminateCandidatesFromCells cellsOfGrouping

let private eliminateCandidatesByDistinctInGrouping getCellsOfGrouping (cellList:Cell.T list) = 
    let listOfCellsByRow = [ for i in 1 .. 9 -> cellList |> getCellsOfGrouping i ]
    listOfCellsByRow |> List.collect eliminateCellsPerGrouping

let eliminateCandidatesByDistinctInRow =
    eliminateCandidatesByDistinctInGrouping Cell.getCellsOfRow

let eliminateCandidatesByDistinctInColumn =
    eliminateCandidatesByDistinctInGrouping Cell.getCellsOfColumn

let eliminateCandidatesByDistinctInBlock =
    eliminateCandidatesByDistinctInGrouping Cell.getCellsOfBlock

let eliminateCandidatesForGivenAndConfirmedCells cellList =
    let clearGivenAndConfirmed cell =
        if cell.ValueStatus<>ValueStatus.Unconfirmed
        then { cell with Values=Array.create 10 0 }
        else cell
    cellList |> List.map clearGivenAndConfirmed

let removeCandidates cellList =
    cellList
    |> eliminateCandidatesForGivenAndConfirmedCells
    |> eliminateCandidatesByDistinctInRow
    |> eliminateCandidatesByDistinctInColumn
    |> eliminateCandidatesByDistinctInBlock
    // other methods of eliminate candidates:
    //|> eliminateCandidatesByCandidateLines
    //|> eliminateCandidatesByDoublePairs
    //|> eliminateCandidatesByMultipleLines
    //|> eliminateCandidatesByNakedPairsAndTriplesAndQuadruples
    //|> eliminateCandidatesByHiddenPairsAndTriplesAndQuadruples
    //|> eliminateCandidatesByXwings
    //|> eliminateCandidatesBySwordfish

let rehydrateCandidates cell = { cell with Values=[|0..9|] }

let rehydrateCandidatesOfUnconfirmedCells cellList =
    cellList
    |> List.map (fun x ->
        match x.ValueStatus with
        | ValueStatus.Unconfirmed -> rehydrateCandidates x
        | _ -> x )
