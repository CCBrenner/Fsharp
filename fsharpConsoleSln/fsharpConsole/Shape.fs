module Shape

type Shape =
    | Rectangle of width : float * length : float
    | Circle of radius : float
    | Prism of width : float * float * height : float

let getShapeWidth shape =
    match shape with
    | Rectangle(width = w) -> w
    | Circle(radius = r) -> 2. * r
    | Prism(width = w) -> w

let stateWidth shape =
    match shape with
    | Rectangle(width = w) -> $"As a rectangle my width is {w}"
    | Circle(radius = r) -> $"As a circle my width is {2. * r}"
    | Prism(width = w) -> $"As a prism my width is {w}"

let renderShapes () =
    let rect = Rectangle(length = 1.3, width = 10.0)
    let circ = Circle (1.0)
    let prism = Prism(5., 2.0, height = 3.0)
    
    printfn $"{getShapeWidth rect}"
    printfn $"{getShapeWidth circ}"
    printfn $"{getShapeWidth prism}"

    printfn $"\n{stateWidth rect}"
    printfn $"{stateWidth circ}"
    printfn $"{stateWidth prism}"
