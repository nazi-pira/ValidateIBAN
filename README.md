# Introduction 
IBAN Validator: This is a simple application that validates the IBAN number. 

# Getting Started
1.	run "dotbnet restore" to download the dependencies
2.	run "dotnet build", 
3.  run "dotnet publish -c Release -o published"
4.  run "dotnet published/IBANCheck.dll" 
5. Test in the browser "http://localhost:5000/CheckIBAN/GB82 WEST 1234 5698 7654 32"
5.  ctrl+c
6. docker build -t aspnetapp .
7. docker run -it --rm -p 5000:80 --name aspnetcore_sample aspnetapp
the program can be run with http protocol and on th port 5000
#  Test--
You can find the automated test in the test file as well.
curl:
curl  http://localhost:7095/CheckIBAN/GBGG2
TEST______False
curl  http://localhost:5000/CHeckIBAN 
curl  http://localhost:5000/CheckIBAN/
curl  http://localhost:5000/CheckIBAN/sfsfsfs%20lsls 

TEST______TRUE
curl  http://localhost:5000/CheckIBAN/GB82%20WEST%201234%205698%207654%2032
curl  http://localhost:5000/CHeckIBAN/SK08%200900%200000%200001%202312%203123


