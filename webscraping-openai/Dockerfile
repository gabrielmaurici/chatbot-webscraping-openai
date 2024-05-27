FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY /src/*.sln .
COPY /src/WebScraping.OpenAI.Grpc/*.csproj ./WebScraping.OpenAI.Grpc/
COPY /src/WebScraping.OpenAI.Grpc/*.json ./WebScraping.OpenAI.Grpc/
COPY /src/WebScraping.OpenAI.Grpc/Protos/*.proto ./WebScraping.OpenAI.Grpc/Protos/
COPY /src/WebScraping.OpenAI.IoC/*.csproj ./WebScraping.OpenAI.IoC/
COPY /src/WebScraping.OpenAI.Domain/*.csproj ./WebScraping.OpenAI.Domain/
COPY /src/WebScraping.OpenAI.Infrastructure/*.csproj ./WebScraping.OpenAI.Infrastructure/
COPY /src/WebScraping.OpenAI.Application/*.csproj ./WebScraping.OpenAI.Application/
RUN dotnet restore

WORKDIR /src/WebScraping.OpenAI.Grpc/
COPY . .

RUN dotnet build "WebScraping.OpenAI.Grpc.csproj" -c Release -o /app/build
RUN dotnet publish "WebScraping.OpenAI.Grpc.csproj" -c release -o /app --self-contained -r linux-x64

FROM selenium/standalone-chrome
USER root
WORKDIR /app
COPY --from=build /app .
EXPOSE 5001
ENTRYPOINT ["./WebScraping.OpenAI.Grpc"]