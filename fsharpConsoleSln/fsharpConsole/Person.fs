module Person

// This is a record definition with an added method:
type Person = { FName : string; LName : string; IsMale : bool } 

module Person =
    let FullName p = p.FName + " " + p.LName
