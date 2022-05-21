# Take Microsoft's dotnet SDK Base
FROM mcr.microsoft.com/dotnet/sdk
# Setup the working directory
WORKDIR /src
# Copy over all of our existing files to build inside the container
COPY . .
# Restore the dependencies of the project
RUN dotnet restore
# Build the release build of the project
RUN dotnet publish -c release -o ./bin/release/IBANCheck --no-self-contained --no-restore
# Expose 7095 5198 for web-server traffic
EXPOSE 7095 
#5198
# Execute the web-server
CMD ["./bin/release/IBANCheck"]

# docker build -t fsharp_hello_docker
# docker run -p 8080:8080 -t fsharp_hello_docker

