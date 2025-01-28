# Use a imagem base do SDK do .NET 6 para build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copie os arquivos do projeto e restaure as dependências
COPY *.csproj .
RUN dotnet restore

# Copie todo o código e construa o projeto
COPY . .
RUN dotnet publish -c Release -o /app/publish

# Use a imagem base do runtime do .NET 6 para a imagem final
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app

# Copie os arquivos publicados da etapa de build para a imagem final
COPY --from=build /app/publish .

# Defina a porta que o aplicativo vai usar
EXPOSE 80

# Comando para rodar o aplicativo
ENTRYPOINT ["dotnet", "GeekShopping.Web.dll"]