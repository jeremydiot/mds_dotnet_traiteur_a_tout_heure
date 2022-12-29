FROM mcr.microsoft.com/dotnet/sdk:7.0
USER root
WORKDIR /app
EXPOSE 80

COPY . .
ENTRYPOINT [ "./entrypoint.sh" ]