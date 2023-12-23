module ProjectVals

let defaultCellValue = 0

let replaceElementInList baseOneIndex replacementValue myList =
    [1..(List.length myList)] |> List.map (fun i -> if i=baseOneIndex then replacementValue else myList[i-1])