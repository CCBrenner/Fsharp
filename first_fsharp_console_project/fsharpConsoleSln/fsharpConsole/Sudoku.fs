module Sudoku

type Cell = { 
    Id: int
    RowId: int
    ColId: int
    BlockId: int
    BlockRowId: int
    BlockColId: int
    Value: int
    Values: array<int> }

module Cell =
    let getRowId id = (id+1)/9
    let getColId id = id%9
    let getBlockId rowId colId = (colId/3)*(rowId/9)

    let create id =
        let values = [| for i in 0 .. 9 -> i |]
        let rowId = getRowId id
        let colId = getColId id
        let blockId = getBlockId id
        let blockRowId = getBlockRowId id
        let blockColId = getBlockColId id
        { Id=id; RowId=rowId; ColId=colId; BlockId=blockId; blockRowId=blockRowId; blockColId= Value=0; Values=values }

    let updateValue cell newVal =
        { cell with Value=newVal }
        
    let eliminatePossibility cell valIndex =
        cell.Values[valIndex] <- 0

    let resetValues cell =
        let resetArr = [| for i in 0 .. 9 -> i |]
        { cell with Values=resetArr }

    let returnCellsFromColumn cellList colNum =
        List.filter (fun x -> x.Id % 9 = colNum) cellList

    let returnCellsFromRow cellList rowNum =
        List.filter (fun x -> x.Id > (rowNum-1)*9 && x.Id < rowNum*9+1) cellList

    let returnCellFromBlock cellList blockNum =
        // blockNum is 1 .. 9 inclusive
        // 1: 1, 2, 3, 10, 11, 12, 19, 20, 21
        // 27 groupings of three
        let rows = (blockNum-1)/3
        List.filter (fun x -> x.Id) 


//type Puzzle = { }

module Puzzle =
    let cellList = [ for i in 1 .. 81 -> Cell.create i ]
        


let testJLKJLK = Cell.returnCellsFromRow Puzzle.cellList 1

//let test x = ((x-1)*9, x*9+1)
//let result = test 2

              
printfn "%s" (string cellList[81].Id)
let testCell = cellList[3].Id
// cell list
// solver algo
// cell groupings

// indexing based on group
// 1 - 81
// cols: y = ((x-1)*9)+1 
// rows: y = x+9

// cols: [ for i in 1 .. 81 -> i % 9 = x ]
// rows: [ for i in 1 .. 81 -> i < x * 9 

let getCellsOfRow cellList row =
    [ for i in cellList -> row - 1 * 9]
    
    // create the list
    // get cells of row
    // perform action


private int GetCellId(int row, int column) =>
        id = ((row - 1) * 9) + column;

        row = (((id - column) / 9) + 1)
        column = - (((row - 1) * 9) - id)

private int GetBlockId(int row, int column)
{
    int rowMod = (row - 1) % 3;
    int blockRow = (row - 1 - rowMod) / 3;

    int colMod = (column - 1) % 3;
    int blockCol = (column - 1 - colMod) / 3;

    int blockId = (blockRow * 3) + blockCol + 1;

    return blockId;
}


    //member this.Cells: List<Cell> = [ for i in 0..9 -> new Cell() ]






// Take input from user and enter as seed data in puzzle cells
// 
// Create cells list
// 