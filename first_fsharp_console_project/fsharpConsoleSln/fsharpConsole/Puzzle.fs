module Puzzle

type Cell =
    { Id: int
      RowId: int
      ColId: int
      Values: array<int>}

type Puzzle =
    { something: string }

    //member this.Cells: List<Cell> = [ for i in 0..9 -> new Cell() ]