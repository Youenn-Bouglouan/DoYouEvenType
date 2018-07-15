namespace DoYouEvenTypeBro

module NullExample =

    type Contact = {
        Email: string
        PhoneNumber: string
    }

    type Customer = {
        Code: string
        Name: string
        Contact: Contact
    }

    let sendEmail customer message =
        printf "sending email to %s <%s>: %s\n" customer.Code customer.Contact.Email message |> ignore
    
    let customer1 = { Code = "TNOWAK"; Name = "Tomek Nowak"; Contact = Unchecked.defaultof<Contact> }

    sendEmail customer1 "Hello Dear Customer!"


// Let's fix this!
// Option<T>
// Some T or None

module OptionExampleTODO =

    type Contact = {
        Email: string
        PhoneNumber: string
    }

    type Customer = {
        Code: string
        Name: string
        Contact: Contact
    }

    let sendEmail customer message =
        printf "sending email to %s <%s>: %s\n" customer.Code customer.Contact.Email message |> ignore
    
    let customer1 = { Code = "TNOWAK"; Name = "Tomek Nowak"; Contact = Unchecked.defaultof<Contact> }

    sendEmail customer1 "Hello Dear Customer!"


// solution


module OptionExampleSolution =

    type Contact = {
        Email: string
        PhoneNumber: string option
    }

    type Customer = {
        Code: string
        Name: string
        Contact: Contact option
    }

    let sendEmail customer message =
        match customer.Contact with
        | Some contact ->
            printf "sending email to %s <%s>: %s\n" customer.Code contact.Email message |> ignore
        | None ->
            printf "oops, cannot contact %s without an email\n" customer.Code        
    
    let customer1 = { Code = "TNOWAK"; Name = "Tomek Nowak"; Contact = None }

    sendEmail customer1 "Hello Dear Customer!"

    let customer2 = { Code = "TNOWAK"; Name = "Tomek Nowak"; Contact = Some { Email = "a.a@a.com"; PhoneNumber = Some "123456789" } }

    sendEmail customer2 "Hello Dear Customer!"

