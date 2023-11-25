module ExampleTwo

open Person

let changeLastName (p:Person) newLName =
        match p.IsMale with
        | true -> p
        | false -> { p with LName = newLName }

let runExample () =

    let firstPerson =
        { FName = "Cory"
          LName = "Trail"
          IsMale = true }
    let newFirstPerson = changeLastName firstPerson "Edwards"
    printf "Hi, I am %s\n" (Person.FullName firstPerson)

    let secondPerson =
        { FName = "Lindsey"
          LName = "Trail"
          IsMale = false }
    let newSecondPerson = changeLastName secondPerson "Edwards"
    printf "Hi, I am %s\n" (Person.FullName newSecondPerson)

    //let newNewSecondPerson = { secondPerson with LName = "Edwards" }
    printf $"Hi, I am %s{(Person.FullName secondPerson)}"
