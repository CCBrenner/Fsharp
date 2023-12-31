﻿module Puzzle

open Cell

//type Puzzle = { }

let originalCellList = createCellList ()
let reassignableCellList = createCellList ()
    
let loadValuesIntoCellList (seedValues:int list) cellList =
    cellList
    |> List.map (fun x -> if seedValues[x.Id-1]<>0 then { x with Value=seedValues[x.Id-1] } else x)

    //[ for i in 0 .. 80 do
    //    let cell = cellList[i]
    //    cell = { cell with Value=seedValues[i] }
    //    cell]

let solve =

    let markCellsAsGivenOrUnconfirmed cellList =
        [ for i in cellList do
            match i.Value with
            | 0 -> { i with ValueStatus=ValueStatus.Unconfirmed }
            | _ -> { i with ValueStatus=ValueStatus.Given } ]

    // create solver data structure
    let CurrentCell = 1
    let PuzzleIsSolvable = true

    // mark cells as Given or Unconfirmed
    let cellList = markCellsAsGivenOrUnconfirmed reassignableCellList

    // perform initial removal of candidates
    //let cellList = removeCandidates cellList

    // start (recursive) algo

    ()  // unfinished