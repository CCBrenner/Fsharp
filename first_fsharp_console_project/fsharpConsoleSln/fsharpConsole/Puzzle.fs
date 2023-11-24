module Puzzle

type Cell = { Id: int; Value: int; Values: array<int> }

module Cell =
    let create id =
        let values = [| for i in 0 .. 9 -> i |]
        { Id=id; Value=0; Values=values }

    let updateValue cell newVal =
        { cell with Value=newVal }
        
    let eliminatePossibility cell valIndex =
        cell.Values[valIndex] <- 0

    let resetValues cell =
        let resetArr = [| for i in 0 .. 9 -> i |]
        { cell with Values=resetArr }

let cellList = [ for i in 0 .. 81 -> Cell.create i ]

printfn "%s" (string cellList[81].Id)

type Puzzle =
    { something: string }
              


    //member this.Cells: List<Cell> = [ for i in 0..9 -> new Cell() ]