
module StackCalculatorTwo

// Arrays:
let myArr:array<int> = Array.zeroCreate 20
let myArr2:array<int> = Array.create 3 7
let myArr3:array<int> = [| for i in 0 .. 9 -> i |]

module SumTypesYa =
    // Discriminated union; has case identifiers (tags) that have types (not required) under a single union
    type SumTypesYa = 
        | CommaBrosType of string
        | SylvieSis of string * int

module SumTypesYeet =
    type SumTypesYeet = { Hella:string; Nump:int }
    let create filt inter = { Hella=filt; Nump=inter }  // compiler uses last defined record type with these labels for type checks

module OtherThing =
    // Record; has labels with types (required) inside of curly braces
    type OtherThing = { Hella:int; Nump:string }
    let create filt inter = { Hella=filt; Nump=inter }
    let tupCreate (filt, inter) = { Hella=filt; Nump=inter } 
    
module SomeNewModule =
    open OtherThing
    let test2 = { SumTypesYeet.Hella="this"; SumTypesYeet.Nump=7 }  // create a record type instance using equals signs (=) for each label assignment
    // I can still specify which record type to use based on label assignment and output definition:
    let test3 = { Hella=7; Nump="this" }
    // the "create" function defaults to using the most recent definition of a record type that has those specific labels":
    let test4 = SumTypesYeet.create "this"
    let test5 = tupCreate (45, "this")
    let test6 = create 45 "sumthin"

open SumTypesYa

module ItsOwnModule =
    // Is accessing the modules that areavailable at the top-level module: "StackCalculatorTwo"

    let test = CommaBrosType ("woop")  // case identifiers (tags) function as constructors as well

    let funcTestOne cBT =
        match cBT with
        | CommaBrosType myGuy -> $"This has been quite a {myGuy}."  // matching, decomposition and use
        | SylvieSis (thisOne, theOtherOne) -> $"What a wonderful {theOtherOne}, plus {thisOne} more."  // matching, decomposition and use

open ItsOwnModule

let run () =
    let myDiscUni = CommaBrosType "long day"
    let myOtherDiscUni = SylvieSis ("day", 7)
    let someRec = SumTypesYeet.create "your worst nightmare" 67

    let locFunc = funcTestOne  // for kicks and giggles
    locFunc myDiscUni |> printfn "%s"
    funcTestOne myOtherDiscUni |> printfn "%s"
    let giveANewFunc = (fun x -> CommaBrosType x)  // for more kicks and more giggles
    let timesThree = locFunc >> giveANewFunc >> funcTestOne >> giveANewFunc >> locFunc  // composition
    giveANewFunc someRec.Hella |> timesThree |> printf "%s"  // three levels nested

// Classes have parentheses (parens) in their definition lines:
type Person(fname, lname, age, favColor) = 

    // Field: by default is private; cannot be made public with access modifier; doesn't need "this."
    // "Let"s must come before "member"s in classes.
    // (None of the type definitions here are required, but they help as references.)
    let LName = lname  

    // Properties: default is public; can be made private (_.Age); can have any self identifier, not just "this" or "self" (ie _.Age)
    member self.FName = fname
    member private _.Age = age  // How is this infering the data type? From where?
    member those.FavColor = favColor

    // Utilizing property composition:
    member this.FullName = this.FName + " " + LName
    member this.ShareALotAboutMySelf = $"Hi, I'm {this.FullName}, I'm {this.Age} and my favorite color is {this.FavColor}."

    // Method:
    member this.MultiplyAgeBy factor = this.Age * factor

module Person =
    let run () =
        let originalAge = 23
        let lanie = Person("Lanie", "Davies", originalAge, "orange")
        printfn $"{lanie.FullName}"
        printfn $"{lanie.ShareALotAboutMySelf}"

        let newAge = 20
        let newLanie = Person(lanie.FName, "Rodriguez", newAge, lanie.FavColor)
        printfn $"\n{newLanie.FullName}"
        printfn $"{newLanie.ShareALotAboutMySelf}"

        let factor = 4
        printfn "\nLanie is %i years old if you multiply her age by %i." (newLanie.MultiplyAgeBy factor) factor
