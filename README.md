# Introduction 
IBAN Validator: This is a simple application that validates the IBAN number. 

# Getting Started
TODO: To get the code up and running you need to have dotnet installed
1.	run "dotbnet restore" to download the dependencies
2.	run "dotnet build"
3.	run "dotnet watch run"
4.	API test: curl -k https://localhost:7095/CheckIBAN/GBGG2
TEST______TRUE
curl -k https://localhost:7095/CHeckIBAN 
curl -k https://localhost:7095/CheckIBAN/
curl -k https://localhost:7095/CheckIBAN/sfsfsfs%20lsls 

TEST______False
curl -k "https://localhost:7095/CheckIBAN/GB82 WEST 1234 5698 7654 32"
curl -k 'https://localhost:7095/CheckIBAN/GB82 WEST 1234 5698 7654 32'
curl -k https://localhost:7095/CheckIBAN/GB82%20WEST%201234%205698%207654%2032
curl -k https://localhost:7095/CheckIBAN/GB82%20WEST%201234%205698%207654%2032
# Build and Test--
TODO: Here you have some Url to test the application. 
https://localhost:5001/CheckIBAN/GB82%20WEST%201234%205698%207654%2032
https://localhost:7095/CheckIBAN/GB82 WEST 1234 5698 7654 32
https://localhost:7095/CheckIBAN/GB82 WEST 1234 5698 7654
https://localhost:7095/CheckIBAN/GB82%20WEST%201234%205698%207654%2032
