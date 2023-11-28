module TestScratch


// Test 1: How to update the label of a record.

type ValueStatus =
    | Given
    | Unconfirmed
    | Confirmed

type Cell = 
    { Id: int
      RowId: int
      ColId: int
      BlockId: int
      BlockRowId: int
      BlockColId: int
      Value: int
      ValueStatus: ValueStatus
      Values: array<int> }

let defaultCellValue = 0

let getBaseId modNum operation id = (operation (id-1) modNum)+1
let getCellLvlId = getBaseId 9
let getBlockLvlId = getBaseId 3

// constructor:
let create id =
    let values = [| for i in 0 .. 9 -> i |]
    let rowId = getCellLvlId (/) id  //let rowId = (id-1)/9+1
    let colId = getCellLvlId (%) id  //let colId = (id-1)%9+1
    let blockId = ((rowId-1)/3*3)+((colId-1)/3)+1  // "/3*3" eliminates the remainder
    let blockRowId = getBlockLvlId (/) blockId  // (blockId-1)/3+1
    let blockColId = getBlockLvlId (%) blockId  // (blockId-1)%3+1
    let cell =
        { Id=id
          RowId=rowId
          ColId=colId
          BlockId=blockId
          BlockRowId=blockRowId
          BlockColId=blockColId
          Value=defaultCellValue
          ValueStatus=Unconfirmed
          Values=values }
    cell

let singleCellInAList = [ create 1 ]

// Because lists in F# are immutable, we need to return a whole new list. Use List.map to do so:
let myFunc theList targetId replacementVal = 
    theList |> List.map (fun x -> if x.Id=targetId then { x with Value=replacementVal } else x)

let newList = myFunc singleCellInAList 1 4 // Success: Value=4


// Test 2: Can unit be stored in a list?
let someList = [(); (); ()]