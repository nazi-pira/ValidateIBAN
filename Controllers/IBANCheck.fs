#light "off"
namespace CheckIBAN.Controllers

open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open System.Text.RegularExpressions

type Response =
    { IBANValid: bool;
      //IBAN: string
    }
    with
    end

[<ApiController>]
[<Route("[controller]")>]
type CheckIBANController (logger : ILogger<CheckIBANController>) = class
    inherit ControllerBase()

    [<HttpGet>]
    member this.Get() = {IBANValid=false}
    [<HttpGet("{iban}")>]
    member this.Get (iban : string) =
        let countries = dict [
            ("AL", 28); ("AD", 24); ("AT", 20); ("AZ", 28); ("BE", 16); ("BH", 22);
            ("BA", 20); ("BR", 29); ("BG", 22); ("CR", 21); ("HR", 21); ("CY", 28);
            ("CZ", 24); ("DK", 18); ("DO", 28); ("EE", 20); ("FO", 18); ("FI", 18);
            ("FR", 27); ("GE", 22); ("DE", 22); ("GI", 23); ("GR", 27); ("GL", 18);
            ("GT", 28); ("HU", 28); ("IS", 26); ("IE", 22); ("IL", 23); ("IT", 27);
            ("KZ", 20); ("KW", 30); ("LV", 21); ("LB", 28); ("LI", 21); ("LT", 20);
            ("LU", 20); ("MK", 19); ("MT", 31); ("MR", 27); ("MU", 30); ("MC", 27);
            ("MD", 24); ("ME", 22); ("NL", 18); ("NO", 15); ("PK", 24); ("PS", 29);
            ("PL", 28); ("PT", 25); ("RO", 24); ("SM", 27); ("SA", 24); ("RS", 22);
            ("SK", 24); ("SI", 19); ("ES", 24); ("SE", 24); ("CH", 21); ("TN", 24);
            ("TR", 26); ("AE", 23); ("GB", 22); ("VG", 24);] in

        //let result ok = {IBANValid=ok; IBAN=iban } in
        let result ok = { IBANValid=ok } in
        let validateCheckSum (ibanCheck : string) =
            let reduce result ch = (result, ch) |> function
                | (-1,_) -> -1 
                | (result,ch) -> (if '0' <= ch && ch <= '9' then 
                        (10*result + (int(ch) - int('0'))) % 97
                    else if 'A' <= ch && ch <= 'Z' then
                        (100*result + (10 + int(ch) - int('A'))) % 97  
                    else -1 ) in 
                    if 1 = Seq.fold reduce 0 (Seq.toList ibanCheck) then
                       result true 
                    else result false in

        let ValidateCountry (ibanStrip : string) =
            let country = ibanStrip.Substring(0,2) in
            match countries.TryGetValue(country) with
            | true, length ->   // country should have iban of this length
                if length = ibanStrip.Length then 
                    validateCheckSum (ibanStrip.Substring(4) + ibanStrip.Substring(0,4))
                else 
                    result false // Incorrect Lenght
            | _ ->  result false in    // country not known

        let validateLenght =
            let ibanStrip = iban.Replace(" ", "") in
            if ibanStrip.Length > 4 then
                ValidateCountry ibanStrip
            else 
                result false in
        
        // Validate characters 
        if not (Regex.IsMatch(iban, @"[^A-Z\d ]")) then 
            validateLenght
        else 
            result false
end


(* TEST______TRUE
curl -k https://localhost:7095/CHeckIBAN 
 Th
 curl -k https://localhost:7095/CheckIBAN/
 curl -k https://localhost:7095/CheckIBAN/sfsfsfs%20lsls 
TEST______False
 curl -k "https://localhost:7095/CheckIBAN/GB82 WEST 1234 5698 7654 32"
 curl -k 'https://localhost:7095/CheckIBAN/GB82 WEST 1234 5698 7654 32'
 curl -k https://localhost:7095/CheckIBAN/GB82%20WEST%201234%205698%207654%2032
 nazipira@Nazis-MacBook-Pro FSharpWebApi % curl -k https://localhost:7095/CheckIBAN/GB82%20WEST%201234%205698%207654%2032
{"IBANValid":true,"iban":"GB82 WEST 1234 5698 7654 32"}%                                                       
nazipira@Nazis-MacBook-Pro FSharpWebApi % curl -k https://localhost:7095/CheckIBAN/GHGG2{"IBANValid":false,"iban":"GHGG2"}%                                                                            
nazipira@Nazis-MacBook-Pro FSharpWebApi % curl -k https://localhost:7095/CheckIBAN/GBGG2
*)